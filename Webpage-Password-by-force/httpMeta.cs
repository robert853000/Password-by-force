using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows;

namespace Webpage_Password_by_force
{
    public class httpPost
    {
        public string vName
        {
            get;
            set;
        }
        public string vValue
        {
            get;
            set;
        }
        public int vId
        { 
            get;
            set;
        }
    }
    public class httpHeaderX
    {
        public string vName
        {
            get;
            set;
        }
        public string vValue
        {
            get;
            set;
        }
        public int vId
        {
            get;
            set;
        }
    }
    public class webCookieX
    {
        public string vName
        {
            get;
            set;
        }
        public string vValue
        {
            get;
            set;
        }
        public string vDomain
        {
            get;
            set;
        }

        public string vPath
        {
            get;
            set;
        }
        public int vId
        {
            get;
            set;
        }

        public Cookie cook = null;
    }
    public class httpMetaData
    {
        public Uri targetURL = null;
        public string postMethod = "GET";

        public int timeout = 200000;
        public string AUTH_USERNAME = "";
        public string AUTH_PASSWORD = "";
        void msgBox(string text)
        {
            string messageBoxText = text;
            string caption = "?";
            MessageBoxButton button = MessageBoxButton.OK;
            MessageBoxImage icon = MessageBoxImage.Warning;
            MessageBoxResult result;

            result = MessageBox.Show(messageBoxText, caption, button, icon, MessageBoxResult.Yes);
        }
        public bool supplyURL(string s)
        {
            targetURL = null;
            Uri uri;
            
            if (!Uri.TryCreate(s, UriKind.Absolute, out uri))
            {
                msgBox("Not a valid url: '" + s + "'");
                return false;
            }
            targetURL = uri;
            return true;

        }

        public Uri getURL()
        {
            Uri uri;
            string s = injectCustomValues(targetURL.OriginalString, false);
            //msgBox(s);
            if (!Uri.TryCreate(s, UriKind.Absolute, out uri))
            {
                msgBox("Not a valid url: '" + s + "'");
                return targetURL;
            }
           
            //msgBox(s);
            return uri;
        }
        /*
         * 
         * 
         *      COOKIES
         * 
         * 
         * 
         * 
         * 
         * */

        public int cookieCount = 0;
        public int cookieId = 0;
        public List<webCookieX> cookieItems = null;
        public System.Net.CookieContainer myCookieContainer;
        public System.Net.CookieContainer updatedCookieContainer
        {
            get
            {
                updateCookieValues();
                return myCookieContainer;
            }
            set
            {
                throw new Exception();
            }
        }

        public void removeCookie(string key, string domain)
        {
            foreach (webCookieX co in cookieItems)
            {
                if (co.vName == key && co.vDomain == domain)
                {
                    removeCookie(co);
                    return;
                }
            }
        }
        public void removeCookie(string id)
        {
            removeCookie(int.Parse(id));
        }
        public void removeCookie(int id)
        {
            foreach (webCookieX co in cookieItems)
            {
                if (co.vId == id)
                {
                    removeCookie(co);
                    return;
                }
            }
        }
        public void removeCookie(webCookieX cookieX)
        {
            CookieContainer cookies_container = updatedCookieContainer;
            string d = cookieX.vDomain;
            if (d.StartsWith("."))
            {
                d = d.Remove(0, 1);
            }
            string s = "http://" + d + cookieX.vPath;

            Uri uri;
            if (!Uri.TryCreate(s, UriKind.Absolute, out uri))
            {
                msgBox("777 Fail: invalid uri:'" + s + "'");
            }
            else
            {
                
                foreach (Cookie each_cookie in cookies_container.GetCookies(uri))
                {

                    if (!each_cookie.Expired && each_cookie.Name == cookieX.vName)
                    {
                        each_cookie.Expires = DateTime.Now.Subtract(TimeSpan.FromDays(1));
                    }

                }

            }
           

            cookieItems.Remove(cookieX);
        }

        public void addOrEditCookie(string key, string val, string path, string domain)
        {
            if (string.IsNullOrEmpty(path))
            {
                path = "/";
            }
            if (!path.StartsWith("/"))
            {
                path = "/" + path;
            }

            if (!validateCookieURI(ref domain)) return;
            addOrEditCookie(new Cookie(key, val, path, domain));

        }
        public webCookieX containtsCookie(string name, string path, string domain)
        {

            foreach (webCookieX co in cookieItems)
            {
                if (co.vName == name && co.vPath == path && co.vDomain == domain)
                {
                    return co;
                }
            }
            return null;
        }
        public void addOrEditCookie(Cookie cook)
        {

            webCookieX cookieX = containtsCookie(cook.Name, cook.Path, cook.Domain);

            if (cookieX == null)
            {
                cookieX = new webCookieX()
                {
                    vName = cook.Name,
                    vValue = cook.Value,
                    vPath = cook.Path,
                    vDomain = cook.Domain,
                    vId = ++cookieId,
                    cook = cook
                };
                cookieItems.Add(cookieX);
            }
            else
            {
                cookieX.vValue = cook.Value;
                if (cookieX.cook != null) cookieX.cook.Value = cook.Value;

            }

        }

        public void onCookiesCollected(CookieCollection colCookies)
        {
            foreach (Cookie cook in colCookies)
            {
                addOrEditCookie(cook);
            }
        }

        public void updateCookieValues()
        {

            foreach (webCookieX co in cookieItems)
            {
                string d = co.vDomain;
                if (d.StartsWith("."))
                {
                    d = d.Remove(0, 1);
                }
                string s = "http://" + d + co.vPath;

                Uri uri;
                if (!Uri.TryCreate(s, UriKind.Absolute, out uri))
                {
                    msgBox("Fail: invalid uri:'" + s + "'");
                }else
                {
                    myCookieContainer.SetCookies(uri, co.vName + "=" + co.vValue + ";"); // "a=a,b=b"
                }
                

            }

        }
        public bool validateCookieURI(ref string uriStr)
        {
            if (uriStr.StartsWith("https:"))
            {
                uriStr = uriStr.Remove(0, 6);
            }
            if (uriStr.StartsWith("http:"))
            {
                uriStr = uriStr.Remove(0, 5);
            }
            if (uriStr.StartsWith(":"))
            {
                uriStr = uriStr.Remove(0, 1);
            }
            if (uriStr.StartsWith("/"))
            {
                uriStr = uriStr.Remove(0, 1);
            }
            if (uriStr.StartsWith("."))
            {
                uriStr = uriStr.Remove(0, 1);
            }

            Regex regex = new Regex("^(http[s]?:?/?/?)?(localhost|([0-9]{1,3}\\.){3}[0-9]{1,3}|(?!-)[A-Za-z0-9-]+([\\-\\.]{1}[a-z0-9]+)*\\.[A-Za-z]{2,6})$", RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.Multiline);

            bool isMatch = regex.IsMatch(uriStr);
            if (!isMatch)
            {
                msgBox("000 Not a valid url: '" + uriStr + "'");
            }
            return isMatch;

        }

        /*
         * 
         * 
         *      HEADERS
         * 
         * 
         * 
         * 
         * 
         * */

        public int headerCount = 0;
        public int headerId = 0;
        public List<httpHeaderX> headerItems = null;

 

        public httpMetaData(System.Net.CookieContainer xCookieContainer)
        {
            headerItems = new List<httpHeaderX>();
            cookieItems = new List<webCookieX>();
            postItems =  new List<httpPost>();
            myCookieContainer = xCookieContainer;
        }

        public void addHeaderItem(string key, string val)
        {
            headerItems.Add(new httpHeaderX()
            {
                vName = key,
                vValue = val,
                vId = ++headerId
            });
        }

        public void addRequestHeaders(HttpWebRequest wr)
        {
            foreach (httpHeaderX header in headerItems)
            {

                wr.Headers.Add(Uri.EscapeDataString(header.vName), Uri.EscapeDataString(header.vValue));
                //wr.Headers.Add(Uri.EscapeDataString(header.vName), header.vValue);
            }
        }
    
     

        public void removeHeader(string id)
        {
            removeHeader(int.Parse(id));

        }
        public void removeHeader(int id)
        {
            foreach (httpHeaderX header in headerItems)
            {
                if (header.vId == id)
                {
                    headerItems.Remove(header);
                    return;
                }
            }

        }

        /*
         * 
         * POST
         * 
         * */
        public int postCount = 0;
        public int postId = 0;
        public List<httpPost> postItems = null;

        public void addPostItem(string key, string val)
        {
            postItems.Add(new httpPost()
            {
                vName = key,
                vValue = val,
                vId = ++postId
            });
        }
        public void removePost(string id)
        {
            removePost(int.Parse(id));

        }
        public void removePost(int id)
        {
            foreach (httpPost postData in postItems)
            {
                if (postData.vId == id)
                {
                    postItems.Remove(postData);
                    return;
                }
            }

        }
        public string injectCustomValues(string str, bool escape)
        {
            str = injectCustomValues(str);
            if (escape)
            {
                return Uri.EscapeDataString(str);
            }
            return str;
            
        }
        public string injectCustomValues (string str)
        {
            str = str.Replace("$$$username", AUTH_USERNAME);
            str = str.Replace("$$$password", AUTH_PASSWORD);
            return str;
        }
        public void addPostData(ref string outx)
        {
            string amp = "";
            foreach (httpPost postData in postItems)
            {

                outx = outx +amp +injectCustomValues (postData.vName,true) + "=" +injectCustomValues(postData.vValue,true);
                amp = "&";
            }
           // msgBox(outx);
        }







        /*
         * 
         * 
         *  DUMP
         * */
        public string dumpCookies()
        {
            var table = (Hashtable)updatedCookieContainer.GetType().InvokeMember("m_domainTable",
              BindingFlags.NonPublic |
              BindingFlags.GetField |
              BindingFlags.Instance,
              null,
              updatedCookieContainer,
              null);
            string txt1 = "";
            foreach (string key in table.Keys)
            {
                txt1 += key + ":\n";
                string original = key;

                if (original.StartsWith("."))
                {
                    original = original.Remove(0, 1);
                    txt1 += "\n";
                    continue;
                }
                if (!original.StartsWith("http"))
                {
                    original = "http://" + original;
                }

                Uri uri;
                if (!Uri.TryCreate(original, UriKind.Absolute, out uri))
                {
                    msgBox("Fail: invalid uri:'" + key + "'");
                }
                else
                {
                    foreach (Cookie cookie in updatedCookieContainer.GetCookies(uri))
                    {
                        txt1 += "" + cookie.Name + " = " + cookie.Value + " ; Domain = " + cookie.Domain + " Path = " + cookie.Path + "\n";

                    }
                }

            }
            return txt1;
        }

        public string dumpAll()
        {
            string outStr = "Headers:\n";
            foreach (httpHeaderX header in headerItems)
            {
                outStr += header.vId + ": " + header.vName + " = " + header.vValue + "\n";
            }

            outStr += "\n\n";
            outStr += "COOKIE LIST CONTAINS:\n";
            foreach (webCookieX co in cookieItems)
            {
                outStr += co.vId + ": " + co.vName + " = " + co.vValue + " (" + co.vDomain + ")\n";
            }

            outStr += "myCookieContainer.GetCookies:\n" + dumpCookies();

            return outStr;
        }

    }

}
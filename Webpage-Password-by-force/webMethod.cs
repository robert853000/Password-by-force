using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Webpage_Password_by_force
{
    class webMethod
    {
        public bool data()
        {
            return true;
        }
        public bool get()
        {
            /*
             * 
             *  HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            req.Method = "GET";
            req.CookieContainer = cookies;
            req.UserAgent = "";
            req.Referer = referer;

            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
            cookies.Add(resp.Cookies);

            string pageSrc;
            using (StreamReader sr = new StreamReader(resp.GetResponseStream()))
            {
                pageSrc = sr.ReadToEnd();
            }
            return pageSrc;
             * */
            return true;
        }

        public bool post(Uri url, string postData, string referer, CookieContainer cookies, ref string pageSrc)
        {
            //string key = "Invalid credentials or user not activated!";
            string key = "failure";

            HttpWebRequest wr = (HttpWebRequest)WebRequest.Create(url);
            wr.Timeout = 200000;
            wr.Method = "POST";
            wr.CookieContainer = cookies;
            wr.AutomaticDecompression = DecompressionMethods.GZip;
            // wr.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/67.0.3396.99 Safari/537.36";
            wr.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:95.0) Gecko/20100101 Firefox/95.0";
            wr.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
            //wr.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8";
            wr.Accept = "*/*";

            // wr.CookieContainer = new CookieContainer();
            // wr.CookieContainer.Add(new Cookie("PHPSESSID", "u3ilo4qnvau5cfr5qsqet16o1b"));
            wr.Referer = referer;
            wr.Host = url.Host;
            // wr.Headers.Add("Origin", url.Host);
            //wr.Headers.Add("Accept-Encoding", "gzip, deflate, br");
            //wr.Headers.Add("Accept-Language", "en-US,en;q=0.5");
           // wr.KeepAlive = true;
           // wr.Credentials = System.Net.CredentialCache.DefaultCredentials;
            /*  wr.Headers.Add("X-Requested-With", "XMLHttpRequest");
             wr.Headers.Add("TE", "trailers");
            wr.Headers.Add("Sec-Fetch-Dest", "empty");
             wr.Headers.Add("Sec-Fetch-Mode", "cors");
             wr.Headers.Add("Sec-Fetch-Site", "same-origin");*/


            //byte[] postBytes = Encoding.ASCII.GetBytes(postData);
            byte[] postBytes = Encoding.UTF8.GetBytes(postData);
            wr.ContentLength = postBytes.Length;

            Stream postStream = wr.GetRequestStream();
         
            postStream.Write(postBytes, 0, postBytes.Length);
            postStream.Dispose();
            HttpWebResponse resp= null;
            try
            {
                resp = (HttpWebResponse)wr.GetResponse();

                //WebHeaderCollection header = resp.Headers;
                cookies.Add(resp.Cookies);

                StreamReader sr = new StreamReader(resp.GetResponseStream()/*, System.Text.Encoding.GetEncoding("utf-8")*/);
                pageSrc = "Original url:" + url.OriginalString + "\n\n\n" + "Host:" + url.Host + "\n\n\n"+wr.Headers + "\n\n\n" + postData + "\n\n\n" + resp.Headers+ "\n\n\nResponse Status Code is : " + resp.StatusCode + ":" + resp.StatusDescription + " \n " + sr.ReadToEnd();
                sr.Dispose();
                /*
                if (resp.StatusCode != HttpStatusCode.OK)
                {
                    pageSrc = "login failed";
                    return false;
                }
                */
                return (!pageSrc.Contains(key));
                //return (pageSrc.Contains(key));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                if (resp != null)
                {
                    resp.Close();
                    resp = null;
                }
            }
            finally
            {
                wr = null;
            }
            return false;

        }
    }
}

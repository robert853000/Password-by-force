using System;
using System.IO;
using System.Net;
using System.Text;
using System.Windows;

namespace Webpage_Password_by_force
{
    class webMethod
    {
        public event EventHandler DataReceivedHandler = null;

        public bool data()
        {
            return true;
        }

        public bool get( httpMetaData fakeRequest, CookieContainer cookies, ref string pageSrc, ref string _reqInfo)
        {
            
            Uri url = fakeRequest.getURL();
            //msgBox(url.OriginalString);
            HttpWebRequest wr = (HttpWebRequest)WebRequest.Create(url);
            fakeRequest.addRequestHeaders(wr);
            wr.UseDefaultCredentials = true;
            wr.Timeout = fakeRequest.timeout;
            wr.Method = "GET";
            wr.CookieContainer = cookies;
            wr.AutomaticDecompression = DecompressionMethods.GZip;
            wr.Host = url.Host;
            HttpWebResponse resp = null;
            try
            {
                resp = (HttpWebResponse)wr.GetResponse();
                fakeRequest.onCookiesCollected(resp.Cookies);
                StreamReader sr = new StreamReader(resp.GetResponseStream() /*, System.Text.Encoding.GetEncoding("utf-8")*/ );

                // Request Headers + Post Data
                _reqInfo += fakeRequest.postMethod + " " + url.OriginalString + "\n\n" + wr.Headers + "\n\n\n";

                // Response Headers 
                _reqInfo += "Response Status Code:" + resp.StatusCode + ": " + resp.StatusDescription + "\n\n" + resp.Headers + "\n\n";


                string pageSrc2 = sr.ReadToEnd();
                // Page Src
                _reqInfo += pageSrc2 + "\n\n-----------------------------------------------------------\n\n";

                sr.Dispose();
                if (resp != null)
                {
                    resp.Close();
                    resp = null;
                }
                pageSrc += pageSrc2;
                onDataReceived();
                return true;
            }
            catch (WebException webException)
            {
                msgBox("WebException:" + webException.Message);
            }
            if (resp != null)
            {
                resp.Close();
                resp = null;
            }
            wr = null;
            return false;
        }

        private void onDataReceived()
        {
            if ( /*response.IsSuccessStatusCode &&*/ DataReceivedHandler != null)
            {
                string responseBodyAsText55 = "Text";
                DataReceivedHandler(this, new ResponseData
                {
                    Data = responseBodyAsText55
                });
            }

        }
        public bool post(/*Uri url,*/ httpMetaData fakeRequest/*, string postData, string referer,*/, CookieContainer cookies, ref string pageSrc, ref string _reqInfo, string REST)
        {

            Uri url = fakeRequest.getURL();
            HttpWebRequest wr = (HttpWebRequest)WebRequest.Create(url);
            fakeRequest.addRequestHeaders(wr);
            wr.Host = url.Host;
            switch (fakeRequest.postMethod)
            {
                case "GET":
                    msgBox("Cannot send content body");
                    return false;
                case "POST":
                    wr.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
                    break;
            }
            // if REST wr.ContentType = "text/plain";

            string postData;
            if (REST != null && REST.Length > 0)
            {
                wr.ContentType = "text/plain";
                postData = fakeRequest.injectCustomValues(REST,false);
            }else if (fakeRequest.postItems != null && fakeRequest.postItems.Count > 0)
            {
                postData = "";
                fakeRequest.addPostData(ref postData);
            }else
            {
                postData = "";
            }
            
            wr.UseDefaultCredentials = true;
            wr.Timeout = fakeRequest.timeout;
            wr.Method = fakeRequest.postMethod;
            wr.CookieContainer = cookies;
            wr.AutomaticDecompression = DecompressionMethods.GZip;
            // wr.Credentials = System.Net.CredentialCache.DefaultCredentials;
            //byte[] postBytes = Encoding.ASCII.GetBytes(postData);
            byte[] postBytes = Encoding.UTF8.GetBytes(postData);
            wr.ContentLength = postBytes.Length;
            Stream postStream = wr.GetRequestStream();
            postStream.Write(postBytes, 0, postBytes.Length);
            postStream.Dispose();
            HttpWebResponse resp = null;

            HttpStatusCode statusCode = 0;
            try
            {
                resp = (HttpWebResponse)wr.GetResponse();
                fakeRequest.onCookiesCollected(resp.Cookies);
                StreamReader sr = new StreamReader(resp.GetResponseStream() /*, System.Text.Encoding.GetEncoding("utf-8")*/ );

                // Request Headers + Post Data
                _reqInfo += fakeRequest.postMethod+ " " + url.OriginalString + "\n\n" + wr.Headers + "\n\n" + postData + "\n\n\n";

                // Response Headers 
                _reqInfo += "Response Status Code:" + resp.StatusCode + ": "+resp.StatusDescription +"\n\n" + resp.Headers + "\n\n" ;


                string pageSrc2 = sr.ReadToEnd();
                // Page Src
                _reqInfo += pageSrc2 + "\n\n-----------------------------------------------------------\n\n";

                sr.Dispose();

                if (resp != null)
                {
                    resp.Close();
                    resp = null;
                }
                pageSrc += pageSrc2;

                onDataReceived();
                return true;
            }
            catch (WebException webException)
            {
                /* HttpStatusCode.Forbidden
                //var resp2 = (FtpWebResponse)wr.;
                //var webException = ex.InnerException as WebException;
                //var rawResponse = string.Empty;

                if (webException.Status == WebExceptionStatus.ProtocolError)
                {

                    statusCode = ((HttpWebResponse)webException.Response).StatusCode;
                    using (Stream data = webException.Response.GetResponseStream())
                    using (var reader = new StreamReader(data))
                    {
                        errorMsg += "Request Headers:\n";
                        errorMsg += url.OriginalString + "\n" + wr.Headers + "\n";

                        errorMsg += "Status Code: " + statusCode.ToString() + "\n";
                        errorMsg += "Status Description: " + ((HttpWebResponse)webException.Response).StatusDescription + "\n";
                        errorMsg += "Response Headers:\n";
                        errorMsg += ((HttpWebResponse)webException.Response).Headers + "\n";
                        errorMsg += "Response:\n";
                        errorMsg += reader.ReadToEnd() + "\n";
                    }

                    switch (statusCode)
                    {
                        case HttpStatusCode.Forbidden:
                            //pageSrc = "403" + ((HttpWebResponse)webException.Response).Headers.ToString();
                            //pageSrc = postData;

                            //HttpWebResponse resp2 = (HttpWebResponse)wr.GetResponse();
                            //StreamReader sr = new StreamReader(resp.GetResponseStream());
                            //pageSrc = sr.ReadToEnd(); 
                            break;
                        default:
                            //pageSrc = ((HttpWebResponse)webException.Response).StatusCode.ToString();
                            break;
                    }

                }*/

              // pageSrc += errorMsg;

                //var webException = exception.InnerException as WebException;
                //string rawResponse = "";
                /*
                 * vresp = (HttpWebResponse)wr.GetResponse();

                //WebHeaderCollection header = resp.Headers;
                cookies.Add(resp.Cookies);

                StreamReader sr = new StreamReader(resp.GetResponseStream());*/
                /*
                var alreadyClosedStream = webException.Response.GetResponseStream() as MemoryStream;
                using (var brandNewStream = new MemoryStream(alreadyClosedStream.ToArray()))
                using (var reader = new StreamReader(brandNewStream))
                    rawResponse = reader.ReadToEnd();

                pageSrc = rawResponse;
                */
                // pageSrc = webException.Response.GetResponseStream().sta;
                // if ((int)resp2.StatusCode ==403)

                // {
                //     pageSrc = "403";
                // }else
                //  {
                //     MessageBox.Show(ex.ToString());
                //  }

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

        void msgBox(string text)
        {
            string messageBoxText = text;
            string caption = "?";
            MessageBoxButton button = MessageBoxButton.OK;
            MessageBoxImage icon = MessageBoxImage.Warning;
            MessageBoxResult result;

            result = MessageBox.Show(messageBoxText, caption, button, icon, MessageBoxResult.Yes);
        }
    }
}
using LiraTaxi.Api.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;

namespace LiraTaxi.Api.Code
{
    public static class AuthToken
    {
        public static string CONTENT_TYPE = @"application/x-www-form-urlencoded";
        public static string POST_METHOD = "POST";
        public static string GET_METHOD = "GET";
        public static string PUT_METHOD = "PUT";

        public static AccessToken PhysmodoToken;
        public static string physmodoAccessToken;

        public static string PhysmodoURL = @ConfigurationManager.AppSettings["AppDomain"];

        public static bool connectHTTP()
        {
            bool rtnFlag = false;

            var tokenUrl = PhysmodoURL + "token";
            var userName = "webdev@physmodo.com";
            var userPassword = "PhysmodoDev123#";
            // var userPassword = "Neetu1234#";  
            var request = string.Format("grant_type=password&username={0}&password={1}", HttpUtility.UrlEncode(userName), HttpUtility.UrlEncode(userPassword));
            PhysmodoToken = HttpPost(tokenUrl, request);
            if (PhysmodoToken != null)
            {
                physmodoAccessToken = PhysmodoToken.access_token;
                //HttpContext.Current.Session["accessToken"] = physmodoAccessToken;
                rtnFlag = true;
                Console.WriteLine("Sucessful log into physmodo web site with " + userName);
            }
            else
            {
                physmodoAccessToken = null;
            }
            return rtnFlag;
        }
        public static AccessToken GetLoingInfo(LoginModel model)
        {
            var tokenUrl = PhysmodoURL + "token";
            var userName = model.UserName;
            var userPassword = model.Password;
            var request = string.Format("grant_type=password&username={0}&password={1}", HttpUtility.UrlEncode(userName), HttpUtility.UrlEncode(userPassword));
            PhysmodoToken = HttpPost(tokenUrl, request);
            return PhysmodoToken;
        }
        public static AccessToken HttpPost(string tokenUrl, string requestDetails)
        {
            AccessToken token = null;

            try
            {
                WebRequest webRequest = WebRequest.Create(tokenUrl);
                webRequest.ContentType = CONTENT_TYPE;
                webRequest.Method = POST_METHOD;
                byte[] bytes = Encoding.ASCII.GetBytes(requestDetails);
                webRequest.ContentLength = bytes.Length;
                using (Stream outputStream = webRequest.GetRequestStream())
                {
                    outputStream.Write(bytes, 0, bytes.Length);
                }
                using (WebResponse webResponse = webRequest.GetResponse())
                {
                    StreamReader newstreamreader = new StreamReader(webResponse.GetResponseStream());
                    string newresponsefromserver = newstreamreader.ReadToEnd();
                    token = new JavaScriptSerializer().Deserialize<AccessToken>(newresponsefromserver);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                token = null;
            }

            return token;
        }


    }

    public class AccessToken
    {
        public string access_token { get; set; }

        public string token_type { get; set; }

        public string expires_in { get; set; }

        public string userName { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Configuration;

namespace M183_Blog.Services
{

    public class NexmoService
    {
        private static readonly HttpClient client = new HttpClient();
        public string SendSMS(int secret, string phonenumber)
        {
            string apiKey = WebConfigurationManager.AppSettings["Nexmo_Api_Key"];
            string apiSecret = WebConfigurationManager.AppSettings["Nexmo_Api_Secret"];
            var request = (HttpWebRequest)WebRequest.Create("https://rest.nexmo.com/sms/json");

            string postData = "api_key=" + apiKey;
            postData += "&api_secret=" + apiSecret;
            postData += "&to=" + phonenumber;
            postData += "&from=\"\"M183 Blog\"\"";
            postData += "&text=\"Token: " + secret + "\" ";
            var data = Encoding.ASCII.GetBytes(postData);

            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = data.Length;

            using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }

            var response = (HttpWebResponse)request.GetResponse();

            return new StreamReader(response.GetResponseStream()).ReadToEnd();
        }
    }
}
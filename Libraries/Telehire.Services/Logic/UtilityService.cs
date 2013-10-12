using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Xml.Serialization;
using Telehire.Services.Abstract;

namespace Telehire.Services.Logic
{
    public class UtilityService : IUtilityService
    {

        private readonly HttpContextBase _httpContextBase;
        public UtilityService(HttpContextBase httpContextBase)
        {
            this._httpContextBase = httpContextBase;
        }

        public string ServiceUrl()
        {
            try
            {

                string service = WebConfigurationManager.AppSettings["ServiceURL"];
                return string.IsNullOrEmpty(service) ? "http://62.173.42.46/AppForms/Services" : service;
            }
            catch
            {
                return "http://62.173.42.46/AppForms/Services";
            }

        }
        public string GenerateRandomDigitCode(int length)
        {
            var random = new Random();
            string str = string.Empty;
            for (int i = 0; i < length; i++)
                str = String.Concat(str, random.Next(10).ToString());
            return str;
        }


        public string SerializeToXML<T>(T obj)
        {
            var msg = "";
            using (MemoryStream stream = new MemoryStream())
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));


                serializer.Serialize(stream, obj);
                var btyeArray = stream.ToArray();
                msg = System.Text.Encoding.Default.GetString(btyeArray);
            }
            return msg;

        }
        public T XMLDeserialize<T>(string data)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(data);
            using (MemoryStream stream = new MemoryStream(buffer))
            {
                try
                {
                    XmlSerializer xs = new XmlSerializer(typeof(T));


                    T obj = (T)xs.Deserialize(stream);

                    stream.Close();

                    return obj;
                }
                catch (Exception ex)
                {
                    return default(T);
                }

            }


        }
        public string RemoteIP
        {
            get
            {
                string msg = "";

                msg = !string.IsNullOrWhiteSpace(_httpContextBase.Request.ServerVariables["REMOTE_ADDR"]) ? _httpContextBase.Request.ServerVariables["REMOTE_ADDR"] : _httpContextBase.Request.ServerVariables["REMOTE_HOST"];
                if (msg == "::1")
                    return this.HostIP;
                return msg;
            }
        }

        public string HostIP
        {
            get { return _httpContextBase.Request.ServerVariables["LOCAL_ADDR"]; }
        }
    }
}

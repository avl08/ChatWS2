using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Web.Script.Serialization;
using UtilitiesChat.Models.WS;

namespace UtilitiesChat
{
    public class RequestUtil
    {
        public Reply oReply { get; set; }
        public RequestUtil()
        {
            oReply = new Reply();
        }

        public Reply Execute<T>(string url, string method, T objectRequest)
        {
            oReply.result = 0;
            string result = "";
            try
            {
                JavaScriptSerializer js = new JavaScriptSerializer();
                string json = JsonConvert.SerializeObject(objectRequest);
                WebRequest request = WebRequest.Create(url);
                request.Method = method;
                request.PreAuthenticate = true;
                request.ContentType = "application/json;charset=utf-8";
                request.Timeout = 6000;

                using (var oStreamWriter = new StreamWriter(request.GetRequestStream()))
                {
                    oStreamWriter.Write(json);
                    oStreamWriter.Flush();
                }
                var oHttpResponse = (HttpWebResponse)request.GetResponse();
                using (var oStreamReader = new StreamReader(oHttpResponse.GetResponseStream()))
                {
                    result = oStreamReader.ReadToEnd();
                }
                oReply = JsonConvert.DeserializeObject<Reply>(result);
            }
            catch (TimeoutException e)
            {
                oReply.message = "Servidor sin respuesta";
            }
            catch (Exception e)
            {
                oReply.result = 0;
                oReply.message = "Ocurrio un error";
            }
            return oReply;
        }
    }
}

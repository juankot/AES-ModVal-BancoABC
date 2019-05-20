using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using AES.Dispatcher.Models;
using AES.Domain;
using Newtonsoft.Json;

namespace AES.ExternalAgents.ServiceClient
{
    public class ServiceClient : IServiceClient
    {
        private readonly string mediaTypeJSON = @"application/json";

        public async Task<String> CallClientAsync(Routing route, string message)
        {
            string result = String.Empty;
            switch (route.Type.ToString())
            {
                case "SOAP":
                    result = await CallServiceSOAPAsync(route, message);
                    break;
                case "REST":
                    result = await CallServiceRESTAsync(route, message);
                    break;
            }
            return result;
        }

        private async Task<String> CallServiceRESTAsync(Routing route, String requestBodyObject)
        {
            // Initialize an HttpWebRequest for the current URL.
            var webReq = (HttpWebRequest)WebRequest.Create(route.Endpoint);
            webReq.Method = route.Action;
            webReq.Accept = mediaTypeJSON;


            //Serialize request object as JSON and write to request body
            if (requestBodyObject != null)
            {
                System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();
                Byte[] byteArray = encoding.GetBytes(requestBodyObject);

                webReq.ContentLength = byteArray.Length;
                webReq.ContentType = mediaTypeJSON;

                using (Stream dataStream = webReq.GetRequestStream())
                {
                    dataStream.Write(byteArray, 0, byteArray.Length);
                }
            }

            var response = await webReq.GetResponseAsync();

            if (response == null)
            {
                return default;
            }

            var streamReader = new StreamReader(response.GetResponseStream());

            var responseContent = streamReader.ReadToEnd().Trim();

            var jsonObject = JsonConvert.DeserializeObject(responseContent);

            return jsonObject.ToString();
        }


        private Task<String> CallServiceSOAPAsync(Routing route, String requestBodyObject)
        {

            XmlDocument soapEnvelopeXml = new XmlDocument();
            soapEnvelopeXml.LoadXml(requestBodyObject);

            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(route.Endpoint);
            webRequest.Headers.Add("SOAPAction", route.Action);
            webRequest.ContentType = "text/xml;charset=\"utf-8\"";
            webRequest.Accept = "text/xml";
            webRequest.Method = "POST";

            using (Stream stream = webRequest.GetRequestStream())
            {
                soapEnvelopeXml.Save(stream);
            }

            // begin async call to web request.
            IAsyncResult asyncResult = webRequest.BeginGetResponse(null, null);

            // suspend this thread until call is complete. You might want to
            // do something usefull here like update your UI.
            asyncResult.AsyncWaitHandle.WaitOne();

            // get the response from the completed web request.
            String soapResult;

            using (WebResponse webResponse = webRequest.EndGetResponse(asyncResult))
            {
                using (StreamReader rd = new StreamReader(webResponse.GetResponseStream()))
                {
                    soapResult = rd.ReadToEnd();
                }
            }
            return Task.FromResult<String>(soapResult);
        }

    }
}

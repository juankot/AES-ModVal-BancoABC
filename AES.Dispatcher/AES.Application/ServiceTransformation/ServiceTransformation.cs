using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Xsl;
using AES.Dispatcher.Models;
using AES.Domain;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AES.Aplication.ServiceTransformation
{
    public class ServiceTransformation : IServiceTransformation
    {

        #region Request
        public async Task<String> GetTransformationRequest(Routing route, string numeroReferencia, string valorAPagar = "")
        {
            string result = null;
            switch (route.Type)
            {
                case "REST":
                    result = await GetTransformationRESTRequest(route, numeroReferencia, valorAPagar);
                    break;
                case "SOAP":
                    result = await GetTransformationSOAPRequest(route, numeroReferencia, valorAPagar);
                    break;
            }

            return result;
        }

        private Task<String> GetTransformationRESTRequest(Routing route, string numeroReferencia, string valorAPagar = "")
        {
            String request = string.Empty;
            Operations operation = Operations.unknown;

            Enum.TryParse<Operations>(route.Provider, out operation);

            switch (operation)
            {
                case Operations.Consultar:
                    route.Endpoint = String.Format(route.Endpoint, numeroReferencia);
                    request = null;
                    break;
                case Operations.Pagar:
                    route.Endpoint = String.Format(route.Endpoint, numeroReferencia);
                    request = route.XSLTRequest.Replace("{0}", valorAPagar);
                    break;
                case Operations.Compensar:
                    route.Endpoint = String.Format(route.Endpoint, numeroReferencia);
                    request = route.XSLTRequest.Replace("{0}", valorAPagar);
                    break;
            }
            return Task.FromResult<String>(request);
        }

        private Task<String> GetTransformationSOAPRequest(Routing route, string numeroReferencia, string valorAPagar = "")
        {
            String request = string.Empty;
            Operations operation = Operations.unknown;

            Enum.TryParse<Operations>(route.Provider, out operation);

            switch (operation)
            {
                case Operations.Consultar:
                    request = route.XSLTRequest.Replace("{0}", numeroReferencia); ;
                    break;
                case Operations.Pagar:
                    request = route.XSLTRequest.Replace("{0}", numeroReferencia).Replace("{1}", valorAPagar);
                    break;
                case Operations.Compensar:
                    request = route.XSLTRequest.Replace("{0}", numeroReferencia).Replace("{1}", valorAPagar);
                    break;
            }
            return Task.FromResult<String>(request);
        }
        #endregion Request

        #region Query response
        public async Task<MultipleDispatcherConsultarNumeroReferenciaGet> GetTransformationQueryResponse(Routing route, string response)
        {
            MultipleDispatcherConsultarNumeroReferenciaGet result = null;
            switch (route.Type)
            {
                case "REST":
                    result = await GetTransformationQueryRESTResponse(route.XSLTResponse, response);
                    break;
                case "SOAP":
                    result = await GetTransformationQuerySOAPesponse(route, response);
                    break;
            }
            return result;
        }

        private Task<MultipleDispatcherConsultarNumeroReferenciaGet> GetTransformationQueryRESTResponse(string response, string template)
        {
            Dictionary<string, string> keys = JsonConvert.DeserializeObject<Dictionary<string, string>>(template);
            JObject responseObject = JObject.Parse(response);

            MultipleDispatcherConsultarNumeroReferenciaGet result;

            string valor = string.Empty, resultado = string.Empty;
            keys.TryGetValue(nameof(ValorPagar).ToString(), out valor);
            keys.TryGetValue(nameof(AES.Dispatcher.Models.Resultado).ToString(), out resultado);

            if (responseObject.Count == 2)
            {
                var valorPagar = new ValorPagar()
                {
                    Ipvalorpagar = Convert.ToDecimal(responseObject[$"{valor}"])
                };
                result = new MultipleDispatcherConsultarNumeroReferenciaGet()
                {
                    ValorPagar = valorPagar,
                    Resultado = new Dispatcher.Models.Resultado() { Descripcion = "Ok" }
                };
            }
            else
            {
                result = new MultipleDispatcherConsultarNumeroReferenciaGet()
                {
                    ValorPagar = null,
                    Resultado = new Dispatcher.Models.Resultado() { Descripcion = responseObject[$"{resultado}"].ToString() }
                };
            }

            return Task.FromResult<MultipleDispatcherConsultarNumeroReferenciaGet>(result);
        }

        private Task<MultipleDispatcherConsultarNumeroReferenciaGet> GetTransformationQuerySOAPesponse(Routing route, string response)
        {
            MultipleDispatcherConsultarNumeroReferenciaGet result = null;
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(response);

            var node = xmlDoc.GetElementsByTagName(route.XSLTResponse);


            var valorPagar = new ValorPagar()
            {
                Ipvalorpagar = Convert.ToDecimal(node.Item(0).InnerText)
            };
            result = new MultipleDispatcherConsultarNumeroReferenciaGet()
            {
                ValorPagar = valorPagar,
                Resultado = new Dispatcher.Models.Resultado() { Descripcion = "Ok" }
            };

            return Task.FromResult<MultipleDispatcherConsultarNumeroReferenciaGet>(result);
        }
        #endregion Query response

        #region Pay Response
        public async Task<MultipleDispatcherPagarPost> GetTransformationPayResponse(Routing route, string response)
        {
            MultipleDispatcherPagarPost result = null;
            switch (route.Type)
            {
                case "REST":
                    result = await GetTransformationPayRESTResponse(response, route.XSLTResponse);
                    break;
                case "SOAP":
                    result = await GetTransformationPaySOAPResponse(route, response);
                    break;
            }
            return result;
        }

        private Task<MultipleDispatcherPagarPost> GetTransformationPaySOAPResponse(Routing route, string response)
        {
            MultipleDispatcherPagarPost result = null;
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(response);

            var node = xmlDoc.GetElementsByTagName(route.XSLTResponse);

            result = new MultipleDispatcherPagarPost()
            {
                Resultado = new Dispatcher.Models.Resultado() { Descripcion = node.Item(0).InnerText },
                Resultado404 = null
            };

            return Task.FromResult<MultipleDispatcherPagarPost>(result);
        }
        private Task<MultipleDispatcherPagarPost> GetTransformationPayRESTResponse(string response, string template)
        {
            Dictionary<string, string> keys = JsonConvert.DeserializeObject<Dictionary<string, string>>(template);
            JObject responseObject = JObject.Parse(response);

            MultipleDispatcherPagarPost result;

            string resultado = string.Empty;
            keys.TryGetValue(nameof(AES.Dispatcher.Models.Resultado).ToString(), out resultado);

            if (responseObject.Count == 2)
            {

                result = new MultipleDispatcherPagarPost()
                {
                    Resultado = new Dispatcher.Models.Resultado() { Descripcion = responseObject[$"{resultado}"].ToString() },
                    Resultado404 = null
                };
            }
            else
            {
                result = new MultipleDispatcherPagarPost()
                {
                    Resultado = new Dispatcher.Models.Resultado() { Descripcion = "404" },
                    Resultado404 = new Dispatcher.Models.Resultado() { Descripcion = responseObject[$"{resultado}"].ToString() }
                };
            }

            return Task.FromResult<MultipleDispatcherPagarPost>(result);
        }

        #endregion Pay Response

        #region Conpensate Response

        public async Task<MultipleDispatcherCompensarPost> GetTransformationCompensateResponse(Routing route, string response)
        {
            MultipleDispatcherCompensarPost result = null;
            switch (route.Type)
            {
                case "REST":
                    result = await GetTransformationCompensateRESTResponse(response, route.XSLTResponse);
                    break;
                case "SOAP":
                    result = await GetTransformationCompensateSOAPResponse(route, response);
                    break;
            }
            return result;
        }
        private Task<MultipleDispatcherCompensarPost> GetTransformationCompensateRESTResponse(string response, string template)
        {
            Dictionary<string, string> keys = JsonConvert.DeserializeObject<Dictionary<string, string>>(template);
            JObject responseObject = JObject.Parse(response);

            MultipleDispatcherCompensarPost result;

            string resultado = string.Empty;
            keys.TryGetValue(nameof(AES.Dispatcher.Models.Resultado).ToString(), out resultado);

            if (responseObject.Count == 2)
            {

                result = new MultipleDispatcherCompensarPost()
                {
                    Resultado = new Dispatcher.Models.Resultado() { Descripcion = responseObject[$"{resultado}"].ToString() },
                    Resultado404 = null
                };
            }
            else
            {
                result = new MultipleDispatcherCompensarPost()
                {
                    Resultado = null,
                    Resultado404 = new Dispatcher.Models.Resultado() { Descripcion = responseObject[$"{resultado}"].ToString() }
                };
            }

            return Task.FromResult<MultipleDispatcherCompensarPost>(result);
        }
        private Task<MultipleDispatcherCompensarPost> GetTransformationCompensateSOAPResponse(Routing route, string response)
        {
            MultipleDispatcherCompensarPost result = null;
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(response);

            var node = xmlDoc.GetElementsByTagName(route.XSLTResponse);

            result = new MultipleDispatcherCompensarPost()
            {
                Resultado = new Dispatcher.Models.Resultado() { Descripcion = node.Item(0).InnerText },
                Resultado404 = null
            };

            return Task.FromResult<MultipleDispatcherCompensarPost>(result);
        }

        #endregion


    }
}

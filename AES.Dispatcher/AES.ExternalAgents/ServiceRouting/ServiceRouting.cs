using AES.Domain;
using AES.ExternalAgents.IntermediateRouting;
using AES.ExternalAgents.IntermediateRouting.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace AES.ExternalAgents.ServiceRouting
{
    public class ServiceRouting : RouteOperationNumeroReferencia, IServiceRouting
    {
        public ServiceRouting() : base(new IntermediateRoutingClient(ConfigurationManager.AppSettings["UrlIntermediateRouting"]))
        {
        }

        public async Task<Routing> GetRoute(string operation, string numeroReferencia)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["UrlIntermediateRouting"]);

                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(@"application/json"));

                var url = $"{ConfigurationManager.AppSettings["UrlIntermediateRouting"]}?operation={operation}&numeroReferencia={numeroReferencia}";
                using (HttpResponseMessage response = await client.GetAsync(url))
                {
                    var res = await response.Content.ReadAsStringAsync();
                    var jo = JObject.Parse(res);
                    var routing = jo["routing"];

                    Routing result = routing.ToObject<Routing>();
                    result.XSLTRequest = result.XSLTRequest.Replace(@"\", string.Empty);
                    result.XSLTResponse = result.XSLTResponse.Replace(@"\", string.Empty);
                    return result;
                }
            }

            //    var result = await this.Get(operation, numeroReferencia);
            //    return result.Content.Routing;
        }

        //public Task<Routing> GetRoute(string operation, string numeroReferencia)
        //{
        //    Routing result = null;
        //    switch (operation)
        //    {
        //        case "Consultar":
        //            result = new Routing()
        //            {
        //                Action = "GET",
        //                Endpoint = "http://ec2-3-215-174-202.compute-1.amazonaws.com:9090/servicios/pagos/v1/payments/{0}",
        //                Provider = "Consultar",
        //                XSLTRequest = null,
        //                XSLTResponse = @"{""ValorPagar"":""valorFactura"",""Resultado"":""valorFactura""}"
        //            };
        //            break;

        //        case "Pagar":
        //            result = new Routing()
        //            {
        //                Action = "POST",
        //                Endpoint = "http://ec2-3-215-174-202.compute-1.amazonaws.com:9090/servicios/pagos/v1/payments/{0}",
        //                Provider = "Pagar",
        //                XSLTRequest = @"{""valorFactura"":""{0}""}",
        //                XSLTResponse = @"{""Resultado"":""mensaje""}"
        //            };
        //            break;
        //        case "Compensar":
        //            result = new Routing()
        //            {
        //                Action = "DELETE",
        //                Endpoint = "http://ec2-3-215-174-202.compute-1.amazonaws.com:9090/servicios/pagos/v1/payments/{0}",
        //                Provider = "Compensar",
        //                XSLTRequest = @"{""valorFactura"":""{0}""}",
        //                XSLTResponse = @"{""Resultado"":""mensaje""}"
        //            };
        //            break;
        //    }

        //    //var result = await this.Get(operation, numeroReferencia);
        //    return Task.FromResult<Routing>(result);
        //}

        /// <summary>
        /// SOAP
        /// </summary>
        /// <param name="operation"></param>
        /// <param name="numeroReferencia"></param>
        /// <returns></returns>
        //public Task<Routing> GetRoute(string operation, string numeroReferencia)
        //{
        //    Routing result = null;
        //    switch (operation)
        //    {
        //        case "Consultar":
        //            result = new Routing()
        //            {
        //                Action = "Consultar",
        //                Endpoint = "http://ec2-54-236-241-13.compute-1.amazonaws.com:8080/gas-service/PagosService?wsdl",
        //                Provider = "Consultar",
        //                Type = "SOAP",
        //                XSLTRequest = "<soapenv:Envelope xmlns:soapenv=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:sch=\"http://www.servicios.co/pagos/schemas\">   <soapenv:Header/>   <soapenv:Body>      <sch:ReferenciaFactura>         <sch:referenciaFactura>{0}</sch:referenciaFactura>      </sch:ReferenciaFactura>   </soapenv:Body></soapenv:Envelope>",
        //                XSLTResponse = "totalPagar"
        //            };
        //            break;

        //        case "Pagar":
        //            result = new Routing()
        //            {
        //                Action = "pagar",
        //                Endpoint = "http://ec2-54-236-241-13.compute-1.amazonaws.com:8080/gas-service/PagosService?wsdl",
        //                Provider = "Pagar",
        //                Type = "SOAP",
        //                XSLTRequest = "<soapenv:Envelope xmlns:soapenv=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:sch=\"http://www.servicios.co/pagos/schemas\">   <soapenv:Header/>   <soapenv:Body>      <sch:PagoResource>         <sch:referenciaFactura>            <sch:referenciaFactura>{0}</sch:referenciaFactura>         </sch:referenciaFactura>         <sch:totalPagar>{1}</sch:totalPagar>      </sch:PagoResource>   </soapenv:Body></soapenv:Envelope>",
        //                XSLTResponse = "mensaje"
        //            };
        //            break;
        //        case "Compensar":
        //            result = new Routing()
        //            {
        //                Action = "compensar",
        //                Endpoint = "http://ec2-54-236-241-13.compute-1.amazonaws.com:8080/gas-service/PagosService?wsdl",
        //                Provider = "Compensar",
        //                Type = "SOAP",
        //                XSLTRequest = "<soapenv:Envelope xmlns:soapenv=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:sch=\"http://www.servicios.co/pagos/schemas\">   <soapenv:Header/>   <soapenv:Body>      <sch:PagoResource>         <sch:referenciaFactura>            <sch:referenciaFactura>{0}</sch:referenciaFactura>         </sch:referenciaFactura>         <sch:totalPagar>{1}</sch:totalPagar>      </sch:PagoResource>   </soapenv:Body></soapenv:Envelope>",
        //                XSLTResponse = "mensaje"
        //            };
        //            break;
        //    }

        //    //var result = await this.Get(operation, numeroReferencia);
        //    return Task.FromResult<Routing>(result);
    //}
    }
}

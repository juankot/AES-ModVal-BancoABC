// Template: Client Proxy T4 Template (RAMLClient.t4) version 5.0

using System;
using System.Collections;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using RAML.Api.Core;
using Raml.Common;
using AES.ExternalAgents.IntermediateRouting.Models;
using AES.Domain;
using System.Collections.Generic;

namespace AES.ExternalAgents.IntermediateRouting
{
    public partial class RouteOperationNumeroReferencia
    {
        private readonly IntermediateRoutingClient proxy;

        internal RouteOperationNumeroReferencia(IntermediateRoutingClient proxy)
        {
            this.proxy = proxy;
        }


        /// <summary>
		/// Retorna el la información de enrutamiento para una operación y número de referencia - /route/{operation}/{numeroReferencia}
		/// </summary>
		/// <param name="operation"></param>
		/// <param name="numeroReferencia"></param>
        public virtual async Task<Models.RouteOperationNumeroReferenciaGetResponse> Get(string operation, string numeroReferencia)
        {

            var url = "?operation={operation}&numeroReferencia={numeroReferencia}";
            url = url.Replace("{operation}", operation.ToString());
            url = url.Replace("{numeroReferencia}", numeroReferencia.ToString());

            url = url.Replace("?&", "?");

            var req = new HttpRequestMessage(HttpMethod.Get, $"{proxy.Client.BaseAddress}{url}");
	        var response = await proxy.Client.SendAsync(req);

            return new Models.RouteOperationNumeroReferenciaGetResponse  
                                            {
                                                RawContent = response.Content,
                                                RawHeaders = response.Headers, 
                                                StatusCode = response.StatusCode,
                                                ReasonPhrase = response.ReasonPhrase,
												SchemaValidation = new Lazy<SchemaValidationResults>(() => new SchemaValidationResults(true), true)
                                            };

        }

        /// <summary>
		/// Retorna el la información de enrutamiento para una operación y número de referencia - /route/{operation}/{numeroReferencia}
		/// </summary>
		/// <param name="request">Models.RouteOperationNumeroReferenciaGetRequest</param>
		/// <param name="responseFormatters">response formatters</param>
        public virtual async Task<Models.RouteOperationNumeroReferenciaGetResponse> Get(Models.RouteOperationNumeroReferenciaGetRequest request, IEnumerable<MediaTypeFormatter> responseFormatters = null)
        {

            var url = "route/{operation}/{numeroReferencia}";
			if(request.UriParameters == null)
				throw new InvalidOperationException("Uri Parameters cannot be null");               

			if(request.UriParameters.Operation == null)
				throw new InvalidOperationException("Uri Parameter Operation cannot be null");

            url = url.Replace("{operation}", request.UriParameters.Operation.ToString());

			if(request.UriParameters.NumeroReferencia == null)
				throw new InvalidOperationException("Uri Parameter NumeroReferencia cannot be null");

            url = url.Replace("{numeroReferencia}", request.UriParameters.NumeroReferencia.ToString());

            url = url.Replace("?&", "?");

            var req = new HttpRequestMessage(HttpMethod.Get, url);

            if(request.RawHeaders != null)
            {
                foreach(var header in request.RawHeaders)
                {
                    req.Headers.TryAddWithoutValidation(header.Key, string.Join(",", header.Value));
                }
            }
	        var response = await proxy.Client.SendAsync(req);
            return new Models.RouteOperationNumeroReferenciaGetResponse  
                                            {
                                                RawContent = response.Content,
                                                RawHeaders = response.Headers,
	                                            Formatters = responseFormatters,
                                                StatusCode = response.StatusCode,
                                                ReasonPhrase = response.ReasonPhrase,
												SchemaValidation = new Lazy<SchemaValidationResults>(() => new SchemaValidationResults(true), true)
                                            };
        }

    }

    /// <summary>
    /// Main class for grouping root resources. Nested resources are defined as properties. The constructor can optionally receive an URL and HttpClient instance to override the default ones.
    /// </summary>
    public partial class IntermediateRoutingClient
    {

		public SchemaValidationSettings SchemaValidation { get; private set; } 

        protected readonly HttpClient client;
        public const string BaseUri = "http://localhost:8080/{version}/";

        internal HttpClient Client { get { return client; } }




        public IntermediateRoutingClient(string endpointUrl)
        {
            SchemaValidation = new SchemaValidationSettings
			{
				Enabled = true,
				RaiseExceptions = true
			};

			if(string.IsNullOrWhiteSpace(endpointUrl))
                throw new ArgumentException("You must specify the endpoint URL", "endpointUrl");

			if (endpointUrl.Contains("{"))
			{
				var regex = new Regex(@"\{([^\}]+)\}");
				var matches = regex.Matches(endpointUrl);
				var parameters = new List<string>();
				foreach (Match match in matches)
				{
					parameters.Add(match.Groups[1].Value);
				}
				throw new InvalidOperationException("Please replace parameter/s " + string.Join(", ", parameters) + " in the URL before passing it to the constructor ");
			}

            client = new HttpClient {BaseAddress = new Uri(endpointUrl)};
        }

        public IntermediateRoutingClient(HttpClient httpClient)
        {
            if(httpClient.BaseAddress == null)
                throw new InvalidOperationException("You must set the BaseAddress property of the HttpClient instance");

            client = httpClient;

			SchemaValidation = new SchemaValidationSettings
			{
				Enabled = true,
				RaiseExceptions = true
			};
        }

        

        public virtual RouteOperationNumeroReferencia RouteOperationNumeroReferencia
        {
            get { return new RouteOperationNumeroReferencia(this); }
        }
                


		public void AddDefaultRequestHeader(string name, string value)
		{
			client.DefaultRequestHeaders.Add(name, value);
		}

		public void AddDefaultRequestHeader(string name, IEnumerable<string> values)
		{
			client.DefaultRequestHeaders.Add(name, values);
		}


    }

} // end namespace









namespace AES.ExternalAgents.IntermediateRouting.Models
{
    

    /// <summary>
    /// Multiple Response Types Routing, Resultado
    /// </summary>
    public partial class  MultipleRouteOperationNumeroReferenciaGet : ApiMultipleResponse
    {
        static readonly Dictionary<HttpStatusCode, string> schemas = new Dictionary<HttpStatusCode, string>
        {
        };

        public static string GetSchema(HttpStatusCode statusCode)
        {
            return schemas.ContainsKey(statusCode) ? schemas[statusCode] : string.Empty;
        }

        //public MultipleRouteOperationNumeroReferenciaGet()
        //{
        //    names.Add((HttpStatusCode)200, "Routing");
        //    types.Add((HttpStatusCode)200, typeof(Routing));
        //    names.Add((HttpStatusCode)404, "Resultado");
        //    types.Add((HttpStatusCode)404, typeof(Resultado));
        //}

        public Routing Routing { get; set; }


        public Resultado Resultado { get; set; }


    } // end class

    /// <summary>
    /// Uri Parameters for resource /route/{operation}/{numeroReferencia}
    /// </summary>
    public partial class  RouteOperationNumeroReferenciaUriParameters 
    {

		[JsonProperty("operation")]
        public string Operation { get; set; }


		[JsonProperty("numeroReferencia")]
        public string NumeroReferencia { get; set; }


    } // end class

    /// <summary>
    /// Request object for method Get of class RouteOperationNumeroReferencia
    /// </summary>
    public partial class RouteOperationNumeroReferenciaGetRequest : ApiRequest
    {
        public RouteOperationNumeroReferenciaGetRequest(RouteOperationNumeroReferenciaUriParameters UriParameters)
        {
            this.UriParameters = UriParameters;
        }


        /// <summary>
        /// Request Uri Parameters
        /// </summary>
        public RouteOperationNumeroReferenciaUriParameters UriParameters { get; set; }

    } // end class

    /// <summary>
    /// Response object for method Get of class RouteOperationNumeroReferencia
    /// </summary>

    public partial class RouteOperationNumeroReferenciaGetResponse : ApiResponse
    {

	    private MultipleRouteOperationNumeroReferenciaGet typedContent;
        /// <summary>
        /// Typed response content
        /// </summary>
        public MultipleRouteOperationNumeroReferenciaGet Content 
	    {
	        get
	        {
		        if (typedContent != null) 
					return typedContent;

		        typedContent = new MultipleRouteOperationNumeroReferenciaGet();

                IEnumerable<string> values = new List<string>();
                if (RawContent != null && RawContent.Headers != null)
                    RawContent.Headers.TryGetValues("Content-Type", out values);

                if (values.Any(hv => hv.ToLowerInvariant().Contains("xml")) &&
                    !values.Any(hv => hv.ToLowerInvariant().Contains("json")))
                {
                    var task = RawContent.ReadAsStreamAsync();

                    var xmlStream = task.GetAwaiter().GetResult();
                    var content = new XmlSerializer(typedContent.GetTypeByStatusCode(StatusCode)).Deserialize(xmlStream);
                    typedContent.SetPropertyByStatusCode(StatusCode, content);
                }
                else
                {
		            var task = Formatters != null && Formatters.Any() 
                                ? RawContent.ReadAsAsync(typedContent.GetTypeByStatusCode(StatusCode), Formatters).ConfigureAwait(false)
                                : RawContent.ReadAsAsync(typedContent.GetTypeByStatusCode(StatusCode)).ConfigureAwait(false);
		        
		            var content = task.GetAwaiter().GetResult();
                    typedContent.SetPropertyByStatusCode(StatusCode, content);
                }

		        return typedContent;
	        }
    	}  
		
		public static string GetSchema(HttpStatusCode statusCode)
        {
            return MultipleRouteOperationNumeroReferenciaGet.GetSchema(statusCode);
        }      

    } // end class

	
	public enum Type
	{
		[JsonProperty("SOAP")]
		SOAP,
		[JsonProperty("REST")]
		REST
    }


} // end Models namespace
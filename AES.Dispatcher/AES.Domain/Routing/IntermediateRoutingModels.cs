using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AES.Domain
{
	public partial class Routing
	{

		/// <summary>
		/// Nombre del proveedor del convenio.
		/// </summary>
		public string Provider { get; set; }


		/// <summary>
		/// Identificador del convenio (primeros 4 dígitos del número de referencia).
		/// </summary>
		public string Identifier { get; set; }


		//[JsonConverter(typeof(StringEnumConverter))]
		public String Type { get; set; }


		/// <summary>
		/// URL del servicio Web proporcionado por el convenio.
		/// </summary>
		public string Endpoint { get; set; }


		/// <summary>
		/// Método HTTP en caso de ser REST, o nombre de capacidad si es SOAP.
		/// </summary>
		public string Action { get; set; }


		/// <summary>
		/// Transformación de Request.
		/// </summary>
		public string XSLTRequest { get; set; }


		/// <summary>
		/// Transformación de Response.
		/// </summary>
		public string XSLTResponse { get; set; }


	} // end class

	public partial class Resultado
	{

		/// <summary>
		/// Descripción de la respuesta
		/// </summary>
		[JsonProperty("descripcion")]
		public string Descripcion { get; set; }


	} // end class
}

// Template: Models (ApiModel.t4) version 3.0

// This code was generated by RAML Server Scaffolder

using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.ComponentModel.DataAnnotations;

namespace AES.Dispatcher.Models
{
    public partial class ValorPagar
    {
        

        /// <summary>
        /// Valor a pagar, pagado o a compensar.
        /// </summary>
        [Required]
        [Range(0.00,double.MaxValue)]
		[JsonProperty("valorPagar")]
        public decimal Ipvalorpagar { get; set; }
    } // end class

} // end Models namespace


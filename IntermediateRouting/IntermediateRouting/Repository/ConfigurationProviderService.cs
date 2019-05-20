using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntermediateRouting.Repository
{
    public class ConfigurationProviderService
    {
        public int Id { get; set; }
        public string Provider { get; set; }
        public string Identifier { get; set; }
        public short Type { get; set; }
        public string Endpoint { get; set; }
        public string Action { get; set; }
        public string XSLTRequest { get; set; }
        public string XSLTResponse { get; set; }
    }
}

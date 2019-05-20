using AES.Dispatcher.Models;
using AES.Domain;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AES.Aplication.ServiceTransformation
{
    public interface IServiceTransformation
    {
        Task<String> GetTransformationRequest(Routing route, string numeroReferencia, string valorAPagar = "");
        Task<MultipleDispatcherConsultarNumeroReferenciaGet> GetTransformationQueryResponse(Routing route,string response);
        Task<MultipleDispatcherPagarPost> GetTransformationPayResponse(Routing route, string response);
        Task<MultipleDispatcherCompensarPost> GetTransformationCompensateResponse(Routing route, string response);
    }
}

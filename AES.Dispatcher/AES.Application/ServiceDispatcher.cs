using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AES.Dispatcher.Models;
using AES.Domain;
using AES.ExternalAgents.IntermediateRouting;
using AES.ExternalAgents.ServiceRouting;
using AES.Aplication.ServiceTransformation;
using AES.ExternalAgents.ServiceClient;

namespace AES.Aplication
{
    public class ServiceDispatcher : IServiceDispatcher
    {

        public ServiceDispatcher()
        {
            ServiceTransformation = new AES.Aplication.ServiceTransformation.ServiceTransformation();
            ServiceRouting = new ServiceRouting();
            ServiceClient = new ServiceClient();
        }
        private IServiceRouting serviceRouting;
        private IServiceTransformation serviceTransformation;
        private IServiceClient serviceClient;

        private IServiceTransformation ServiceTransformation
        {
            get { return serviceTransformation; }
            set { serviceTransformation = value; }
        }

        private IServiceRouting ServiceRouting
        {
            get { return serviceRouting; }
            set { serviceRouting = value; }
        }

        private IServiceClient ServiceClient
        {
            get { return serviceClient; }
            set { serviceClient = value; }
        }

        public async Task<MultipleDispatcherConsultarNumeroReferenciaGet> Consultar(string numeroReferencia)
        {
            var route = await ServiceRouting.GetRoute(Operations.Consultar.ToString(), numeroReferencia);
            String requestBodyObject = await ServiceTransformation.GetTransformationRequest(route, numeroReferencia);
            String responseBodyObject = await ServiceClient.CallClientAsync(route, requestBodyObject);

            MultipleDispatcherConsultarNumeroReferenciaGet result = await ServiceTransformation.GetTransformationQueryResponse(route, responseBodyObject);
            return result;
        }

        public async Task<MultipleDispatcherPagarPost> Pagar(Pago pago)
        {
            var route = await ServiceRouting.GetRoute(Operations.Pagar.ToString(), pago.NumeroReferencia);
            String requestBodyObject = await ServiceTransformation.GetTransformationRequest(route, pago.NumeroReferencia, pago.ValorPagar.ToString());
            String responseBodyObject = await ServiceClient.CallClientAsync(route, requestBodyObject);

            MultipleDispatcherPagarPost result = await ServiceTransformation.GetTransformationPayResponse(route, responseBodyObject);
            return result;
        }

        public async Task<MultipleDispatcherCompensarPost> Compensar(Pago pago)
        {
            var route = await ServiceRouting.GetRoute(Operations.Compensar.ToString(), pago.NumeroReferencia);
            String requestBodyObject = await ServiceTransformation.GetTransformationRequest(route, pago.NumeroReferencia, pago.ValorPagar.ToString());
            String responseBodyObject = await ServiceClient.CallClientAsync(route, requestBodyObject);

            MultipleDispatcherCompensarPost result = await ServiceTransformation.GetTransformationCompensateResponse(route, responseBodyObject);
            return result;
        }
    }
}

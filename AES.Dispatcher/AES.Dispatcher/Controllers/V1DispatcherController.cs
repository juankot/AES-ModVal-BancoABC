// Template: Controller Implementation (ApiControllerImplementation.t4) version 3.0

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using AES.Aplication;
using AES.Dispatcher.Models;

namespace AES.Dispatcher
{
    public partial class v1DispatcherController : Iv1DispatcherController
    {
        private IServiceDispatcher ServiceDispatcher { get { return new ServiceDispatcher(); } }
        //public v1DispatcherController(IServiceDispatcher serviceDispatcher)
        //{
        //    ServiceDispatcher = serviceDispatcher;
        //}

        /// <summary>
        /// Retorna el valor a pagar asociado a un número de referencia - /consultar/{numeroReferencia}
        /// </summary>
        /// <param name="numeroReferencia"></param>
        /// <returns>MultipleDispatcherConsultarNumeroReferenciaGet</returns>
        public async Task<IHttpActionResult> Get([FromUri] string numeroReferencia)
        {
            var result = await ServiceDispatcher.Consultar(numeroReferencia);
            return Ok(result);
        }

        /// <summary>
        /// /pagar
        /// </summary>
        /// <param name="pago"></param>
        /// <returns>MultipleDispatcherPagarPost</returns>
        public async Task<IHttpActionResult> Post(Models.Pago pago)
        {
            // TODO: implement Post - route: dispatcher/pagar
            // var result = new MultipleDispatcherPagarPost();
            var result = await ServiceDispatcher.Pagar(pago);
            return Ok(result);
        }

        /// <summary>
        /// /compensar
        /// </summary>
        /// <param name="pago"></param>
        /// <returns>MultipleDispatcherCompensarPost</returns>
        public async Task<IHttpActionResult> PostCompensar(Models.Pago pago)
        {
            // TODO: implement PostCompensar - route: dispatcher/compensar
            var result = await ServiceDispatcher.Compensar(pago);
            return Ok(result);
        }

    }
}

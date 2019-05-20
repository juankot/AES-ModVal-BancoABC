// Template: Controller Interface (ApiControllerInterface.t4) version 3.0

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using AES.Dispatcher.Models;


namespace AES.Dispatcher
{
    public interface Iv1DispatcherController
    {

        Task<IHttpActionResult> Get([FromUri] string numeroReferencia);
        Task<IHttpActionResult> Post(Models.Pago pago);
        Task<IHttpActionResult> PostCompensar(Models.Pago pago);
    }
}

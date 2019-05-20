// Template: Controller Implementation (ApiControllerImplementation.t4) version 3.0


using DemoCal.IntermediateRouting.Models;
using IntermediateRouting.Logic;
using IntermediateRouting.Repository;
using Microsoft.AspNetCore.Mvc;

namespace DemoCal.IntermediateRouting
{
    [Route("api/[controller]")]
    [ApiController]
    public class RouteOperationNumeroReferenciaController
    {
        private readonly DbManager _dataAccessProvider;

        public RouteOperationNumeroReferenciaController(DbManager context)
        {
            _dataAccessProvider = context;
        }

        /// <summary>
        /// Retorna el la información de enrutamiento para una operación y número de referencia - /route/{operation}/{numeroReferencia}
        /// </summary>
        /// <param name="operation"></param>
        /// <param name="numeroReferencia"></param>
        /// <returns>MultipleRouteOperationNumeroReferenciaGet</returns>
        public ActionResult<MultipleRouteOperationNumeroReferenciaGet> Get(string operation, string numeroReferencia)
        {
            RouterLogic logic = new RouterLogic(_dataAccessProvider);

            return logic.GetProviderByReferenceAndOperation(numeroReferencia, operation);
        }
    }
}

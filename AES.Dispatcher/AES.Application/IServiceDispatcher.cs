using AES.Dispatcher.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AES.Aplication
{
    public interface IServiceDispatcher
    {
        Task<MultipleDispatcherConsultarNumeroReferenciaGet> Consultar(string numeroReferencia);
        Task<MultipleDispatcherPagarPost> Pagar(Pago pago);
        Task<MultipleDispatcherCompensarPost> Compensar(Pago pago);
    }
}

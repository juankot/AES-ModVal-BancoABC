using AES.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AES.ExternalAgents.ServiceRouting
{
    public interface IServiceRouting
    {
        Task<Routing> GetRoute(string operation, string numeroReferencia);
    }
}

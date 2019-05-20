using AES.Dispatcher.Models;
using AES.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AES.ExternalAgents.ServiceClient
{
    public interface IServiceClient
    {
        Task<String> CallClientAsync(Routing route, string message);
    }
}

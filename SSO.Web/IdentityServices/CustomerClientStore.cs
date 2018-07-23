using IdentityServer4.Models;
using IdentityServer4.Stores;
using Microsoft.Extensions.Options;
using SSO.Services.Interfaces;
using SSO.Web.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuickstartIdentityServer.CustomerChange
{
    public class CustomerClientStore : IClientStore
    {
        private readonly IEnumerable<Client> _clients;
        private readonly IOidcClientService oidcClientService;
        public CustomerClientStore(IEnumerable<Client> clients, IOptionsMonitor<JsonClientOptions> options)
        {
            _clients = clients;
        }
        public Task<Client> FindClientByIdAsync(string clientId)
        {
            return Task.FromResult(_clients.FirstOrDefault(p => p.ClientId == clientId));
        }
    }

}

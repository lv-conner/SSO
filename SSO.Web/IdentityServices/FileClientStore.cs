using IdentityServer4.Models;
using IdentityServer4.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuickstartIdentityServer.CustomerChange
{
    public class FileClientStore : IClientStore
    {
        private readonly IEnumerable<Client> _clients;
        public FileClientStore(IEnumerable<Client> clients)
        {
            _clients = clients;
        }
        public Task<Client> FindClientByIdAsync(string clientId)
        {
            return Task.FromResult(_clients.FirstOrDefault(p => p.ClientId == clientId));
        }
    }
    public class FileClientStoreOptions
    {
        public FileClientStoreOptions()
        {

        }
        public string FileName { get; set; }
    }
}

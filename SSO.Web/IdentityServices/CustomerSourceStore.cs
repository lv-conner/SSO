using IdentityServer4.Models;
using IdentityServer4.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace QuickstartIdentityServer.CustomerChange
{
    public class CustomerSourceStore : IResourceStore
    {
        private readonly IEnumerable<IdentityResource> _identityResources;
        private readonly IEnumerable<ApiResource> _apiResource;
        private readonly Resources _resources;
        public CustomerSourceStore(IEnumerable<IdentityResource> identityResources,IEnumerable<ApiResource> apiResources)
        {
            _identityResources = identityResources;
            _apiResource = apiResources;
            _resources = new Resources(identityResources, apiResources);
        }
        public Task<ApiResource> FindApiResourceAsync(string name)
        {
            return Task.FromResult(_apiResource.First(p => p.Name == name));
        }

        public Task<IEnumerable<ApiResource>> FindApiResourcesByScopeAsync(IEnumerable<string> scopeNames)
        {
            return Task.FromResult(_apiResource.Where(p => scopeNames.Contains(p.Name)));
        }

        public Task<IEnumerable<IdentityResource>> FindIdentityResourcesByScopeAsync(IEnumerable<string> scopeNames)
        {
            return Task.FromResult(_identityResources.Where(p => scopeNames.Contains(p.Name)));
        }

        public Task<Resources> GetAllResourcesAsync()
        {
            return Task.FromResult(_resources);
        }
    }
}

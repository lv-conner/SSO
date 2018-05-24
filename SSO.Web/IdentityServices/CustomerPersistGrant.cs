using IdentityServer4.Models;
using IdentityServer4.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SSO.Web.IdentityServices
{
    /// <summary>
    /// 授权持久化用于，在用户点击记住此次授权后，将此次的授权信息记录下来，以便在下一次使用的时候不需要再次选择相应的权限。
    /// IdentityServer默认是用内存持久化。
    /// </summary>
    public class CustomerPersistGrant : IPersistedGrantStore
    {
        public Task<IEnumerable<PersistedGrant>> GetAllAsync(string subjectId)
        {
            throw new NotImplementedException();
        }

        public Task<PersistedGrant> GetAsync(string key)
        {
            throw new NotImplementedException();
        }

        public Task RemoveAllAsync(string subjectId, string clientId)
        {
            throw new NotImplementedException();
        }

        public Task RemoveAllAsync(string subjectId, string clientId, string type)
        {
            throw new NotImplementedException();
        }

        public Task RemoveAsync(string key)
        {
            throw new NotImplementedException();
        }

        public Task StoreAsync(PersistedGrant grant)
        {
            throw new NotImplementedException();
        }
    }
}

using IdentityServer4.Models;
using IdentityServer4.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace SSO.Web.IdentityServices
{
    /// <summary>
    /// 授权持久化用于，在用户点击记住此次授权后，将此次的授权信息记录下来，以便在下一次使用的时候不需要再次选择相应的权限。
    /// IPersistedGrantStore接口应用在用户登陆成功后的Consent检查。
    /// IdentityServer默认是用内存持久化。
    /// </summary>
    public class CustomerPersistGrant : IPersistedGrantStore
    {
        private readonly ConcurrentDictionary<string, PersistedGrant> _store = new ConcurrentDictionary<string, PersistedGrant>();
        public Task<IEnumerable<PersistedGrant>> GetAllAsync(string subjectId)
        {
            return Task.FromResult(_store.Where(p => p.Value.SubjectId == subjectId).Select(p=>p.Value));
        }

        public Task<PersistedGrant> GetAsync(string key)
        {
            _store.TryGetValue(key, out var persistedGrant);
            return Task.FromResult(persistedGrant);
        }

        public Task RemoveAllAsync(string subjectId, string clientId)
        {
            var list =  _store.Where(p => p.Value.SubjectId == subjectId && p.Value.ClientId == clientId);
            foreach (var item in list)
            {
                _store.TryRemove(item.Key, out var rm);
            }
            return Task.CompletedTask;
        }

        public Task RemoveAllAsync(string subjectId, string clientId, string type)
        {
            var list = _store.Where(p => p.Value.SubjectId == subjectId && p.Value.ClientId == clientId && p.Value.Type == type);

            foreach (var item in list)
            {
                _store.TryRemove(item.Key, out var old);
            }
            return Task.CompletedTask;
        }

        public Task RemoveAsync(string key)
        {
            if (_store.Remove(key, out var old))
            {
                return Task.CompletedTask;
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        public Task StoreAsync(PersistedGrant grant)
        {
            _store.AddOrUpdate(grant.Key, grant,(s,p) =>
            {
                _store.TryRemove(s, out var old);
                return p;
            });
            return Task.CompletedTask;

        }
    }
}
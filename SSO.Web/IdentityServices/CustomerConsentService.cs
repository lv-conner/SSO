using IdentityServer4.Models;
using IdentityServer4.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SSO.Web.IdentityServices
{
    /// <summary>
    /// 许可服务
    /// </summary>
    public class CustomerConsentService : IConsentService
    {
        public Task<bool> RequiresConsentAsync(ClaimsPrincipal subject, Client client, IEnumerable<string> scopes)
        {
            throw new NotImplementedException();
        }

        public Task UpdateConsentAsync(ClaimsPrincipal subject, Client client, IEnumerable<string> scopes)
        {
            throw new NotImplementedException();
        }
    }
}

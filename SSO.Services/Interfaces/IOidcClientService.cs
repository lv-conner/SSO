using SSO.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace SSO.Services.Interfaces
{
    public interface IOidcClientService
    {
        OidcClient Find(string clientId);
    }
}

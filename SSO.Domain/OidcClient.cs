using System;

namespace SSO.Domain
{
    public class OidcClient
    {
        public string ClientId { get; set; }
        public string ClientName { get; set; }
        public string AllowedGrantTypes { get; set; }
        public bool RequireConsent { get; set; }
        public string RedirectUris { get; set; }
        public string PostLogoutRedirectUris { get; set; }
        public bool AllowAccessTokensViaBrowser { get; set; }
        public string AllowedScopes { get; set; }

    }
}

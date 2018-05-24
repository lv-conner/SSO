using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Mvc;

namespace SSO.Web.Controllers
{
    public class ConsentController : Controller
    {
        public IIdentityServerInteractionService _interaction;
        public ConsentController(IIdentityServerInteractionService interactionService)
        {
            _interaction = interactionService;
        }
        public async Task<IActionResult> Index(string returnUrl)
        {
            var request = await _interaction.GetAuthorizationContextAsync(returnUrl);
            ConsentResponse response = new ConsentResponse()
            {
                RememberConsent = true,
                ScopesConsented = request.ScopesRequested
            };
            await _interaction.GrantConsentAsync(request, response);
            return Redirect(returnUrl);
        }
    }
}
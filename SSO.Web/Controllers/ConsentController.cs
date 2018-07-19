using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Models;
using IdentityServer4.Services;
using IdentityServer4.Stores;
using Microsoft.AspNetCore.Mvc;
using SSO.Web.Models;

namespace SSO.Web.Controllers
{
    //purpose：根据Client请求的相关信息(ClientId,ClientScrect,Scopes)构建授权信息。
    public class ConsentController : Controller
    {
        private readonly IIdentityServerInteractionService _interaction;
        private readonly IClientStore _clientStore;
        private readonly IResourceStore _resourceStore;
        public ConsentController(IIdentityServerInteractionService interactionService,IClientStore clientStore,IResourceStore resourceStore)
        {
            _clientStore = clientStore;
            _interaction = interactionService;
            _resourceStore = resourceStore;
        }
        [HttpGet]
        public async Task<IActionResult> Index(string returnUrl)
        {
            //根据请求Url获取客户端此次请求的ClientId,授权范围等相关信息来获取授权资源。
            var request = await _interaction.GetAuthorizationContextAsync(returnUrl);
            var client = _clientStore.FindEnabledClientByIdAsync(request.ClientId);
            //查找可用资源
            var resource = _resourceStore.FindEnabledResourcesByScopeAsync(request.ScopesRequested);
            //构造授权视图
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Index(ConsentDto model, string returnUrl)
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
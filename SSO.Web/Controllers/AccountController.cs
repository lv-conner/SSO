using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using SSO.Web.Models;

namespace SSO.Web.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginDto model, string returnUrl)
        {
            ClaimsIdentity claimsIdentity = new ClaimsIdentity("IdentityServerConstants.DefaultCookieAuthenticationScheme");
            claimsIdentity.AddClaim(new Claim(ClaimTypes.Name, "tim"));
            claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, "admin"));
            claimsIdentity.AddClaim(new Claim(ClaimTypes.Sid, "001"));
            claimsIdentity.AddClaim(new Claim("sub", "001tim"));
            ClaimsPrincipal user = new ClaimsPrincipal(claimsIdentity);
            await HttpContext.SignInAsync(IdentityServerConstants.DefaultCookieAuthenticationScheme, user);
            if (string.IsNullOrEmpty(returnUrl))
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return Redirect(returnUrl);
            }
        }
    }
}
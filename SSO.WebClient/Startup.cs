using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace SSO.WebClient
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            //清除默认Claim类型对应表。
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            //添加认证服务。配置默认认证模式为Cookie，默认质询模式为oidc
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = "Cookies";
                //配置的OpenIdConnect的Scheme要与该值相同
                options.DefaultChallengeScheme = "oidc";
            })
            //添加CookieAuthenticationHandler
            .AddCookie("Cookies")
            //添加OpenIdConnectorHandler,名称为oidc，配置默认ChanllengSchemeHandler
            .AddOpenIdConnect("oidc", options =>
            {
                //可以配置相应事件来监听认证过程
                options.Events.OnTicketReceived = context =>
                {
                    return Task.CompletedTask;
                };
                //配置质询成功得到Token后的登陆模式
                options.SignInScheme = "Cookies";
                //配置认证Url
                options.Authority = "http://localhost:14590/";
                //配置是否仅支持Https,在开发环境中应设置为false,默认值为true，意味着正式环境中授权Url应仅支持Https
                options.RequireHttpsMetadata = false;
                //配置从认证服务器中得到的客户Id
                options.ClientId = "mvc";
                //配置是否将Access_token和Refresh_token存储在Cookie中，默认为false以减少cookie大小
                options.SaveTokens = true;
                //配置请求的权限。
                options.Scope.Add("Customer");
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            //添加认证中间件。
            app.UseAuthentication();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}

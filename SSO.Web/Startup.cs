using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Stores;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using QuickstartIdentityServer;
using QuickstartIdentityServer.CustomerChange;
using SSO.Web.IdentityServices;

namespace SSO.Web
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
            //配置IdentityServer4
            services.AddIdentityServer(options =>
            {
                //配置用户登陆Url
                options.UserInteraction.LoginUrl = "/Account/Login";
                //配置用户登出Url
                options.UserInteraction.LogoutUrl = "/Account/Logout";
                //当需要授权是跳转到哪一个页面进行授权。
                options.UserInteraction.ConsentUrl = "/Consent/Index";
            })
            //添加Rsa密钥，密钥长度需要大于等于2048，此处配置加密Token的密钥
            .AddSigningCredential(Config.GetRsaSecurityKey())
            //添加资源存储
            .AddResourceStore<CustomerSourceStore>()
            //添加客户端存储
            .AddClientStore<FileClientStore>()
            //添加用户信息提供服务
            .AddProfileService<CustomerProfileService>();
            //替换默认的内存持久化
            //services.AddSingleton<IPersistedGrantStore，CustomerPersistGrant>();
            services.AddSingleton(Config.GetApiResources());
            //需要注意配置的客户端的RedirectUrl要与客户端Url一致，否则会被视为UnAuthorizedClient。
            services.AddSingleton(Config.GetClients());
            //配置身份识别资源
            services.AddSingleton(Config.GetIdentityResources());
            //配置用户
            services.AddSingleton(Config.GetUsers());
            services.AddMvc();
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
            app.UseIdentityServer();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}

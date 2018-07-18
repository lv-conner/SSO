using IdentityServer4.Models;
using IdentityServer4.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuickstartIdentityServer.CustomerChange
{
    //个人信息生成
    public class CustomerProfileService : IProfileService
    {
        //获取返回到客户端的声明(Claim)
        //此处可根据需要返回响应信息
        public Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            context.AddRequestedClaims(context.Subject.Claims);
            return Task.CompletedTask;
        }
        //验证当前登陆用户的激活状态
        public Task IsActiveAsync(IsActiveContext context)
        {
            context.IsActive = true;
            return Task.CompletedTask;
        }
    }
}

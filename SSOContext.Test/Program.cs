using System;
using Microsoft.Extensions.DependencyInjection;
using SSOContext;
using SSO.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace SSOContext.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            IServiceCollection servcies = new ServiceCollection()
                .AddDbContext<SSO.Context.SSOContext>(options =>
                {
                    //options.UseSqlServer("Data Source=PRCNMG1L0311;initial catalog=SSOContext;user Id=sa;password=******");
                })
                .AddTransient<IPersonRepository, PersonRepository>();
            IServiceProvider provider = servcies.BuildServiceProvider();
            List<IPersonRepository> rservices = new List<IPersonRepository>();
            for (int i = 0; i < 10; i++)
            {
                rservices.Add(provider.GetService<IPersonRepository>());
            }

            using (var context = provider.GetService<SSO.Context.SSOContext>())
            {
                context.Database.EnsureCreated();
            }
            Console.WriteLine("Hello World!");
            Console.ReadKey();
        }
    }
}

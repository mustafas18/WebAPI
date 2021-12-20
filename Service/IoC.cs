using Logic.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebAPI.Core.Model;
using WebAPI.DAL;

namespace Logic.Services
{
    public static class IoC
    {
        public static void AddMyServices(this IServiceCollection services)
        {
            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<AppDBContext>()
                .AddDefaultTokenProviders(); 
            services.AddScoped<IJwtHandler,JwtHandler>();
            services.AddScoped<ILoginService,LoginService>();
            services.AddScoped<IUserService, UserService>();
           

            


        }
    }
}

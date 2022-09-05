using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using CustomerAccount.BL.Interfaces;
using CustomerAccount.DAL;
using CustomerAccount.DAL.EF;

namespace CustomerAccount.BL
{
    public static class Extentions
    {
        public static void AddDIServices(this IServiceCollection services)
        {
            services.AddScoped<IAccountBL, AccountBL>();
            services.AddScoped<ILoginBL, LoginBL>();
            services.AddScoped<IStorage, Storage>();
        }
        public static void AddDBContextService(this IServiceCollection services, string connection)
        {
            services.AddDbContextFactory<CustomerAccountDBContext>(item =>
           item.UseSqlServer(connection));
        }
    }
}

using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using CustomerAccount.BL.Interfaces;
using CustomerAccount.DAL;
using CustomerAccount.DAL.EF;

namespace CustomerAccount.BL
{
    public static class Extentions
    {
        public static void AddServices(this IServiceCollection Services, string connection)
        {
            Services.AddDbContextFactory<CustomerAccountDBContext>(item =>
            item.UseSqlServer(connection));

            // add DI services
            Services.AddScoped<IAccountBL, AccountBL>();
            Services.AddScoped<ILoginBL, LoginBL>();

            Services.AddScoped<IStorage, Storage>();


        }
    }
}

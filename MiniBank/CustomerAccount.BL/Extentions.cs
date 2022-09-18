using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using CustomerAccount.BL.Interfaces;
using CustomerAccount.DAL;
using CustomerAccount.DAL.EF;
using CustomerAccount.DAL.Interfaces;
using CustomerAccount.BL.Options;
using System.Text.Json.Nodes;
using EmailSender.Service;

namespace CustomerAccount.BL
{
    public static class Extentions
    {
        public static void AddDIServices(this IServiceCollection services)
        {
            services.AddScoped<IAccountBL, AccountBL>();
            services.AddScoped<ILoginBL, LoginBL>();
            services.AddScoped<IStorage, Storage>();
            services.AddScoped<IOperationBL, OperationBL>();
            services.AddScoped<IStorage, Storage>();
            services.AddScoped<IEmailVerificationBL, EmailVerificationBL>();
            services.AddScoped<ISendsEmail, SendsEmail>();

        }
        public static void AddDBContextService(this IServiceCollection services, string connection)
        {
            services.AddDbContextFactory<CustomerAccountDBContext>(item =>
                item.UseSqlServer(connection));
        }

        //public static void AddOptionServices(this IServiceCollection services, JsonArray   )
        //{
        //    services.Configure<EmailVerificationsOptions>();
        //}
    }
}

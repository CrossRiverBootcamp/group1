using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Transaction.DAL;
using Transaction.DAL.EF;
using Transaction.BL.Interfaces;
using EmailSender.Service;

namespace Transaction.BL
{
    public static class Extentions
    {
        public static void AddDIServices(this IServiceCollection services)
        {
            services.AddScoped<ITransactionBL, TransactionBL>();
            services.AddScoped<IStorage, Storage>();
            services.AddScoped<ISendsEmail, SendsEmail>();
        }
        public static void AddDBContextService(this IServiceCollection services, string connection)
        {
            services.AddDbContextFactory<TransactionDBContext>(item =>
                item.UseSqlServer(connection));
        }
    }
}

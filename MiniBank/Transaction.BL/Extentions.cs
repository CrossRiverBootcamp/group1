using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Transaction.DAL;
using Transaction.DAL.EF;

namespace Transaction.BL
{
    public static class Extentions
    {
        public static void AddDIServices(this IServiceCollection services)
        {
         
        }
        public static void AddDBContextService(this IServiceCollection services, string connection)
        {
            services.AddDbContextFactory<TransactionDBContext>(item =>
           item.UseSqlServer(connection));
        }
    }
}

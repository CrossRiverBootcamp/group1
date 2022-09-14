﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using CustomerAccount.BL.Interfaces;
using CustomerAccount.DAL;
using CustomerAccount.DAL.EF;
using CustomerAccount.DAL.Interfaces;

namespace CustomerAccount.BL
{
    public static class Extentions
    {
        public static void AddDIServices(this IServiceCollection services)
        {
            services.AddScoped<IAccountBL, AccountBL>();
            services.AddScoped<ILoginBL, LoginBL>();
            services.AddScoped<ICustomerAccountDAL, CustomerAccountDAL>();
            services.AddScoped<IOperationBL, OperationBL>();
            services.AddScoped<IOperationDAL, OperationDAL>();
            services.AddScoped<IEmailVerificationBL, EmailVerificationBL>();

        }
        public static void AddDBContextService(this IServiceCollection services, string connection)
        {
            services.AddDbContextFactory<CustomerAccountDBContext>(item =>
                item.UseSqlServer(connection));
        }
        //מיותר????? 
        public static void AddDIServicesNSB(this IServiceCollection services)
        {
            services.AddScoped<IAccountBL, AccountBL>();
            services.AddScoped<ICustomerAccountDAL, CustomerAccountDAL>();
        }
    }
}

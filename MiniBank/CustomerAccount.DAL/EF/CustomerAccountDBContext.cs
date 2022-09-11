using CustomerAccount.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerAccount.DAL.EF
{
    public class CustomerAccountDBContext : DbContext
    {
        public CustomerAccountDBContext(DbContextOptions<CustomerAccountDBContext> options) : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<AccountData> AccountDatas { get; set; }
        public DbSet<OperationData> Operations { get; set; }

    }
}

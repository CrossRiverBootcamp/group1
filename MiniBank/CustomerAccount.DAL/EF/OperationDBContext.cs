using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerAccount.DAL.EF
{
    public class OperationDBContext
    {
        public class OperationDBContext : DbContext
        {
            public OperationDBContext(DbContextOptions<OperationDBContext> options) : base(options)
            {
            }

            public DbSet<Operation> Operations { get; set; }
         

        }
    }
}

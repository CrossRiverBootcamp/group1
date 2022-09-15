using NServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerAccount.Messeges
{
    public class DeleteExpiredRows : ICommand
    {

        public DateTime Date { get; set; }
    }
}
}

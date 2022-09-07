using NServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transaction.NSB
{
    public class TransactionPolicyData : ContainSagaData
    {
        public Guid TransactionId { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transaction.DTO
{
    public class UpadateTransactionStatusDTO
    {
        public Guid TransactioId { get; set; }
        public bool IsSuccess { get; set; }
        public string? FailureReasun { get; set; }
    }
}

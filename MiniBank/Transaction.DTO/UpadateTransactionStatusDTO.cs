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
        //צריך פה אטרביוטים?
        [Required]
        public Guid TransactioId { get; set; }
        [Required]
        public bool IsSuccess { get; set; }
        public string FailureReasun { get; set; }
    }
}

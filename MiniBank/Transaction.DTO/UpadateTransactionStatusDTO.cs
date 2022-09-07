using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transaction.DAL.Models;

namespace Transaction.DTO
{
    public class UpadateTransactionStatusDTO
    {
        [Required]
        public Guid TransactioId { get; set; }
        [Required]
        public StatusEnum Status { get; set; }
        public string FailureReasun { get; set; }
    }
}

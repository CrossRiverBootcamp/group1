using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transaction.DTO
{
    public class TransactionDTO
    {
 

        [Required]
        public Guid FromAccountId { get; set; }

        [Required]
        public Guid ToAccountId { get; set; }

        [Required]
        public int Amount { get; set; }

        [Required]
        public DateTime Date { get; set; }
      
     
    }
}

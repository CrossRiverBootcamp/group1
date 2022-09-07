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
        [Range (1,1000000)]
        [Required]
        public float Amount { get; set; }

        [Required]
        public DateTime Date { get; set; }
      
     
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerAccount.DTO
{
    public class OperationDTO
    {
        [Required]
        public Boolean IsCredit { get; set; }

        [Required]
        public Guid TransactionPartnerAccountId { get; set; }

        [Required]
        public int  Amount { get; set; }
        [Required]
        public int Balance { get; set; }
        [Required]
        public DateTime Date { get; set; }

    }
}

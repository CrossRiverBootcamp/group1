using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerAccount.DAL.Entities
{
    public class OperationData
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [ForeignKey("AccountData")]
       
        public int AccountId { get; set; }
        virtual public AccountData AccountData { get; set; }
        [Required]
        public int TransactionId { get; set; }

        [Required]
        public Boolean IsCredit { get; set; }
       
        [Required]
        public int TransactionAmount { get; set; }
        [Required]
        public int Balance { get; set; }
        [Required]
        public DateTime OperationTime { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerAccount.DAL.Entities
{
    public class AccountData
    {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        [Required]
        [ForeignKey("Customer")]
        public Guid CustomerId { get; set; }
        virtual public Customer Customer { get; set; }
        [Required]
        public DateTime OpenDate { get; set; }
        [Required]
        public string Balance { get; set; }
    }
}

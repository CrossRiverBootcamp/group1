using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerAccount.DAL.Entities
{
    public class EmailVerification
    {
        [Key]
        [EmailAddress]
        [MaxLength(25)]
        public string Email { get; set; }
        [Required]
        [StringLength(6)]
        public string VerificationCode { get; set; }
        [Required]
        public DateTime ExpirationTime { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Only positive number allowed")]
        public int NumOfAttemps { get; set; }
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Only positive number allowed")]
        public int CodeNum { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerAccount.DTO
{
    public class CustomerAccountDTO
    {
        [Required]
        [MinLength(2)]
        [MaxLength(25)]
        public string FirstName { get; set; }
        [Required]
        [MinLength(2)]
        [MaxLength(25)]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(25)]
        public string Email { get; set; }
        [Required]
        [MaxLength(15)]
        [MinLength(4)]
        public string Password { get; set; }
        [Required]
        public DateTime OpenDate { get; set; }
        [Required]
        public int Balance { get; set; }
    
    }
}

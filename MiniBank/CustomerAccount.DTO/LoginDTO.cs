using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerAccount.DTO
{
    public class LoginDTO
    {
        [Required]
        [EmailAddress]
        [MaxLength(25)]
        public string Email { get; set; }
        [Required]
        [MaxLength(15)]
        [MinLength(4)]
        public string Password { get; set; }
    }
}

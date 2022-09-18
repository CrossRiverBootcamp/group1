using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerAccount.DAL.Models
{
    public class EmailVerificationModel
    {
        public string Email { get; set; }
        public string VerificationCode { get; set; }
        public DateTime ExpirationTime { get; set; }
        public int NumOfAttemps { get; set; }
        public int CodeNum { get; set; }
    }
}

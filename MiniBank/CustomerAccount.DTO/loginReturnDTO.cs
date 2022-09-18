
using System.ComponentModel.DataAnnotations;


namespace CustomerAccount.DTO
{
    
    public class loginReturnDTO

    {
        [Required]
     
        public  Guid AccountId{ get; set; }

        [Required]
        public string Token { get; set; }
    }
}

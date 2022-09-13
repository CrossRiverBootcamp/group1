using System.ComponentModel.DataAnnotations;


namespace CustomerAccount.DTO
{
    public class TransactionPartnerDetailsDTO
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
    }
}

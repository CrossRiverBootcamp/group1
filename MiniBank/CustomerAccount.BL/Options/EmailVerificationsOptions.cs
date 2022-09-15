namespace CustomerAccount.WebAPI.Options
{
    public class EmailVerificationsOptions
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public int NumOfAttemptsAllowed { get; set; }
        public int NumOfVerficationCodesAllowed { get; set; }
        public int VerficationCodeValidityTime { get; set; }
    }
}

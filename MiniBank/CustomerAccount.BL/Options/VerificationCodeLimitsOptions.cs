namespace CustomerAccount.BL.Options;

public class VerificationCodeLimitsOptions
{
    public int NumOfGuessingAttemptsAllowed { get; set; }
    public int NumOfResendsAllowed { get; set; }
    public int ValidityTime { get; set; }
}

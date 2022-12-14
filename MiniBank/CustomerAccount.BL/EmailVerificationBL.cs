using CustomerAccount.BL.Interfaces;
using CustomerAccount.BL.Options;
using CustomerAccount.DAL.Entities;
using CustomerAccount.DAL.Interfaces;
using CustomerAccount.DAL.Models;
using CustomerAccount.DTO;
using ExtendedExceptions;
using Microsoft.Extensions.Options;
using NServiceBus.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using EmailSender.Service;

namespace CustomerAccount.BL
{
    public class EmailVerificationBL : IEmailVerificationBL
    {
        //DI
        private readonly IStorage _storage;
        private readonly ISendsEmail _sendsEmail;

        //add options
        private readonly VerificationCodeLimitsOptions _verificationOptions;
        private readonly EmailOptions _emailOptions;

        public EmailVerificationBL(IStorage storage,
            ISendsEmail sendsEmail,
            IOptions<VerificationCodeLimitsOptions> verificationOptions,
            IOptions<EmailOptions> emailOptions
            )
        {
            _storage = storage;
            _sendsEmail = sendsEmail;
            _verificationOptions = verificationOptions.Value;
            _emailOptions = emailOptions.Value;
        }

        private async Task<string> CreateEmailVerification(string email, int codeNum)
        {
            Random rand = new Random();
            string verificationCode = "";
            for (int i = 0; i < 6; i++)
                verificationCode += rand.Next(0, 9).ToString();

            EmailVerificationModel emailVerificationModel = new EmailVerificationModel()
            {
                Email = email,
                VerificationCode = verificationCode,
                ExpirationTime = DateTime.UtcNow.AddSeconds(360),
                CodeNum = codeNum
            };

            await _storage.CreateEmailVerification(emailVerificationModel);
            return verificationCode;
        }
        private string[] CreateVerificationEmailBody(string verificationCode)
        {
            // string link = "<a href= http://localhost:4200/#/aaaaa/?id=">Confirm your email here</a>";

            string subject = "Email Verification | Mini-Bank CR";
            string body = "Your verfication code is: " + verificationCode +
                "<br> The code is valid for 5 minutes <br> If this wasn't you- note someone is trying to use your email";

            string[] content = new string[2];
            content[0] = subject;
            content[1] = body;

            return content;
        }
        private async Task<int> UpdateLimitAndReturnNumberOfResends(string email)
        {
            int CodeNum = await _storage.UpdateAndGetNumOfResends(email);
            if (CodeNum > _verificationOptions.NumOfResendsAllowed)
                throw new TooManyRetriesException();
            return CodeNum;
        }
        public async Task HandleEmailVerificationRequest(string email, bool isResendRequest)
        {
            int codeNum;

            //check if a resend request
            if (isResendRequest)
                codeNum = (await UpdateLimitAndReturnNumberOfResends(email))+1;
            else
                codeNum = 1;

            //create EmailVerification Model
            string verificationCode = await CreateEmailVerification(email, codeNum);

            //create Verification email
            string[] content = CreateVerificationEmailBody(verificationCode);

            //send Verification email
            _sendsEmail.SendEmail(_emailOptions, email, content[0], content[1]);
        }
        
        public Task<bool> ValidateCodeAndTime(CustomerDTO customerDTO)
        {
            return _storage.ValidateCodeAndTime(customerDTO.Email, customerDTO.VerificationCode);
        }
        public async Task<int> UpdateAndLimitNumberOfAttempts(string email)
        {
            int numOfAttempts = await _storage.UpdateAndGetNumOfAttempts(email);
            if (numOfAttempts > _verificationOptions.NumOfGuessingAttemptsAllowed)
                throw new TooManyRetriesException();
            return numOfAttempts;
        }
        public Task DeleteExpiredRows()
        {
            return _storage.DeleteExpiredRows();
        }
    }
}

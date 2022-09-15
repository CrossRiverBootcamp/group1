using CustomerAccount.BL.Interfaces;
using CustomerAccount.DAL.Entities;
using CustomerAccount.DAL.Interfaces;
using CustomerAccount.DAL.Models;
using CustomerAccount.DTO;
using ExtendedExceptions;
using NServiceBus.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace CustomerAccount.BL
{
    public class EmailVerificationBL : IEmailVerificationBL
    {
        //move to options
        private static int NumOfAttemptsAllowed = 3;
        private static int NumOfVerficationCodesAllowed = 2;
        private static readonly string _FromEmail = "crssrvrminibank@gmail.com";
        private static readonly string _FromPassword = "dfgoasfhwgytqefi";
        //private static readonly int _VerificationCodeValidity = 360;

        private readonly ICustomerAccountDAL _customerAccountDAL;

        public EmailVerificationBL(ICustomerAccountDAL customerAccountDAL)
        {
            _customerAccountDAL = customerAccountDAL;
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

            await _customerAccountDAL.CreateEmailVerification(emailVerificationModel);
            return verificationCode;
        }
        private async Task<string[]> CreateVerificationEmailBodey(string verificationCode)
        {
            // string link = "<a href= http://localhost:4200/#/guest-confirm/?id="
            // + g.Id + ">Confirm your email here</a>";

            string subject = "Email Verification | Mini-Bank CR";
            string body = "Your verfication code is: " + verificationCode +
                "The code is valid for 5 minutes <br> If this wasn't you- note someone is trying to use your email";

            string[] content = new string[2];
            content[0] = subject;
            content[1] = body;

            return content;
        }
        public async Task HandleEmailVerificationRequest(string email, bool isResendRequest)
        {
            int codeNum;

            //check if a resend request
            if (isResendRequest)
                codeNum = await UpdateLimitAndReturnNumberOfResends(email);
            else
                codeNum = 1;

            //create EmailVerification Model
            string verificationCode = await CreateEmailVerification(email, codeNum);

            //create Verification email
            string[] content = await CreateVerificationEmailBodey(verificationCode);

            //send Verification email
            SendEmail(email, content[0], content[1]);
        }
        //private??
        public void SendEmail(string email, string subject, string body)
        {
            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress(_FromEmail);
                mail.To.Add(email);
                mail.Subject = subject;
                mail.Body = body;
                mail.IsBodyHtml = true;
                //mail.Attachments.Add(new Attachment("C:\\file.zip"));

                using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                {
                    smtp.Credentials = new NetworkCredential(_FromEmail, _FromPassword);
                    smtp.UseDefaultCredentials = false;
                    smtp.EnableSsl = true;
                    smtp.Send(mail);
                }
            }
        }
        public Task<bool> ValidateCodeAndTime(CustomerDTO customerDTO)
        {
            return _customerAccountDAL.ValidateCodeAndTime(customerDTO.Email, customerDTO.VerificationCode);
        }
        public async Task UpdateAndLimitNumberOfAttempts(string email)
        {
            int numOfAttempts = await _customerAccountDAL.UpdateAndGetNumOfAttempts(email);
            if (numOfAttempts == NumOfAttemptsAllowed)
                throw new TooManyRetriesException();
        }
        public async Task<int> UpdateLimitAndReturnNumberOfResends(string email)
        {
            int CodeNum = await _customerAccountDAL.UpdateAndGetNumOfResends(email);
            if (CodeNum == NumOfVerficationCodesAllowed)
                throw new TooManyRetriesException();

            return CodeNum;
        }
    }
}

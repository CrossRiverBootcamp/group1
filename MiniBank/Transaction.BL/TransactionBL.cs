using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transaction.DAL.EF;
using Transaction.DAL.Entities;
using AutoMapper;
using Transaction.DTO;
using Transaction.DAL;
using Transaction.DAL.Models;
using Transaction.BL.Interfaces;
using NServiceBus;
using Transaction.Messeges;
using ExtendedExceptions;
using System.Security.Claims;
using EmailSender;
using System.Net.NetworkInformation;
using EmailSender.Service;
using Microsoft.Extensions.Options;
using CustomerAccount.BL.Options;

namespace Transaction.BL
{
    public class TransactionBL : ITransactionBL
    {
        //add DI
        private readonly IStorage _Storage;
        private readonly IMapper _mapper;
        private readonly ISendsEmail _emailSender;

        //add options
        private readonly EmailOptions _emailOptions;
        public TransactionBL(IMapper mapper, IStorage Storage, ISendsEmail emailSender,
            IOptions<EmailOptions> emailOptions)
        {
            _mapper = mapper;
            _Storage = Storage;
            _emailSender = emailSender;
            _emailOptions = emailOptions.Value;
        }
        private string[] CreateTransactionDoneBody(bool isDone)
        {
            string subject = "Your transaction Update | Mini-Bank CR";
            string body = "Your transfer request had returned with status: " + isDone;

            string[] content = new string[2];
            content[0] = subject;
            content[1] = body;

            return content;
        }
        private string[] CreateAccoutCreditedBody()
        {
            string subject = "Accout credited message| Mini-Bank CR";
            string body = "Your account has bin credited, see your account for more info." ;

            string[] content = new string[2];
            content[0] = subject;
            content[1] = body;

            return content;
        }

        public void InformCustomerWithTrasactionStatus(string email, bool isDone)
        {
            string[] content = CreateTransactionDoneBody(isDone);
            _emailSender.SendEmail(_emailOptions, email, content[0], content[1]);
        }
        public void InformAccuntCredited(string email)
        {
            string[] content = CreateAccoutCreditedBody();
            _emailSender.SendEmail(_emailOptions, email, content[0], content[1]);
        }
        public async Task<bool> PostTransactionStartSaga(TransactionDTO transactionDTO, IMessageSession _messageSession)
        {

            DAL.Entities.Transaction transaction = _mapper.Map<TransactionDTO, DAL.Entities.Transaction>(transactionDTO);
            transaction.Date = DateTime.UtcNow;

            try
            {
                var transactionId = await _Storage.PostTransaction(transaction);
                TransactionReqMade transactionReqMade = new TransactionReqMade()
                {
                    TransactionId = transactionId,
                    FromAccountId = transaction.FromAccountId,
                    ToAccountId = transaction.ToAccountId,
                    Amount = transaction.Amount
                };

                //publish the event
                await _messageSession.Publish(transactionReqMade);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public async Task ChangeTransactionStatus(UpadateTransactionStatusDTO upadateTransactionStatusDTO)
        {
            _Storage.ChangeTransactionStatus(upadateTransactionStatusDTO);
        }
        public Guid getAccountIDFromToken(ClaimsPrincipal User)
        {
            var accountID = User.Claims.First(x => x.Type.Equals("AccountID", StringComparison.InvariantCultureIgnoreCase)).Value;
            return Guid.Parse(accountID);
        }
    }
}
﻿using System;
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

namespace Transaction.BL
{
    public class TransactionBL : ITransactionBL
    {
        private readonly IStorage _Storage;
        private readonly IMapper _mapper;
        private readonly ISendsEmail _emailSender;
        public TransactionBL(IMapper mapper, IStorage Storage, ISendsEmail emailSender)
        {
            _mapper = mapper;
            _Storage = Storage; 
            _emailSender = emailSender;
        }
        private Task<string[]> CreateTransactionDoneBodey(StatusEnum status)
        {
            // string link = "<a href= http://localhost:4200/#/guest-confirm/?id="
            // + g.Id + ">Confirm your email here</a>";

            string subject = "Your transaction Update | Mini-Bank CR";
            string body = "Your transfer request had returned with status: "+ status;

            string[] content = new string[2];
            content[0] = subject;
            content[1] = body;

            return content;
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

        public async Task InformCustomerWithTrasactionStatus(string email, StatusEnum status)
        {
            string[] content = await CreateTransactionDoneBodey(status);
            _emailSender.SendEmail(, email, content[0], content[1]);
        }

    }
}

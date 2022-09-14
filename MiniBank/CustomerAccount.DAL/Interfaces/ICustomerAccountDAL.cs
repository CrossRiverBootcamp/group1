﻿using CustomerAccount.DAL.Entities;
using CustomerAccount.DAL.Models;

namespace CustomerAccount.DAL.Interfaces
{
    public interface ICustomerAccountDAL
    {
        Task<bool> CreateCustomerAccount(CustomerModel customerModel, AccountData accountData);
        Task CreatesEmailVerification(EmailVerificationModel emailVerificationModel);
        Task<bool> CustomerExists(string email);
        Task<bool> CustumerAccountExists(Guid accountId);
        Task<AccountData> GetAccountData(Guid accountDataId);
        Task<int> GetNumOfAttempts(string email);
        Task<Guid> Login(string email, string password);
        Task<BalancesModel> MakeBankTransfer(Guid fromAccountId, Guid toAccountId, int amount);
        Task<bool> SenderHasEnoughBalance(Guid accountId, int amount);
        Task UpdateNumOfAttempts(string email);
        Task<bool> ValidateCodeAndTime(string email, string verificationCode);
    }
}
﻿using CustomerAccount.DTO;

namespace CustomerAccount.BL.Interfaces
{
    public interface IAccountBL
    {
        Task<bool> CreateAccount(CustomerDTO customerDTO);
         Task<CustomerAccountInfoDTO> GetAccountInfo(Guid accountId);

        Task<bool> CustumerAccountExists(Guid accountId);
        Task<bool> SenderHasEnoughBalance (Guid accountId, int amount);
        Task MakeBankTransfer(Guid fromAccountId, Guid toAccountId, int amount);

        //moved to operation BL
        //Task<TransactionPartnerDetailsDTO> GetTransactionPartnerAccountInfo(Guid transactionPartnerAccountId);
    }
}
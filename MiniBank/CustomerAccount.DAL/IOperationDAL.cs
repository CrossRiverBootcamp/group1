﻿using CustomerAccount.DAL.Entities;

namespace CustomerAccount.DAL
{
    public interface IOperationDAL
    {
        Task<IEnumerable<OperationData>> GetByPageAndAccountId(Guid AccountId, int PageNumber, int PageSize);
    }
}
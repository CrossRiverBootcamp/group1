﻿using CustomerAccount.DAL.Entities;
using CustomerAccount.DAL.Models;

namespace CustomerAccount.DAL.Interfaces
{
    public interface IOperationDAL
    {
        Task<IEnumerable<OperationData>> GetByPageAndAccountId(Guid AccountId, int PageNumber, int PageSize);

        Task<Guid> PostOperation(OperationData operation);
    }
}
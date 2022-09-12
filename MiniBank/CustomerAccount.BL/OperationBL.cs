using AutoMapper;
using CustomerAccount.BL.Interfaces;
using CustomerAccount.DAL.Entities;
using CustomerAccount.DAL.Interfaces;
using CustomerAccount.DAL.Models;
using CustomerAccount.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerAccount.BL
{
    public class OperationBL : IOperationBL
    {
        private readonly IMapper _mapper;
        private readonly IOperationDAL _operationDAL;
        public OperationBL(IMapper mapper, IOperationDAL operationDAL)
        {
            _mapper = mapper;
            _operationDAL = operationDAL;
        }
        public async Task<IEnumerable<OperationDTO>> GetByPageAndAccountId(Guid AccountId, SortDirection sortDirection,int PageNumber, int PageSize)
        {
            return _mapper.Map<IEnumerable<OperationData>, IEnumerable<OperationDTO>>(await _operationDAL.GetByPageAndAccountId(AccountId, sortDirection, PageNumber, PageSize));

        }
    }
}

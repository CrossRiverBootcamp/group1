using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerAccount.BL
{
    public class OperationBL
    {
        private readonly IMapper _mapper;
        private readonly IOperationDAL OperationDAL;
        public OperationBL(IMapper mapper, IOperationDAL OperationDAL)
        {
            _mapper = mapper;
            _operationDAL = operationDAL;
        }
        public Task<List<OperationDTO>> GetByPageAndAccountId( int AccountId, int PageNumber, int PageSize)
        {
            return _mapper.Map<Operation, OperationDTO>(_OperationDAL.GetByPageAndAccountId(AccountId, PageNumber, PageSize));
            

    }
}

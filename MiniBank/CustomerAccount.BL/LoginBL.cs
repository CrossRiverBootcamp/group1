using AutoMapper;
using CustomerAccount.BL.Interfaces;
using CustomerAccount.DAL.Interfaces;
using CustomerAccount.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerAccount.BL
{
    public class LoginBL:ILoginBL
    {
        private readonly IMapper _mapper;
        private readonly ICustomerAccountDAL _CustomerAccountDAL;
        public LoginBL(IMapper mapper, ICustomerAccountDAL CustomerAccountDAL)
        {
            _mapper = mapper;
            _CustomerAccountDAL = CustomerAccountDAL;
        }

         public Task <Guid> Login(LoginDTO loginDTO)
         {
            return _CustomerAccountDAL.Login( loginDTO.Email , loginDTO.Password);
         }
    }
}

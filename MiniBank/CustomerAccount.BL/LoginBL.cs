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
        private readonly IStorage _storage;
        public LoginBL(IMapper mapper, IStorage storage)
        {
            _mapper = mapper;
            _storage = storage;
        }

         public Task <Guid> Login(LoginDTO loginDTO)
         {
             return _storage.Login( loginDTO.Email ,loginDTO.Password);
         }
    }
}

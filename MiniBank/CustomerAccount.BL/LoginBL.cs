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
        private readonly IStorage _Storage;
        public LoginBL(IMapper mapper, IStorage Storage)
        {
            _mapper = mapper;
            _Storage = Storage;
        }

         public Task <Guid> Login(LoginDTO loginDTO)
         {
            return _Storage.Login( loginDTO.Email , loginDTO.Password);
         }
    }
}

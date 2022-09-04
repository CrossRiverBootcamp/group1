using AutoMapper;
using CustomerAccount.DAL;
using CustomerAccount.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerAccount.BL
{
    public class LoginBL
    {
        private readonly IMapper _mapper;
        private readonly IStorage _Storage;
        public LoginBL(IMapper mapper, IStorage storage)
        {
            _mapper = mapper;
            _Storage = storage;
        }

         Task <Guid> Login(LoginDTO loginDTO)
         {

            return  _Storage.Login( loginDTO.Email , loginDTO.Password);
         }
    }
}

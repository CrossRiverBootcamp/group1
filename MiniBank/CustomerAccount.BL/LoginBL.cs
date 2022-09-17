using AutoMapper;
using CustomerAccount.BL.Interfaces;
using CustomerAccount.DAL.Interfaces;
using CustomerAccount.DTO;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CustomerAccount.BL
{
    public class LoginBL:ILoginBL
    {
        private readonly IMapper _mapper;
        private readonly IStorage _Storage;
        private readonly IConfiguration _configuration;

        public LoginBL(IMapper mapper, IStorage Storage, IConfiguration configuration

)
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

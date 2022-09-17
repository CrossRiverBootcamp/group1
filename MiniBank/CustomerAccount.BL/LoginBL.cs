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
            _configuration = configuration;

        }
        //מה הפונקציה מחזירה
         public async Task<Guid> Login(LoginDTO loginDTO)
         {
            string token =  await GetToken();
            Guid accountId =  await  _Storage.Login( loginDTO.Email , loginDTO.Password);
            return new idWithToken()
            {
                token = token,
                accountId = accountId,
            };
            //ליצור אובייקט ולהחזיר את הטוקן והאקאונט
        }
        public async Task<string> GetToken()
        {
            //create claims details based on the user information
            var claims = new[] {
                        new Claim("Role", "user")
                    };
            var issuer = "https://exemple.com";
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:key"]));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer,
                issuer,
                claims,
                expires: DateTime.UtcNow.AddMinutes(10),
                signingCredentials: signIn);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}

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
    public class LoginBL :  ILoginBL
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

        public async Task<loginReturnDTO> Login(LoginDTO loginDTO)
        {

            Guid accountId = await _Storage.Login(loginDTO.Email, loginDTO.Password);
            string token = CreateToken(accountId);

            return new loginReturnDTO()
            {
                AccountId = accountId,
                Token = token
            };
        }
        public string CreateToken(Guid AccountId)
        {
            var claims = new[] {
                        new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim("AccountId",AccountId.ToString()),

                    };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["key"]));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.UtcNow.AddMinutes(10),
                signingCredentials: signIn);
            string accountToken = new JwtSecurityTokenHandler().WriteToken(token);
            return accountToken;
        }
    }
}

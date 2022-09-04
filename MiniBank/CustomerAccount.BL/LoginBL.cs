using AutoMapper;
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

        Task<Guid>LogIn(string email, string password)
        {

            _Storage.LogIn(string email, string password);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CustomerAccount.DTO;

namespace CustomerAccount.BL
{
    public class AutoMapping
    {
        public AutoMapping() : Profile
        {
            //.reveseMap כדאי??

            CreateMap<CustomerDTO, Customer>();

            CreateMap<LoginDTO, Login>();

            CreateMap<AccountDTO, Customer>()
                .ForMember(dest => dest.OpenDate, opts => opts
                    .MapFrom(src => src.Account.OpenDate))
                .ForMember(dest => dest.Balance, opts => opts
                    .MapFrom(src => src.Account.Balance));
        }
    }
}

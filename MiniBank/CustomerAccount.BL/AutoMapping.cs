using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CustomerAccount.DTO;

namespace CustomerAccount.BL
{
    public class AutoMapping : Profile
    {
        public AutoMapping() 
        {
           

          

            

            CreateMap<AccountData, AccountCustomerInfoDTO>()
                .ForMember(dest => dest.FirstName, opts => opts
                    .MapFrom(src => src.customer.FirstName))
                .ForMember(dest => dest.LastName, opts => opts
                    .MapFrom(src => src.customer.LastName));
        }
    }
}

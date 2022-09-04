using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CustomerAccount.DAL.Entities;
using CustomerAccount.DTO;

namespace CustomerAccount.BL
{
    public class AutoMapping : Profile
    {
        public AutoMapping() 
        {


            CreateMap<CustomerDTO, Customer>();
            CreateMap<CustomerAccountDTO, Customer>();




            CreateMap<AccountData, CustomerAccountInfoDTO>()
                .ForMember(dest => dest.FirstName, opts => opts
                    .MapFrom(src => src.Customer.FirstName))
                .ForMember(dest => dest.LastName, opts => opts
                    .MapFrom(src => src.Customer.LastName));
        }
    }
}

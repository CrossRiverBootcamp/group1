using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CustomerAccount.DAL.Entities;
using CustomerAccount.DAL.Models;
using CustomerAccount.DTO;

namespace CustomerAccount.BL
{
    public class AutoMapping : Profile
    {
        public AutoMapping() 
        {
            CreateMap<CustomerDTO, Customer>();
            CreateMap<BalancesModel, BalancesDTO>();

            CreateMap<AccountData, CustomerAccountInfoDTO>()
                .ForMember(dest => dest.FirstName, opts => opts
                    .MapFrom(src => src.Customer.FirstName))
                .ForMember(dest => dest.LastName, opts => opts
                    .MapFrom(src => src.Customer.LastName))
                .ForMember(dest => dest.Balance, opts => opts
                    .MapFrom(src => src.Balance / 100));

            CreateMap<AccountData, TransactionPartnerDetailsDTO>()
                .ForMember(dest => dest.FirstName, opts => opts
                    .MapFrom(src => src.Customer.FirstName))
                .ForMember(dest => dest.LastName, opts => opts
                    .MapFrom(src => src.Customer.LastName)).
                    ForMember(dest => dest.Email, opts => opts
                    .MapFrom(src => src.Customer.Email));


        }
    }
}

using AutoMapper;
using CustomerAccount.DAL.Entities;
using CustomerAccount.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerAccount.DAL
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<EmailVerificationModel, EmailVerification>();
            CreateMap<CustomerModel,Customer>();
        }

    }
}

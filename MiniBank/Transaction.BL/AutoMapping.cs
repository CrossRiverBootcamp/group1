using AutoMapper;
using Transaction.DTO;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transaction.DAL;
namespace Transaction.BL
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<TransactionDTO, DAL.Entities.Transaction>();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transaction.DAL.EF;

namespace Transaction.DAL.Assets;

public enum StatusEnum
{
    PROCESSING,SUCCESS,FAIL
}

public class h {
    TransactionDBContext tr;
}
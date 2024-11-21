using Core.Entities;
using Domain.Entities.FinanceManagements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.TransportationManagements
{
    public class TransportationExternalService : BaseEntity
    {



        public ICollection<FinanceBalance>? FinanceBalances { get; set; }

    }
}

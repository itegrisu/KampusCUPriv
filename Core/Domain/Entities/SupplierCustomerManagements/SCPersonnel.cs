using Core.Entities;
using Domain.Entities.GeneralManagements;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.SupplierCustomerManagements
{
    public class SCPersonnel : BaseEntity
    {

        public Guid GidSCCompanyFK { get; set; }
        public SCCompany SCCompanyFK { get; set; }
        public Guid GidPersonnelFK { get; set; }
        public User UserFK { get; set; }

        public EnumSCPersonnelLoginStatus SCPersonnelLoginStatus { get; set; }


    }
}

using Core.Entities;
using Domain.Entities.DefinitionManagements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.SupplierCustomerManagements
{
    public class SCBank : BaseEntity
    {

        public Guid GidSCCompanyFK { get; set; }
        public SCCompany SCCompanyFK { get; set; }
        public Guid GidCurrencyFK { get; set; }
        public Currency CurrencyFK { get; set; }

        public string Bank { get; set; } = string.Empty;
        public string BranchName { get; set; } = string.Empty;
        public string? BranchCode { get; set; }
        public string AccountNumber { get; set; } = string.Empty;
        public string? IbanNo { get; set; }
        public string? SwiftNo { get; set; }


    }
}

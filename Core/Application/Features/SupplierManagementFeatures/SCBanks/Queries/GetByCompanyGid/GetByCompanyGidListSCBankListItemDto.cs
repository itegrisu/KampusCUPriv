using Core.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.SupplierManagementFeatures.SCBanks.Queries.GetByCompanyGid
{
    public class GetByCompanyGidListSCBankListItemDto :IDto
    {
        public Guid Gid { get; set; }
        public Guid GidSCCompanyFK { get; set; }
        public string SCCompanyFKCompanyName { get; set; }
        public Guid GidCurrencyFK { get; set; }
        public string CurrencyFKDovizAdi { get; set; }
        public string Bank { get; set; }
        public string BranchName { get; set; }
        public string? BranchCode { get; set; }
        public string AccountNumber { get; set; }
        public string? IbanNo { get; set; }
        public string? SwiftNo { get; set; }

    }
}

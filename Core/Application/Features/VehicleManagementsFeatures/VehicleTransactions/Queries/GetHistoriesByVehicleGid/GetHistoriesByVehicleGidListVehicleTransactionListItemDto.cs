using Core.Application.Dtos;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.VehicleManagementsFeatures.VehicleTransactions.Queries.GetHistoriesByVehicleGid
{
    public class GetHistoriesByVehicleGidListVehicleTransactionListItemDto : IDto
    {
        public Guid Gid { get; set; }
        public Guid GidVehicleFK { get; set; }
        public string VehicleAllFKPlateNumber { get; set; }
        public Guid? GidSupplierCustomerFK { get; set; }
        public string SCCompanyFKCompanyName { get; set; }
        public Guid? GidVehicleUsePersonnelFK { get; set; }
        public string UserFKFullName { get; set; }
        public Guid GidFeeCurrencyFK { get; set; }
        public string CurrencyFKName { get; set; }
        public int StartKM { get; set; }
        public int? EndKM { get; set; }
        public int? Fee { get; set; }
        public DateTime? ContractStartDate { get; set; }
        public DateTime? ContractEndDate { get; set; }
        public string? ContactPerson { get; set; }
        public string? ContactPhone { get; set; }
        public string? ArventoAPIInfo { get; set; }
        public string? LicenseFile { get; set; }
        public string? ContractFile { get; set; }
        public string? Description { get; set; }
        public DateTime PurchaseDate { get; set; }
        public DateTime? EndDate { get; set; }
        public EnumVehicleStatus VehicleStatus { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}

using Core.Application.Dtos;
using Core.Enum;
using Domain.Enums;

namespace Application.Features.VehicleManagementFeatures.VehicleTransactions.Queries.GetList;

public class GetListVehicleTransactionListItemDto : IDto
{
    public Guid Gid { get; set; }
    public Guid GidVehicleAllFK { get; set; }
    public string VehicleAllFKPlateNumber { get; set; }
    public Guid? GidSupplierCustomerFK { get; set; }
    public string SCCompanyFKCompanyName { get; set; }
    public Guid? GidVehicleUsePersonnelFK { get; set; }
    public string UserFKFullName { get; set; }
    public int StartKM { get; set; }
    public int MonthlyRentalFee { get; set; }
    public DateTime ContractStartDate { get; set; }
    public DateTime ContractEndDate { get; set; }
    public string? ContactPerson { get; set; }
    public string? ContactPhone { get; set; }
    public string? ArventoAPIInfo { get; set; }
    public string? LicenseFile { get; set; }
    public string? ContractFile { get; set; }
    public string? Description { get; set; }
    public DateTime PurchaseDate { get; set; }
    public DateTime SaleDate { get; set; }
    public EnumVehicleStatus VehicleStatus { get; set; }


}
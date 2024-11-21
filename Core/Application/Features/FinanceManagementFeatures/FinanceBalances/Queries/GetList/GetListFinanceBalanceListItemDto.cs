using Core.Application.Dtos;
using Core.Enum;
using Domain.Enums;

namespace Application.Features.FinanceManagementFeatures.FinanceBalances.Queries.GetList;

public class GetListFinanceBalanceListItemDto : IDto
{
    public Guid Gid { get; set; }
    public Guid GidSupplierCustomerFK { get; set; }
    public string SCCompanyFKCompanyName { get; set; }
    public Guid? GidVehicleTransactionFK { get; set; }
    public string VehicleTransactionFKVehicleAllFKPlateNumber { get; set; }
    public Guid? GidTransportationFK { get; set; }
    public string TransportationFKTransportationNo { get; set; }
    public Guid? GidTransportationExternalServiceFK { get; set; }
    public string TransportationExternalServiceFKTitle { get; set; }
    public Guid GidFeeCurrencyFK { get; set; }
    public string CurrencyFKName { get; set; }
    public EnumBalanceType BalanceType { get; set; }
    public EnumBalanceResourceType BalanceResourceType { get; set; }
    public DateTime ExpirationDate { get; set; }
    public decimal Fee { get; set; }
    public EnumPaymentStatus PaymentStatus { get; set; }
    public DateTime PaymentDate { get; set; }
    public string? PaymentFile { get; set; }
    public string? Description { get; set; }


}
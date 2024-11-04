using Core.Application.Dtos;
using Domain.Enums;

namespace Application.Features.SupplierCustomerManagementFeatures.SCCompanies.Queries.GetList;

public class GetListSCCompanyListItemDto : IDto
{
    public Guid Gid { get; set; }
    public string CompanyName { get; set; }
    public string? Phone { get; set; }
    public string? WebSite { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
    public bool WebLoginStatus { get; set; }
    public string? Description { get; set; }
    public string? SpecialNote { get; set; }
    public string? TaxOffice { get; set; }
    public string? TaxNumber { get; set; }
    public string? Keywords { get; set; }
    public EnumPartnerType PartnerType { get; set; }
    public int SupplierRank { get; set; }
    public int CustomerRank { get; set; }
    public bool IsHotel { get; set; }
    public EnumType Type { get; set; }
    public EnumStatus Status { get; set; }


}
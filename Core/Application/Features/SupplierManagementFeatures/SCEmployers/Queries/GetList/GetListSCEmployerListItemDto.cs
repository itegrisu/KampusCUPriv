using Core.Application.Dtos;

namespace Application.Features.SupplierCustomerManagementFeatures.SCEmployers.Queries.GetList;

public class GetListSCEmployerListItemDto : IDto
{
    public Guid Gid { get; set; }
    public Guid GidSCCompanyFK { get; set; }
    public string SCCompanyFKCompanyName { get; set; }
    public string FullName { get; set; }
    public string Duty { get; set; }
    public string? Phone { get; set; }
    public string? Email { get; set; }
    public string? SpecialNote { get; set; }


}
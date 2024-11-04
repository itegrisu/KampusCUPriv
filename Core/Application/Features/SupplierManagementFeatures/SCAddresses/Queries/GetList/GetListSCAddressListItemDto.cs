using Core.Application.Dtos;

namespace Application.Features.SupplierCustomerManagementFeatures.SCAddresses.Queries.GetList;

public class GetListSCAddressListItemDto : IDto
{
    public Guid Gid { get; set; }
    public Guid GidSCCompanyFK { get; set; }
    public string SCCompanyFKCompanyName { get; set; }
    public Guid GidCityFK { get; set; }
    public string CityFKName { get; set; }
    public string Title { get; set; }
    public string? District { get; set; }
    public string? PostalCode { get; set; }
    public string Address { get; set; }


}
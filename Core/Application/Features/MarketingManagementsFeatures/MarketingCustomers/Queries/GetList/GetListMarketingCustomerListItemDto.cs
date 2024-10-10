using Core.Application.Dtos;

namespace Application.Features.MarketingManagementFeatures.MarketingCustomers.Queries.GetList;

public class GetListMarketingCustomerListItemDto : IDto
{
    public Guid Gid { get; set; }

    public string FullName { get; set; }
    public string? Company { get; set; }
    public string Duty { get; set; }
    public string? PreviousDuty { get; set; }
    public string? Gsm { get; set; }
    public string? Email { get; set; }
    public string? Description { get; set; }


}
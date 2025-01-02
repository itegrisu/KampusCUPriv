using Core.Application.Dtos;

namespace Application.Features.SupplierCustomerManagementFeatures.SCWorkHistories.Queries.GetList;

public class GetListSCWorkHistoryListItemDto : IDto
{
    public Guid Gid { get; set; }
    public Guid GidSCCompanyFK { get; set; }
    public string SCCompanyFKCompanyName { get; set; }
    public string Title { get; set; }
    public string? Detail { get; set; }
    public DateTime? WorkDate { get; set; }
    public string? WorkFile { get; set; }


}
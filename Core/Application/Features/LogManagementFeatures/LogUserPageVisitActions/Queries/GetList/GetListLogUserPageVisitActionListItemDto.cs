using Core.Application.Dtos;
using Core.Enum;
using Domain.Entities.GeneralManagements;

namespace Application.Features.LogManagementFeatures.LogUserPageVisitActions.Queries.GetList;

public class GetListLogUserPageVisitActionListItemDto : IDto
{
    public Guid Gid { get; set; }
    public Guid GidUserFK { get; set; }
    public string UserFKFullName { get; set; }
    public string? IpAddress { get; set; }
    public string PageInfo { get; set; }
    public string OperationQuery { get; set; }
    public string JSonData { get; set; }
    public DateTime CreatedDate { get; set; }


}
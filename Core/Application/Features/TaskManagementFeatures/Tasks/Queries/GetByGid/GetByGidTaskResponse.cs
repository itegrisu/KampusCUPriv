using Core.Application.Responses;
using Domain.Enums;

namespace Application.Features.TaskManagementFeatures.Tasks.Queries.GetByGid;

public class GetByGidTaskResponse : IResponse
{
    public Guid Gid { get; set; }
    public string UserFKGid { get; set; }
    public string UserFKFullName { get; set; }
    public string Title { get; set; }
    public DateTime EndDate { get; set; }
    public string Description { get; set; }
    public EnumPriorityType PriorityType { get; set; }
}
using Application.Features.Base;
using Application.Features.TaskManagementFeatures.TaskGroups.Queries.GetByGid;
using Core.Application.Responses;

namespace Application.Features.TaskManagementFeatures.TaskGroups.Commands.Update;

public class UpdatedTaskGroupResponse : BaseResponse, IResponse
{
    public GetByGidTaskGroupResponse Obj { get; set; }
}
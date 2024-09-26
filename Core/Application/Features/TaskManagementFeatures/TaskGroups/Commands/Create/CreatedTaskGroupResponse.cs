using Application.Features.Base;
using Application.Features.TaskManagementFeatures.TaskGroups.Queries.GetByGid;
using Core.Application.Responses;

namespace Application.Features.TaskManagementFeatures.TaskGroups.Commands.Create;

public class CreatedTaskGroupResponse : BaseResponse, IResponse
{
    public GetByGidTaskGroupResponse Obj { get; set; }
}
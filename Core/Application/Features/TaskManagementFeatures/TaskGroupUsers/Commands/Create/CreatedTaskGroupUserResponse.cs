using Application.Features.Base;
using Application.Features.TaskManagementFeatures.TaskGroupUsers.Queries.GetByGid;
using Core.Application.Responses;

namespace Application.Features.TaskManagementFeatures.TaskGroupUsers.Commands.Create;

public class CreatedTaskGroupUserResponse : BaseResponse, IResponse
{
    public GetByGidTaskGroupUserResponse Obj { get; set; }
}
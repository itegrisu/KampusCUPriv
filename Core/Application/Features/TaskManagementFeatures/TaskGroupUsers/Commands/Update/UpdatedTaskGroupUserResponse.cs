using Application.Features.Base;
using Application.Features.TaskManagementFeatures.TaskGroupUsers.Queries.GetByGid;
using Core.Application.Responses;

namespace Application.Features.TaskManagementFeatures.TaskGroupUsers.Commands.Update;

public class UpdatedTaskGroupUserResponse : BaseResponse, IResponse
{
    public GetByGidTaskGroupUserResponse Obj { get; set; }
}
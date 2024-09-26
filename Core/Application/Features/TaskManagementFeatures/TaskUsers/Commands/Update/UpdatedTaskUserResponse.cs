using Application.Features.Base;
using Application.Features.TaskManagementFeatures.TaskUsers.Queries.GetByGid;
using Core.Application.Responses;

namespace Application.Features.TaskManagementFeatures.TaskUsers.Commands.Update;

public class UpdatedTaskUserResponse : BaseResponse, IResponse
{
    public GetByGidTaskUserResponse Obj { get; set; }
}
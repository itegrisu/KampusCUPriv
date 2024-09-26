using Application.Features.Base;
using Application.Features.TaskManagementFeatures.TaskUsers.Queries.GetByGid;
using Core.Application.Responses;

namespace Application.Features.TaskManagementFeatures.TaskUsers.Commands.Create;

public class CreatedTaskUserResponse : BaseResponse, IResponse
{
    public GetByGidTaskUserResponse Obj { get; set; }
}
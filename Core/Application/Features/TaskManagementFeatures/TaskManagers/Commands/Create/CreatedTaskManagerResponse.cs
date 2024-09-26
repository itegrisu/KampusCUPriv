using Application.Features.Base;
using Application.Features.TaskManagementFeatures.TaskManagers.Queries.GetByGid;
using Core.Application.Responses;

namespace Application.Features.TaskManagementFeatures.TaskManagers.Commands.Create;

public class CreatedTaskManagerResponse : BaseResponse, IResponse
{
    public GetByGidTaskManagerResponse Obj { get; set; }
}
using Application.Features.Base;
using Application.Features.GeneralManagementFeatures.Users.Queries.GetByGid;
using Core.Application.Responses;


namespace Application.Features.GeneralManagementFeatures.Users.Commands.Create;

public class CreatedUserResponse : BaseResponse, IResponse
{
    public GetByGidUserResponse Obj { get; set; }
}
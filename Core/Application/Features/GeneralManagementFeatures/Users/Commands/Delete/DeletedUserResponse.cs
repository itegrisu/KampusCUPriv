using Application.Features.Base;
using Application.Features.GeneralManagementFeatures.Users.Queries.GetByGid;
using Core.Application.Responses;


namespace Application.Features.GeneralManagementFeatures.Users.Commands.Delete;

public class DeletedUserResponse : BaseResponse, IResponse
{
    public GetByGidUserResponse Obj { get; set; }

}
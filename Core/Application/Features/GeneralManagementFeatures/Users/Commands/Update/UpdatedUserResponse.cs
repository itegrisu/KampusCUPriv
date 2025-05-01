using Application.Features.Base;
using Application.Features.GeneralFeatures.Users.Queries.GetByGid;
using Core.Application.Responses;

namespace Application.Features.GeneralFeatures.Users.Commands.Update;

public class UpdatedUserResponse : BaseResponse, IResponse
{
    public GetByGidUserResponse Obj { get; set; }
}
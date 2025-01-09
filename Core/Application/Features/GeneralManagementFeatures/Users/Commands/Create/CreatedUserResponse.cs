using Application.Features.Base;
using Application.Features.GeneralFeatures.Users.Queries.GetByGid;
using Core.Application.Responses;
using System.Configuration;

namespace Application.Features.GeneralFeatures.Users.Commands.Create;

public class CreatedUserResponse : BaseResponse, IResponse
{
    public GetByGidUserResponse Obj { get; set; }
}
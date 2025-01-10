using Application.Features.Base;
using Application.Features.GeneralFeatures.Admins.Queries.GetByGid;
using Core.Application.Responses;
using System.Configuration;

namespace Application.Features.GeneralFeatures.Admins.Commands.Create;

public class CreatedAdminResponse : BaseResponse, IResponse
{
    public GetByGidAdminResponse Obj { get; set; }
}
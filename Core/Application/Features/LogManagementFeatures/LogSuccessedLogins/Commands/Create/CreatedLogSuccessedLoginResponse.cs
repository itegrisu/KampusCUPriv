using Application.Features.Base;
using Application.Features.LogManagementFeatures.LogSuccessedLogins.Queries.GetByGid;
using Core.Application.Responses;
using System.Configuration;

namespace Application.Features.LogManagementFeatures.LogSuccessedLogins.Commands.Create;

public class CreatedLogSuccessedLoginResponse : BaseResponse, IResponse
{
    public GetByGidLogSuccessedLoginResponse Obj { get; set; }
}
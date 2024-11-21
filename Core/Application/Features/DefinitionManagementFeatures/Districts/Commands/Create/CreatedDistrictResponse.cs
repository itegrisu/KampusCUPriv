using Application.Features.Base;
using Application.Features.DefinitionManagementFeatures.Districts.Queries.GetByGid;
using Core.Application.Responses;
using System.Configuration;

namespace Application.Features.DefinitionManagementFeatures.Districts.Commands.Create;

public class CreatedDistrictResponse : BaseResponse, IResponse
{
    public GetByGidDistrictResponse Obj { get; set; }
}
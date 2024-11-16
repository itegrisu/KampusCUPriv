using Application.Features.Base;
using Application.Features.DefinitionManagementFeatures.TyreTypes.Queries.GetByGid;
using Core.Application.Responses;
using System.Configuration;

namespace Application.Features.DefinitionManagementFeatures.TyreTypes.Commands.Create;

public class CreatedTyreTypeResponse : BaseResponse, IResponse
{
    public GetByGidTyreTypeResponse Obj { get; set; }
}
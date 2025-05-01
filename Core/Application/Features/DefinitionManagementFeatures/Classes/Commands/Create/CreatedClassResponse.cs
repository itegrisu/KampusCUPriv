using Application.Features.Base;
using Application.Features.DefinitionFeatures.Classes.Queries.GetByGid;
using Core.Application.Responses;
using System.Configuration;

namespace Application.Features.DefinitionFeatures.Classes.Commands.Create;

public class CreatedClassResponse : BaseResponse, IResponse
{
    public GetByGidClassResponse Obj { get; set; }
}
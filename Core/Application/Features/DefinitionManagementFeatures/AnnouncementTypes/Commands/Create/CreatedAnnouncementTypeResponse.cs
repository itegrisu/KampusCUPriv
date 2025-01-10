using Application.Features.Base;
using Application.Features.DefinitionFeatures.AnnouncementTypes.Queries.GetByGid;
using Core.Application.Responses;
using System.Configuration;

namespace Application.Features.DefinitionFeatures.AnnouncementTypes.Commands.Create;

public class CreatedAnnouncementTypeResponse : BaseResponse, IResponse
{
    public GetByGidAnnouncementTypeResponse Obj { get; set; }
}
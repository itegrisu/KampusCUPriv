using Application.Features.Base;
using Application.Features.DefinitionFeatures.AnnouncementTypes.Queries.GetByGid;
using Core.Application.Responses;

namespace Application.Features.DefinitionFeatures.AnnouncementTypes.Commands.Update;

public class UpdatedAnnouncementTypeResponse : BaseResponse, IResponse
{
    public GetByGidAnnouncementTypeResponse Obj { get; set; }
}
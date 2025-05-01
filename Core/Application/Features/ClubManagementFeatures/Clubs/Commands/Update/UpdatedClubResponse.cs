using Application.Features.Base;
using Application.Features.ClubFeatures.Clubs.Queries.GetByGid;
using Core.Application.Responses;

namespace Application.Features.ClubFeatures.Clubs.Commands.Update;

public class UpdatedClubResponse : BaseResponse, IResponse
{
    public GetByGidClubResponse Obj { get; set; }
}
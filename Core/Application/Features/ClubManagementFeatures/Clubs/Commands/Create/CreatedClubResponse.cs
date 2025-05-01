using Application.Features.Base;
using Application.Features.ClubFeatures.Clubs.Queries.GetByGid;
using Core.Application.Responses;
using System.Configuration;

namespace Application.Features.ClubFeatures.Clubs.Commands.Create;

public class CreatedClubResponse : BaseResponse, IResponse
{
    public GetByGidClubResponse Obj { get; set; }
}
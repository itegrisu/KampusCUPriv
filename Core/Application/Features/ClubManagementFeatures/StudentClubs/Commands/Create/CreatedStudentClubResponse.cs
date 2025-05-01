using Application.Features.Base;
using Application.Features.ClubFeatures.StudentClubs.Queries.GetByGid;
using Core.Application.Responses;
using System.Configuration;

namespace Application.Features.ClubFeatures.StudentClubs.Commands.Create;

public class CreatedStudentClubResponse : BaseResponse, IResponse
{
    public GetByGidStudentClubResponse Obj { get; set; }
}
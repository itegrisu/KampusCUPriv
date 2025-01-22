using Application.Features.Base;
using Application.Features.ClubFeatures.StudentClubs.Queries.GetByGid;
using Core.Application.Responses;

namespace Application.Features.ClubFeatures.StudentClubs.Commands.Update;

public class UpdatedStudentClubResponse : BaseResponse, IResponse
{
    public GetByGidStudentClubResponse Obj { get; set; }
}
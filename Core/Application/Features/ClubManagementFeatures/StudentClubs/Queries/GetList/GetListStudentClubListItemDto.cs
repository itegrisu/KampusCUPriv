using Core.Application.Dtos;
using Core.Enum;

namespace Application.Features.ClubFeatures.StudentClubs.Queries.GetList;

public class GetListStudentClubListItemDto : IDto
{
    public Guid Gid { get; set; }
    public Guid GidUserFK { get; set; }
    public string UserFKName { get; set; }
    public Guid GidClubFK { get; set; }
    public string ClubFKName { get; set; }
    public string ClubFKLogo { get; set; }
}
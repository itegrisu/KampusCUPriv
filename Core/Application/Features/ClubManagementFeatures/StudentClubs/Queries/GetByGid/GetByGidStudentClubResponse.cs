using Core.Application.Responses;

namespace Application.Features.ClubFeatures.StudentClubs.Queries.GetByGid
{
    public class GetByGidStudentClubResponse : IResponse
    {
        public Guid Gid { get; set; }
        public Guid GidUserFK { get; set; }
        public string UserFKName { get; set; }
        public Guid GidClubFK { get; set; }
        public string ClubFKName { get; set; }
        public string ClubFKLogo { get; set; }
    }
}
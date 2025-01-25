using Core.Application.Responses;

namespace Application.Features.ClubFeatures.Clubs.Queries.GetByGid
{
    public class GetByGidClubResponse : IResponse
    {
        public Guid Gid { get; set; }
        public Guid? GidManagerFK { get; set; }
        public string UserFKName { get; set; }
        public Guid GidCategoryFK { get; set; }
        public string CategoryFKName { get; set; }
        public string Name { get; set; }
        public string? Logo { get; set; }
        public string? Description { get; set; }
        public string? Color { get; set; }
    }
}
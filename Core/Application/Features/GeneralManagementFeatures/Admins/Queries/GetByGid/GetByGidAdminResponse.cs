using Core.Application.Responses;

namespace Application.Features.GeneralFeatures.Admins.Queries.GetByGid
{
    public class GetByGidAdminResponse : IResponse
    {
        public Guid Gid { get; set; }
        public Guid GidClubFK { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
using Core.Application.Responses;

namespace Application.Features.LogManagementFeatures.LogSuccessedLogins.Queries.GetByGid
{
    public class GetByGidLogSuccessedLoginResponse : IResponse
    {
        public Guid Gid { get; set; }
        public Guid GidUserFK { get; set; }
        public string UserFKFullName{ get; set; }
        public string? IpAddress { get; set; }
        public string SessionId { get; set; }
        public DateTime LogOutDate { get; set; }

    }
}
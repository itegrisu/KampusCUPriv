using Core.Application.Responses;

namespace Application.Features.LogManagementFeatures.LogFailedLogins.Queries.GetByGid
{
    public class GetByGidLogFailedLoginResponse : IResponse
    {
        public Guid Gid { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string? IpAddress { get; set; }
        public string? Description { get; set; }

    }
}
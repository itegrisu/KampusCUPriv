using Core.Application.Responses;

namespace Application.Features.AccommodationManagementFeatures.PartTimeWorkers.Queries.GetByGid
{
    public class GetByGidPartTimeWorkerResponse : IResponse
    {
        public Guid Gid { get; set; }

        public string IdentityNo { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string PasswordHash { get; set; }
        public bool IsLoginStatus { get; set; }
        public string Gsm { get; set; }
        public DateTime BirthDate { get; set; }

    }
}
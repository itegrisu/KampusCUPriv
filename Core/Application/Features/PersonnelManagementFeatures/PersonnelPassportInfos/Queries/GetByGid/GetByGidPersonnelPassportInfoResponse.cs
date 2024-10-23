using Core.Application.Responses;

namespace Application.Features.PersonnelManagementFeatures.PersonnelPassportInfos.Queries.GetByGid
{
    public class GetByGidPersonnelPassportInfoResponse : IResponse
    {
        public Guid Gid { get; set; }
        public Guid GidPersonnelFK { get; set; }
        public string UserFKFullName { get; set; }
        public string PassportNo { get; set; }
        public DateTime DateOfIssue { get; set; }
        public DateTime ValidityDate { get; set; }
        public string? Document { get; set; }

    }
}
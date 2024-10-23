using Core.Application.Responses;

namespace Application.Features.PersonnelManagementFeatures.PersonnelResidenceInfos.Queries.GetByGid
{
    public class GetByGidPersonnelResidenceInfoResponse : IResponse
    {
        public Guid Gid { get; set; }
        public Guid GidPersonnelFK { get; set; }
        public string UserFKFullName { get; set; }
        public string SessionSerialNo { get; set; }
        public DateTime DateOfIssue { get; set; }
        public DateTime ValidityDate { get; set; }
        public string? Document { get; set; }




    }
}
using Core.Application.Responses;

namespace Application.Features.PersonnelManagementFeatures.PersonnelPermitInfos.Queries.GetByGid
{
    public class GetByGidPersonnelPermitInfoResponse : IResponse
    {
        public Guid Gid { get; set; }
        public Guid GidPersonnelFK { get; set; }
        public string UserFKFullName { get; set; }
        public Guid GidPermitFK { get; set; }
        public string PermitTypeFKName { get; set; }

        public DateTime PermitStartDate { get; set; }
        public DateTime PermitEndDate { get; set; }
        public string? Document { get; set; }
        public string? Description { get; set; }


    }
}
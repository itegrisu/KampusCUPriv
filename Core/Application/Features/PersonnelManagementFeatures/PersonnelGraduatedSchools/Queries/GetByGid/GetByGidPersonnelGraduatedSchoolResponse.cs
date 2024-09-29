using Core.Application.Responses;
using Domain.Enums;

namespace Application.Features.PersonnelManagementFeatures.PersonnelGraduatedSchools.Queries.GetByGid
{
    public class GetByGidPersonnelGraduatedSchoolResponse : IResponse
    {
        public Guid Gid { get; set; }
        public Guid GidPersonnelFK { get; set; }
        public string UserFKFullName { get; set; }
        public EnumEducationalInstitutionType EducationalInstitutionType { get; set; }
        public string SchoolInfo { get; set; }
        public string DepartmentInfo { get; set; }
        public int StartYear { get; set; }
        public DateTime GraduationDate { get; set; }
        public string? Document { get; set; }
        public string? Description { get; set; }

    }
}
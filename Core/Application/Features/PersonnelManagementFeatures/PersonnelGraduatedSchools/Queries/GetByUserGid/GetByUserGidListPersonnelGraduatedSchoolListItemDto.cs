using Core.Application.Dtos;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.PersonnelManagementFeatures.PersonnelGraduatedSchools.Queries.GetByUserGid
{
    public class GetByUserGidListPersonnelGraduatedSchoolListItemDto : IDto 
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

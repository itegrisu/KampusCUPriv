using Core.Entities;
using Domain.Entities.GeneralManagements;
using Domain.Enums;

namespace Domain.Entities.PersonnelManagements
{
    public class PersonnelGraduatedSchool : BaseEntity
    {
        public Guid GidPersonnelFK { get; set; }
        public User UserFK { get; set; }
        public EnumEducationalInstitutionType EducationalInstitutionType { get; set; }
        public string SchoolInfo { get; set; } = string.Empty;
        public string DepartmentInfo { get; set; } = string.Empty;
        public int StartYear { get; set; }
        public DateTime? GraduationDate { get; set; }
        public string? Document { get; set; }
        public string? Description { get; set; }


    }
}

using Core.Entities;
using Domain.Entities.DefinitionManagements;
using Domain.Entities.GeneralManagements;

namespace Domain.Entities.PersonnelManagements
{
    public class PersonnelPermitInfo : BaseEntity
    {
        public Guid GidPersonnelFK { get; set; }
        public User UserFK { get; set; }
        public Guid GidPermitFK { get; set; }
        public PermitType PermitTypeFK { get; set; }
        public DateTime PermitStartDate { get; set; }
        public DateTime PermitEndDate { get; set; }
        public string? Document { get; set; }
        public string? Description { get; set; }


    }
}

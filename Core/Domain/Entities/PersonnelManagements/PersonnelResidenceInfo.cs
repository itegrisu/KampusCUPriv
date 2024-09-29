using Core.Entities;
using Domain.Entities.GeneralManagements;

namespace Domain.Entities.PersonnelManagements
{
    public class PersonnelResidenceInfo : BaseEntity
    {

        public Guid GidPersonnelFK { get; set; }
        public User UserFK { get; set; }
        public string SessionSerialNo { get; set; } = string.Empty;
        public DateTime DateOfIssue { get; set; }
        public DateTime ValidityDate { get; set; }
        public string? Document { get; set; }
        public string? Description { get; set; }

    }
}

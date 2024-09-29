using Core.Entities;
using Domain.Entities.DefinitionManagements;
using Domain.Entities.GeneralManagements;

namespace Domain.Entities.PersonnelManagements
{
    public class PersonnelDocument : BaseEntity
    {
        public Guid GidPersonnelFK { get; set; }
        public User UserFK { get; set; }
        public Guid GidDocumentType { get; set; }
        public DocumentType DocumentTypeFK { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime? ValidityDate { get; set; }
        public string? Document { get; set; }
        public string? Description { get; set; }

    }
}

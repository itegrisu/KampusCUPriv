using Core.Entities;
using Domain.Entities.PersonnelManagements;
using Domain.Entities.VehicleManagements;

namespace Domain.Entities.DefinitionManagements
{
    public class DocumentType : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public ICollection<PersonnelDocument>? PersonnelDocuments { get; set; }
        public ICollection<VehicleDocument>? VehicleDocuments { get; set; }
    }
}

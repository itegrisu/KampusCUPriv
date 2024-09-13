using Core.Entities;
using Domain.Entities.PersonnelManagements;

namespace Domain.Entities.DefinitionManagements
{
    public class DocumentType : BaseEntity
    {
        public string BelgeAdi { get; set; } = string.Empty;
        public ICollection<PersonnelDocument>? PersonnelDocuments { get; set; }

    }
}

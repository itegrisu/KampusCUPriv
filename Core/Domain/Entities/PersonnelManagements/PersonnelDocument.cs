using Core.Entities;
using Domain.Entities.DefinitionManagements;
using Domain.Entities.GeneralManagements;

namespace Domain.Entities.PersonnelManagements
{
    public class PersonnelDocument : BaseEntity
    {
        public Guid GidPersonelFK { get; set; }
        public User UserFK { get; set; }
        public Guid GidBelgeTuru { get; set; }
        public DocumentType DocumentTypeFK { get; set; }
        public string BelgeAdi { get; set; } = string.Empty;
        public DateTime? GecerlilikTarihi { get; set; }
        public string? Belge { get; set; }
        public string? Aciklama { get; set; }


    }
}

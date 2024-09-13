using Core.Entities;
using Domain.Entities.PersonnelManagements;

namespace Domain.Entities.DefinitionManagements
{
    public class ForeignLanguage : BaseEntity
    {
        public string DilAdi { get; set; } = string.Empty;
        public string? DilKodu { get; set; }
        public ICollection<PersonnelForeignLanguage>? PersonnelForeignLanguages { get; set; }

    }
}

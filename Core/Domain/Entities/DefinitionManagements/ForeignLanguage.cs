using Core.Entities;
using Domain.Entities.AccommodationManagements;
using Domain.Entities.PersonnelManagements;

namespace Domain.Entities.DefinitionManagements
{
    public class ForeignLanguage : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string? LanguageCode { get; set; }
        public ICollection<PersonnelForeignLanguage>? PersonnelForeignLanguages { get; set; }
        public ICollection<PartTimeWorkerForeignLanguage>? PartTimeWorkerForeignLanguages { get; set; }
    }
}

using Core.Entities;
using Domain.Entities.DefinitionManagements;
using Domain.Entities.GeneralManagements;
using Domain.Enums;

namespace Domain.Entities.PersonnelManagements
{

    public class PersonnelForeignLanguage : BaseEntity
    {

        public Guid GidPersonelFK { get; set; }
        public User UserFK { get; set; }
        public Guid GidLanguageFK { get; set; }
        public ForeignLanguage ForeignLanguageFK { get; set; }
        public EnumKonusmaDuzeyi KonusmaDuzeyi { get; set; }
        public EnumOkumaDuzeyi OkumaDuzeyi { get; set; }


    }

}

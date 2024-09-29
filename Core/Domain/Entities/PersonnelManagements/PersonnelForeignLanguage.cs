using Core.Entities;
using Domain.Entities.DefinitionManagements;
using Domain.Entities.GeneralManagements;
using Domain.Enums;

namespace Domain.Entities.PersonnelManagements
{

    public class PersonnelForeignLanguage : BaseEntity
    {

        public Guid GidPersonnelFK { get; set; }
        public User UserFK { get; set; }
        public Guid GidLanguageFK { get; set; }
        public ForeignLanguage ForeignLanguageFK { get; set; }

        public EnumLanguageLevel SpeakingLevel { get; set; }
        public EnumLanguageLevel ReadLevel { get; set; }

    }

}

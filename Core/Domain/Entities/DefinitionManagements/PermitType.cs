using Core.Entities;
using Domain.Entities.PersonnelManagements;

namespace Domain.Entities.DefinitionManagements
{
    public class PermitType : BaseEntity
    {
        public string IzinAdi { get; set; } = string.Empty;
        public ICollection<PersonnelPermitInfo>? PersonnelPermitInfos { get; set; }

    }
}

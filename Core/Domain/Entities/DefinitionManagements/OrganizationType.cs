using Core.Entities;
using Domain.Entities.OrganizationManagements;

namespace Domain.Entities.DefinitionManagements
{
    public class OrganizationType : BaseEntity
    {


        public string Name { get; set; } = string.Empty;

        public ICollection<Organization>? Organizations { get; set; }

    }
}

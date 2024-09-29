using Core.Entities;
using Domain.Entities.GeneralManagements;

namespace Domain.Entities.DefinitionManagements
{
    public class Country : BaseEntity, IHasRowNo
    {

        public string Name { get; set; } = string.Empty;
        public string CountryCode { get; set; } = string.Empty;
        public string? PhoneCode { get; set; }
        public int RowNo { get; set; }

        public ICollection<User>? Users { get; set; }
        public ICollection<City>? Cities { get; set; }

    }
}

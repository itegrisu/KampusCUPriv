using Core.Entities;
using Domain.Entities.GeneralManagements;

namespace Domain.Entities.DefinitionManagements
{
    public class Country : BaseEntity, IHasRowNo
    {

        public string UlkeAdi { get; set; } = string.Empty;
        public string UlkeKodu { get; set; } = string.Empty;
        public string? TelefonKodu { get; set; }
        public int RowNo { get; set; }

        public ICollection<User>? Users { get; set; }
        public ICollection<City>? Cities { get; set; }

    }
}

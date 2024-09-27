using Core.Entities;
using Domain.Entities.SupplierCustomerManagements;

namespace Domain.Entities.DefinitionManagements
{
    public class Currency : BaseEntity
    {
        public string DovizAdi { get; set; } = string.Empty;
        public string? DovizKodu { get; set; }
        public string? DovizSimgesi { get; set; }
        public ICollection<SCBank>? SCBanks { get; set; }
    }
}

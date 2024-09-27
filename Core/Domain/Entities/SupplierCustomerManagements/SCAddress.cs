using Core.Entities;
using Domain.Entities.DefinitionManagements;

namespace Domain.Entities.SupplierCustomerManagements
{
    public class SCAddress : BaseEntity
    {

        public Guid GidSCCompanyFK { get; set; }
        public SCCompany SCCompanyFK { get; set; }
        public Guid GidCityFK { get; set; }
        public City CityFK { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? District { get; set; }
        public string? PostalCode { get; set; }
        public string Address { get; set; } = string.Empty;


    }
}

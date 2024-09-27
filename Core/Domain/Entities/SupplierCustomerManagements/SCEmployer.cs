using Core.Entities;

namespace Domain.Entities.SupplierCustomerManagements
{
    public class SCEmployer : BaseEntity
    {

        public Guid GidSCCompanyFK { get; set; }
        public SCCompany SCCompanyFK { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Duty { get; set; } = string.Empty;
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? SpecialNote { get; set; }


    }
}

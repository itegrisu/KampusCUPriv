using Core.Entities;

namespace Domain.Entities.SupplierCustomerManagements
{
    public class SCWorkHistory : BaseEntity
    {

        public Guid GidSCCompanyFK { get; set; }
        public SCCompany SCCompanyFK { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Detail { get; set; }
        public DateTime? WorkDate { get; set; }
        public string? WorkFile { get; set; }


    }
}

using Core.Application.Responses;

namespace Application.Features.GeneralManagementFeatures.Departments.Queries.GetByGid
{
    public class GetByGidDepartmentResponse : IResponse
    {
        public Guid Gid { get; set; }
        public Guid GidAsilYoneticiFK { get; set; }
        public string AsilYoneticiFKTamAd { get; set; }
        public Guid? GidYedekYoneticiFK { get; set; }
        public string YedekYoneticiFKTamAd { get; set; }
        public string DepartmanAdi { get; set; }
        public string? Detay { get; set; }

    }
}
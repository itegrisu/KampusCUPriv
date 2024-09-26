using Core.Application.Responses;

namespace Application.Features.PersonnelManagementFeatures.PersonnelResidenceInfos.Queries.GetByGid
{
    public class GetByGidPersonnelResidenceInfoResponse : IResponse
    {
        public Guid Gid { get; set; }
        public Guid GidPersonelFK { get; set; }
        public string UserFKTamAd { get; set; }
        public string OturumSeriNo { get; set; }
        public DateTime VerilisTarihi { get; set; }
        public DateTime GecerlilikTarihi { get; set; }
        public string? Belge { get; set; }
        public string? Aciklama { get; set; }

    }
}
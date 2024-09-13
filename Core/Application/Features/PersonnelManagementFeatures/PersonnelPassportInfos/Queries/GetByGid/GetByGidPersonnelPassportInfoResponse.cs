using Core.Application.Responses;

namespace Application.Features.PersonnelManagementFeatures.PersonnelPassportInfos.Queries.GetByGid
{
    public class GetByGidPersonnelPassportInfoResponse : IResponse
    {
        public Guid Gid { get; set; }
        public Guid GidPersonelFK { get; set; }
        public string UserFKTamAd { get; set; }
        public string PasaportNo { get; set; }
        public DateTime VerilisTarihi { get; set; }
        public DateTime GecerlilikTarihi { get; set; }
        public string? Belge { get; set; }
        public string? Aciklama { get; set; }

    }
}
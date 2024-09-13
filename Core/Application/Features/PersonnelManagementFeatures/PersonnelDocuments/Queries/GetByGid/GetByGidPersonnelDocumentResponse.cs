using Core.Application.Responses;

namespace Application.Features.PersonnelManagementFeatures.PersonnelDocuments.Queries.GetByGid
{
    public class GetByGidPersonnelDocumentResponse : IResponse
    {
        public Guid Gid { get; set; }
        public Guid GidPersonelFK { get; set; }
        public string UserFKTamAd { get; set; }
        public Guid GidBelgeTuru { get; set; }
        public string DocumentTypeFKBelgeAdi { get; set; }
        public string BelgeAdi { get; set; }
        public DateTime GecerlilikTarihi { get; set; }
        public string? Belge { get; set; }
        public string? Aciklama { get; set; }

    }
}
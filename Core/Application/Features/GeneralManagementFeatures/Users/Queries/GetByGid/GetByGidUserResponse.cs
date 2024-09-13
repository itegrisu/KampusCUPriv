using Core.Application.Responses;
using Domain.Enums;

namespace Application.Features.GeneralManagementFeatures.Users.Queries.GetByGid
{
    public class GetByGidUserResponse : IResponse
    {
        public Guid Gid { get; set; }
        public Guid? GidUyrukFK { get; set; }
        public string CountryFKName { get; set; }
        public string Adi { get; set; }
        public string Soyadi { get; set; }
        public string EPosta { get; set; }
        public string? Unvani { get; set; }
        public string? ProfilResmi { get; set; }
        public bool AktifHesapMi { get; set; }
        public bool SistemAdminMi { get; set; }
        public string Gsm { get; set; }
        public string? DogumYeri { get; set; }
        public DateTime DogumTarihi { get; set; }
        public string? KimlikNo { get; set; }
        public string? PasaportNo { get; set; }
        public string? SGKNo { get; set; }
        public string? EhliyetNo { get; set; }
        public string? Not { get; set; }
        public EnumMedeniDurumu? MedeniDurumu { get; set; }
        public EnumKanGrubu? KanGrubu { get; set; }
        public EnumCinsiyet Cinsiyet { get; set; }
        public EnumEMailAktivasyonDurumu EMailAktivasyonDurumu { get; set; }
        public EnumSmsAktivasyonDurumu SmsAktivasyonDurumu { get; set; }
    }
}
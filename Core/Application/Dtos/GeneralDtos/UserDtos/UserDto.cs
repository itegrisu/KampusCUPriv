using Domain.Entities.DefinitionManagements;
using Domain.Enums;

namespace Application.Dtos.GeneralDtos.UserDtos
{
    public class UserDto
    {
        public Guid? GidUyrukFK { get; set; }
        public Country? CountryFK { get; set; }
        public string Adi { get; set; } = string.Empty;
        public string Soyadi { get; set; } = string.Empty;
        public string EPosta { get; set; } = string.Empty;
        public string? Unvani { get; set; }
        public string Sifre { get; set; } = string.Empty;
        public string SifreHash { get; set; } = string.Empty;
        public string SifreGuncellemeToken { get; set; } = string.Empty;
        public DateTime? TokenGecerlilikSuresi { get; set; }
        public string? ProfilResmi { get; set; }
        public bool AktifHesapMi { get; set; }
        public bool SistemAdminMi { get; set; }
        public string Gsm { get; set; } = string.Empty;
        public string? DogumYeri { get; set; }
        public DateTime? DogumTarihi { get; set; }
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

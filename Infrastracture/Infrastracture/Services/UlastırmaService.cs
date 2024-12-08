using Application.Abstractions;
using System.Net;
using System.ServiceModel;
using UlasımSoapTest; // bu test için kutuphanedir.
//using UlasimSoap;  // bu canlı için kutuphanedir. 

namespace Infrastracture.Services
{
    public class UlastirmaService : IUlasımService
    {
        public string TestUserName { get; } = "999999";
        public string TestPassword { get; } = "999999testtest";
        public string UserName { get; } = "595160";
        public string Password { get; } = "Z7A45FK8JN";

        private bool uestsLive = false;     //false ise test true ise canli (sunucu)

        public UdhbUetdsAriziServiceClient UedtsService()
        {

            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
            UdhbUetdsAriziServiceClient client = new UdhbUetdsAriziServiceClient();
            client.Endpoint.Binding = GetBinding();

            client.ClientCredentials.ServiceCertificate.Authentication.CertificateValidationMode = System.ServiceModel.Security.X509CertificateValidationMode.None;

            if (uestsLive)
            {
                client.ClientCredentials.UserName.UserName = UserName;
                client.ClientCredentials.UserName.Password = Password;
            }

            else
            {
                client.ClientCredentials.UserName.UserName = TestUserName;
                client.ClientCredentials.UserName.Password = TestPassword;
            }
            return client;
        }
        private static BasicHttpBinding GetBinding()  // bu ne bilmiyoruz hatayı cözdü.
        {
            var binding = new BasicHttpBinding
            {
                Security = new BasicHttpSecurity
                {
                    Mode = BasicHttpSecurityMode.Transport, // HTTPS için Transport
                    Transport = new HttpTransportSecurity
                    {
                        ClientCredentialType = HttpClientCredentialType.Basic // Basic Authentication
                    }
                },
                MaxReceivedMessageSize = 65536 // Gerekirse artırılabilir
            };

            return binding;
        }

        public uetdsYtsUser User()
        {
            uetdsYtsUser user = new uetdsYtsUser();

            if (uestsLive)
            {
                user.kullaniciAdi = UserName;
                user.sifre = Password;
            }
            else
            {
                user.kullaniciAdi = TestUserName;
                user.sifre = TestPassword;
            }

            return user;
        }

        public async Task TestService()
        {
            var result = await UedtsService().servisTestAsync("Test mesajı");

            var elma = result.servisTestResponse1;

            Console.WriteLine(elma);
        }


        public async Task<string> IpListesiAsync()
        {
            var result = await UedtsService().ipListeleAsync(User());

            if (result.@return.sonucKodu == 0)
                return "OK";        //Başarılysa direkt seferRefNo Gönderilecek
            else
                return "HATA ";     //HATA MESAJI
        }


        #region Metodlar

        //public async Task<string> SeferEkleAsync(UlasimSeferleri ulasimSeferi)  //Ulasım seferleri dto oluşturulmalı.
        //{
        //    uetdsAriziSeferBilgileriInput seferBilgileriInput = new uetdsAriziSeferBilgileriInput();

        //    seferBilgileriInput.aracPlaka = ulasimSeferi.AracFK.Plaka;
        //    seferBilgileriInput.aracTelefonu = ulasimSeferi.AracTelefonNumarasi;
        //    seferBilgileriInput.firmaSeferNo = ulasimSeferi.SeferNo;
        //    seferBilgileriInput.hareketTarihi = ulasimSeferi.BaslamaTarihi;
        //    seferBilgileriInput.hareketSaati = ulasimSeferi.BaslamaTarihi.ToString("HH:mm");
        //    seferBilgileriInput.seferBitisTarihi = ulasimSeferi.BitisTarihi;
        //    seferBilgileriInput.seferBitisSaati = ulasimSeferi.BitisTarihi.ToString("HH:mm");
        //    seferBilgileriInput.seferAciklama = ulasimSeferi.Aciklama;


        //    var result = await UedtsService().seferEkleAsync(User(), seferBilgileriInput);

        //    long seferRefNo = result.@return.uetdsSeferReferansNo;
        //    int sonucKodu = result.@return.sonucKodu;
        //    string sonucMesaji = result.@return.sonucMesaji;

        //    if (sonucKodu == 0)
        //        return seferRefNo.ToString(); //Başarılysa direkt seferRefNo Gönderilecek
        //    else
        //        return "HATA " + sonucMesaji; //HATA MESAJI
        //}

        //public string SeferGuncelle(UlasimSeferleri ulasimSeferi)
        //{
        //    uetdsAriziSeferBilgileriInput seferBilgileriInput = new uetdsAriziSeferBilgileriInput();

        //    seferBilgileriInput.aracPlaka = ulasimSeferi.AracFK.Plaka;
        //    seferBilgileriInput.aracTelefonu = ulasimSeferi.AracTelefonNumarasi;
        //    seferBilgileriInput.firmaSeferNo = ulasimSeferi.SeferNo;
        //    seferBilgileriInput.hareketSaati = ulasimSeferi.BaslamaTarihi.ToString("HH:mm");
        //    seferBilgileriInput.hareketTarihi = ulasimSeferi.BaslamaTarihi;
        //    seferBilgileriInput.seferAciklama = ulasimSeferi.Aciklama;
        //    seferBilgileriInput.seferBitisSaati = ulasimSeferi.BitisTarihi.ToString("HH:mm");
        //    seferBilgileriInput.seferBitisTarihi = ulasimSeferi.BitisTarihi;

        //    var res = UedtsService().seferGuncelle(User(), long.Parse(ulasimSeferi.SeferRefNo), true, seferBilgileriInput);

        //    long seferRefNo = res.uetdsSeferReferansNo;
        //    int sonucKodu = res.sonucKodu;
        //    string sonucMesaji = res.sonucMesaji;

        //    if (sonucKodu == 0)
        //        return seferRefNo.ToString(); //Başarılysa direkt seferRefNo Gönderilecek
        //    else
        //        return "HATA " + sonucMesaji; //HATA MESAJI
        //}

        //public string PersonelEkle(List<UlasimSeferPersonelleri> personeller, long seferRefNumber)
        //{
        //    List<string> hataliPersoneller = new List<string>();

        //    foreach (UlasimSeferPersonelleri personel in personeller)
        //    {
        //        List<uetdsAriziSeferPersonelBilgileriInput> personelListesi = new List<uetdsAriziSeferPersonelBilgileriInput>();

        //        uetdsAriziSeferPersonelBilgileriInput personelBilgileriInput = new uetdsAriziSeferPersonelBilgileriInput();

        //        if (personel.EnumPersonelTuru == UlasimSeferPersonelleri.PersonelTuruEnum.AsliPersonel)
        //        {
        //            Yonetici yonetici = YoneticiBO.Instance.GetirYoneticiObjectId(personel.IdGorevliPersonelFK);
        //            Ulkeler ulke = UlkelerBO.Instance.GetirUlkelerObjectId(yonetici.IdUyrukFK.Value);

        //            personelBilgileriInput.adi = yonetici.Adi;
        //            personelBilgileriInput.soyadi = yonetici.Soyadi;
        //            personelBilgileriInput.tcKimlikPasaportNo = yonetici.KimlikNo == null ? yonetici.PasaportNo : yonetici.KimlikNo;
        //            personelBilgileriInput.telefon = yonetici.Gsm;
        //            personelBilgileriInput.turKodu = (int)personel.EnumTuru;
        //            personelBilgileriInput.uyrukUlke = ulke.UlkeKodu;
        //            personelBilgileriInput.cinsiyet = yonetici.CinsiyetStr;
        //            personelBilgileriInput.adres = ""; //TODO - Konuşulacak

        //            personelListesi.Add(personelBilgileriInput);
        //            var res = UedtsService().personelEkle(User(), seferRefNumber, personelListesi.ToArray());

        //            if (res.sonucKodu == 0)
        //            {
        //                personel.EnumPersonelDurumu = UlasimSeferPersonelleri.PersonelDurumuEnum.Gecerli;

        //                UlasimSeferPersonelleriBO.Instance.GuncelleUlasimSeferPersonelleri(personel);
        //            }
        //            else
        //            {
        //                hataliPersoneller.Add(yonetici.AdiSoyadi);
        //            }
        //        }
        //        else if (personel.EnumPersonelTuru == UlasimSeferPersonelleri.PersonelTuruEnum.DisPersonel)
        //        {
        //            DisPersoneller yonetici = DisPersonellerBO.Instance.GetirDisPersonellerObjectId(personel.IdGorevliPersonelFK);
        //            Ulkeler ulke = UlkelerBO.Instance.GetirUlkelerObjectId(yonetici.IdUyrukFK.Value);

        //            personelBilgileriInput.adi = yonetici.Adi;
        //            personelBilgileriInput.soyadi = yonetici.Soyadi;
        //            personelBilgileriInput.tcKimlikPasaportNo = yonetici.KimlikNo == null ? yonetici.PasaportNo : yonetici.KimlikNo;
        //            personelBilgileriInput.telefon = yonetici.Gsm;
        //            personelBilgileriInput.turKodu = (int)personel.EnumTuru;
        //            personelBilgileriInput.uyrukUlke = ulke.UlkeKodu;
        //            personelBilgileriInput.cinsiyet = yonetici.CinsiyetStr;
        //            personelBilgileriInput.adres = ""; //TODO - Konuşulacak

        //            personelListesi.Add(personelBilgileriInput);
        //            var res = UedtsService().personelEkle(User(), seferRefNumber, personelListesi.ToArray());

        //            if (res.sonucKodu == 0)
        //            {
        //                personel.EnumPersonelDurumu = UlasimSeferPersonelleri.PersonelDurumuEnum.Gecerli;

        //                UlasimSeferPersonelleriBO.Instance.GuncelleUlasimSeferPersonelleri(personel);
        //            }
        //            else
        //            {
        //                hataliPersoneller.Add(yonetici.AdiSoyadi);
        //            }
        //        }

        //    }

        //    if (hataliPersoneller.Count == 0)
        //    {
        //        return "";
        //    }
        //    else
        //    {
        //        string hataliPersonellerMesaj = "HATA. ";

        //        foreach (string personel in hataliPersoneller)
        //        {
        //            hataliPersonellerMesaj += personel + " ";
        //        }

        //        hataliPersonellerMesaj += "bilgili personel veya personellerde hata var. Lütfen bilgileri kontrol edip tekrar deneyin. Diğer personeller eklenmiştir.";
        //        return hataliPersonellerMesaj;
        //    }
        //}

        //public string GrupEkle(UlasimSeferGruplari ulasimSeferGruplari)
        //{
        //    uetdsAriziGrupBilgileriInput grupBilgileriInput = new uetdsAriziGrupBilgileriInput();

        //    UlasimSeferleri sefer = UlasimSeferleriBO.Instance.GetirUlasimSeferleriObjectId(ulasimSeferGruplari.IdUlasimSeferFK);

        //    Ulkeler baslangicUlke = UlkelerBO.Instance.GetirUlkelerObjectId(ulasimSeferGruplari.IdBaslangicUlkeFK);
        //    grupBilgileriInput.baslangicUlke = ulasimSeferGruplari.BaslangicUlkeFK.UlkeKodu;

        //    Ulkeler bitisUlke = UlkelerBO.Instance.GetirUlkelerObjectId(ulasimSeferGruplari.IdBitisUlkeFK);
        //    grupBilgileriInput.bitisUlke = ulasimSeferGruplari.BitisUlkeFK.UlkeKodu;

        //    if (ulasimSeferGruplari.IdBaslangicSehirFK != null)
        //    {
        //        Sehirler baslangicSehir = SehirlerBO.Instance.GetirSehirlerObjectId(ulasimSeferGruplari.IdBaslangicSehirFK.Value);
        //        grupBilgileriInput.baslangicIl = long.Parse(baslangicSehir.PlakaKodu);

        //        Ilceler baslangicIlce = IlcelerBO.Instance.GetirIlcelerObjectId(ulasimSeferGruplari.IdBaslangicIlceFK.Value);
        //        grupBilgileriInput.baslangicIlce = baslangicIlce.IlceKodu;
        //    }

        //    if (ulasimSeferGruplari.IdBitisSehirFK != null)
        //    {
        //        Sehirler bitisSehir = SehirlerBO.Instance.GetirSehirlerObjectId(ulasimSeferGruplari.IdBitisSehirFK.Value);
        //        grupBilgileriInput.bitisIl = long.Parse(bitisSehir.PlakaKodu);

        //        Ilceler bitisIlce = IlcelerBO.Instance.GetirIlcelerObjectId(ulasimSeferGruplari.IdBitisIlceFK.Value);
        //        grupBilgileriInput.bitisIlce = bitisIlce.IlceKodu;
        //    }

        //    grupBilgileriInput.baslangicYer = ulasimSeferGruplari.BaslangicYeri;
        //    grupBilgileriInput.bitisYer = ulasimSeferGruplari.BitisYeri;
        //    grupBilgileriInput.grupAciklama = ulasimSeferGruplari.Aciklama;
        //    grupBilgileriInput.grupAdi = ulasimSeferGruplari.GrupAdi;
        //    grupBilgileriInput.grupUcret = ulasimSeferGruplari.TasimaUcreti;

        //    uetdsAriziGrupIslemSonuc res;
        //    int sonucKodu = 9999;
        //    string sonucMesaji = "";

        //    if (string.IsNullOrEmpty(ulasimSeferGruplari.GrupRefId))
        //    {
        //        res = UedtsService().seferGrupEkle(User(), long.Parse(sefer.SeferRefNo), true, grupBilgileriInput);
        //        sonucKodu = res.sonucKodu;
        //        sonucMesaji = res.sonucMesaji;

        //        if (sonucKodu == 0)
        //            return res.uetdsGrupRefNo;
        //        else
        //            return "HATA " + sonucMesaji;//HATA MESAJI
        //    }
        //    else
        //    {
        //        res = UedtsService().seferGrupGuncelle(User(), long.Parse(sefer.SeferRefNo), true, long.Parse(ulasimSeferGruplari.GrupRefId), true, grupBilgileriInput);
        //        sonucKodu = res.sonucKodu;
        //        sonucMesaji = res.sonucMesaji;

        //        if (sonucKodu == 0)
        //            return "Grup Güncellendi";
        //        else
        //            return "HATA " + sonucMesaji;//HATA MESAJI
        //    }
        //}

        //public string YolcuEkleCoklu(List<UlasimSeferYolculari> ulasimSeferYolculari, string seferRefNo, string grupRefNo)
        //{
        //    List<UlasimSeferYolculari> hataliYolcular = new List<UlasimSeferYolculari>();

        //    foreach (UlasimSeferYolculari yolcu in ulasimSeferYolculari)
        //    {
        //        uetdsAriziSeferYolcuBilgileriInput yolcuBilgileriInput = new uetdsAriziSeferYolcuBilgileriInput();

        //        if (!string.IsNullOrEmpty(yolcu.YolcuRefNo))
        //        {
        //            uetdsAriziYolcuIptalInput yolcuGelmeyenInput = new uetdsAriziYolcuIptalInput();

        //            var resIptal = UedtsService().yolcuIptalUetdsYolcuRefNoIle(User(), long.Parse(seferRefNo), true, long.Parse(yolcu.YolcuRefNo), true, "");

        //            if (resIptal.sonucKodu != 0)
        //            {
        //                return resIptal.sonucMesaji;
        //            }
        //            else
        //            {
        //                yolcu.YolcuRefNo = null;
        //                yolcu.EnumDurumu = UlasimSeferYolculari.DurumuEnum.Taslak;

        //                UlasimSeferYolculariBO.Instance.GuncelleUlasimSeferYolculari(yolcu);
        //            }
        //        }

        //        yolcuBilgileriInput.adi = "Test Ad"; //yolcu.Adi;
        //        yolcuBilgileriInput.soyadi = "Test Soyad"; //yolcu.Soyadi;
        //        yolcuBilgileriInput.tcKimlikPasaportNo = "15058023598";     //yolcu.TCKNoPasaportNo;
        //        yolcuBilgileriInput.uyrukUlke = yolcu.Ulkesi;
        //        yolcuBilgileriInput.cinsiyet = yolcu.CinsiyetStr;
        //        yolcuBilgileriInput.grupId = long.Parse(grupRefNo);
        //        yolcuBilgileriInput.telefonNo = yolcu.TelefonNo;

        //        var res = UedtsService().yolcuEkle(User(), long.Parse(seferRefNo), true, yolcuBilgileriInput);

        //        if (res.sonucKodu == 0)
        //        {
        //            yolcu.YolcuRefNo = res.uetdsYolcuRefNo;
        //            yolcu.EnumDurumu = UlasimSeferYolculari.DurumuEnum.Gecerli;

        //            UlasimSeferYolculariBO.Instance.GuncelleUlasimSeferYolculari(yolcu);
        //        }
        //        else
        //        {
        //            hataliYolcular.Add(yolcu);
        //        }
        //    }

        //    if (hataliYolcular.Count == 0)
        //    {
        //        return "";
        //    }
        //    else
        //    {
        //        string hataliYolcuMesaj = "";

        //        foreach (UlasimSeferYolculari yolcu in hataliYolcular)
        //        {
        //            hataliYolcuMesaj += yolcu.Adi + " " + yolcu.Soyadi + " ";
        //        }

        //        hataliYolcuMesaj += "bilgili yolcuda veya yolcularda hata var. Lütfen bilgileri kontrol edip tekrar deneyin. Diğer yolcular başarıyla eklenmiştir.";
        //        return hataliYolcuMesaj;
        //    }

        //}

        //public string YolcuIptalEt(UlasimSeferYolculari yolcu, string seferRefNo, string grupRefNo)
        //{
        //    List<UlasimSeferYolculari> hataliYolcular = new List<UlasimSeferYolculari>();

        //    var resIptal = UedtsService().yolcuIptalUetdsYolcuRefNoIle(User(), long.Parse(seferRefNo), true, long.Parse(yolcu.YolcuRefNo), true, "");

        //    if (resIptal.sonucKodu != 0)
        //    {
        //        return "HATA " + resIptal.sonucMesaji;
        //    }
        //    else
        //    {
        //        yolcu.YolcuRefNo = null;
        //        yolcu.EnumDurumu = UlasimSeferYolculari.DurumuEnum.Taslak;

        //        UlasimSeferYolculariBO.Instance.GuncelleUlasimSeferYolculari(yolcu);
        //        return "Yolcu İptal işlemi başarılı.";
        //    }
        //}

        //public string SeferCiktisiAl(UlasimSeferleri ulasimSeferi, string path)
        //{
        //    var resCikti = UedtsService().seferDetayCiktisiAl(User(), long.Parse(ulasimSeferi.SeferRefNo), true);

        //    if (resCikti.sonucKodu != 0)
        //    {
        //        return "HATA " + resCikti.sonucMesaji;
        //    }
        //    else
        //    {
        //        string gidPdf = ulasimSeferi.Gid;

        //        System.IO.File.WriteAllBytes(string.Format("{0}.pdf", path + gidPdf), resCikti.sonucPdf);

        //        ulasimSeferi.SeferDosyasi = gidPdf + ".pdf";

        //        UlasimSeferleriBO.Instance.GuncelleUlasimSeferleri(ulasimSeferi);
        //        return "Başarılı.";
        //    }
        //}

        #endregion


    }
}

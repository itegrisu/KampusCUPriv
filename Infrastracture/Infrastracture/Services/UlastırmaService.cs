using Application.Abstractions;
using Application.Features.TransportationManagementFeatures.TransportationServices.Queries.GetByGid;
using Application.Features.TransportationManagementsFeatures.TransportationServices.Commands.ReportTransportationService;
using Application.Repositories.TransportationRepos.TransportationPassengerRepo;
using Application.Repositories.TransportationRepos.TransportationPersonnelRepo;
using Application.Repositories.TransportationRepos.TransportationServiceRepo;
using Domain.Entities.TransportationManagements;
using Domain.Enums;
using System.Net;
using System.ServiceModel;
using System.Text;
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

        private readonly ITransportationServiceReadRepository _transportationServiceReadRepository;
        private readonly ITransportationServiceWriteRepository _transportationServiceWriteRepository;
        private readonly ITransportationPersonnelWriteRepository _transportationPersonnelWriteRepository;
        private readonly ITransportationPassengerWriteRepository _transportationPassengerWriteRepository;
        public UlastirmaService(ITransportationServiceReadRepository transportationServiceReadRepository, ITransportationPersonnelWriteRepository transportationPersonnelWriteRepository, ITransportationPassengerWriteRepository transportationPassengerWriteRepository, ITransportationServiceWriteRepository transportationServiceWriteRepository)
        {
            _transportationServiceReadRepository = transportationServiceReadRepository;
            _transportationPersonnelWriteRepository = transportationPersonnelWriteRepository;
            _transportationPassengerWriteRepository = transportationPassengerWriteRepository;
            _transportationServiceWriteRepository = transportationServiceWriteRepository;
        }

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
                MaxReceivedMessageSize = 2147483647 // Gerekirse artırılabilir
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
        public async Task<string> SeferEkleAsync(TransportationService ulasimSeferi)
        {
            uetdsAriziSeferBilgileriInput seferBilgileriInput = new uetdsAriziSeferBilgileriInput();

            seferBilgileriInput.aracPlaka = ulasimSeferi.VehicleAllFK.PlateNumber;
            seferBilgileriInput.aracTelefonu = ulasimSeferi.VehiclePhone;
            seferBilgileriInput.firmaSeferNo = ulasimSeferi.TransportationFK.TransportationNo;
            seferBilgileriInput.hareketTarihi = ulasimSeferi.StartDate;
            seferBilgileriInput.hareketSaati = ulasimSeferi.StartDate.ToString("HH:mm");
            seferBilgileriInput.seferBitisTarihi = ulasimSeferi.EndDate;
            seferBilgileriInput.seferBitisSaati = ulasimSeferi.EndDate.ToString("HH:mm");
            seferBilgileriInput.seferAciklama = ulasimSeferi.Description;

            var result = await UedtsService().seferEkleAsync(User(), seferBilgileriInput);

            long seferRefNo = result.@return.uetdsSeferReferansNo;
            int sonucKodu = result.@return.sonucKodu;
            string sonucMesaji = result.@return.sonucMesaji;

            if (sonucKodu == 0)
                return seferRefNo.ToString(); //Başarılysa direkt seferRefNo Gönderilecek
            else
                return "HATA " + sonucMesaji; //HATA MESAJI
        }
        public async Task<string> SeferGuncelleAsync(TransportationService transportationService, long seferRefNumber)
        {
            // UETDS sefer bilgileri nesnesini dolduruyoruz
            var seferBilgileriInput = new uetdsAriziSeferBilgileriInput
            {
                aracPlaka = transportationService.VehicleAllFK.PlateNumber,
                aracTelefonu = transportationService.VehiclePhone,
                firmaSeferNo = transportationService.TransportationFK.TransportationNo,
                hareketTarihi = transportationService.StartDate,
                hareketSaati = transportationService.StartDate.ToString("HH:mm"),
                seferBitisTarihi = transportationService.EndDate,
                seferBitisSaati = transportationService.EndDate.ToString("HH:mm"),
                seferAciklama = transportationService.Description
            };

            // UETDS servisi üzerinden sefer güncelleme işlemi gerçekleştiriliyor
            var result = await UedtsService().seferGuncelleAsync(User(), seferRefNumber, seferBilgileriInput
            );

            // Sonuç bilgileri
            long seferRefNo = result.@return.uetdsSeferReferansNo;
            int sonucKodu = result.@return.sonucKodu;
            string sonucMesaji = result.@return.sonucMesaji;

            // Başarı durumu kontrol ediliyor
            if (sonucKodu == 0)
            {
                return seferRefNo.ToString(); // Güncellenen seferin referans numarasını döndür
            }
            else
            {
                return $"HATA {sonucMesaji}"; // Hata mesajını döndür
            }
        }
        public async Task<string> SeferIptalAsync(long seferRefNumber)
        {
            // İptal açıklamasını burada belirtebilirsiniz.
            string iptalAciklama = "Sefer teknik bir sebepten dolayı iptal edilmiştir.";

            // UETDS sefer iptal servis çağrısı
            var result = await UedtsService().seferIptalAsync(
                User(),                     // Kullanıcı bilgileri
                seferRefNumber,             // Uetds Sefer Referans Numarası
                iptalAciklama               // İptal açıklaması
            );

            // Sonuç bilgileri
            int sonucKodu = result.@return.sonucKodu;
            string sonucMesaji = result.@return.sonucMesaji;

            // İşlem durumu kontrol ediliyor
            if (sonucKodu == 0)
            {
                return "Sefer iptal işlemi başarılı."; // Başarı mesajı
            }
            else
            {
                return $"HATA: {sonucMesaji}"; // Hata mesajı
            }
        }
        public async Task<string> SeferCiktisiAl(TransportationService transportationService, string path)
        {
            // UETDS servisine kullanıcı bilgilerini ve sefer referans numarasını gönderiyoruz
            var resCikti = await UedtsService().seferDetayCiktisiAlAsync(User(), long.Parse(transportationService.RefNoTransportation));

            // İşlem sonucunu kontrol ediyoruz
            if (resCikti.@return.sonucKodu != 0)
            {
                // Hata durumunda mesajı döndürüyoruz
                return $"HATA: {resCikti.@return.sonucMesaji}";
            }
            else
            {
                // PDF dosyasını byte[] olarak alıyoruz
                byte[] pdfData = resCikti.@return.sonucPdf;

                // Dosya adını belirliyoruz 
                string fileName = $"{path}{transportationService.Gid}.pdf";

                // PDF dosyasını belirtilen dizine yazıyoruz
                await System.IO.File.WriteAllBytesAsync(fileName, pdfData);
                transportationService.TransportationFile = $"{transportationService.Gid}.pdf";

                _transportationServiceWriteRepository.Update(transportationService);
                await _transportationServiceWriteRepository.SaveAsync();

                // Başarılı mesajını döndürüyoruz
                return $"Başarılı: PDF dosyası {fileName} olarak kaydedildi.";
            }
        }
        public async Task<string> GrupEkleAsync(TransportationGroup transportationGroup)
        {
            uetdsAriziGrupBilgileriInput grupBilgileriInput = new uetdsAriziGrupBilgileriInput();

            var transportationService = await _transportationServiceReadRepository.GetSingleAsync(x => x.Gid == transportationGroup.GidTransportationServiceFK);

            grupBilgileriInput.baslangicYer = transportationGroup.StartPlace;
            grupBilgileriInput.baslangicUlke = transportationGroup.StartCountryFK.CountryCode;
            grupBilgileriInput.baslangicIl = long.Parse(transportationGroup.StartCityFK.PlateCode);
            grupBilgileriInput.baslangicIlce = transportationGroup.StartDistrictFK.DistrictCode;
            grupBilgileriInput.bitisYer = transportationGroup.EndPlace;
            grupBilgileriInput.bitisUlke = transportationGroup.EndCountryFK.CountryCode;
            grupBilgileriInput.bitisIl = long.Parse(transportationGroup.EndCityFK.PlateCode);
            grupBilgileriInput.bitisIlce = transportationGroup.EndDistrictFK.DistrictCode;
            grupBilgileriInput.grupAciklama = transportationGroup.Description;
            grupBilgileriInput.grupAdi = transportationGroup.GroupName;
            grupBilgileriInput.grupUcret = transportationGroup.TransportationFee.ToString();

            int sonucKodu = 9999;
            string sonucMesaji = "";

            if (string.IsNullOrEmpty(transportationGroup.RefNoTransportationGroup))
            {

                var response = await UedtsService().seferGrupEkleAsync(User(), long.Parse(transportationService.RefNoTransportation), grupBilgileriInput);

                sonucKodu = response.@return.sonucKodu;
                sonucMesaji = response.@return.sonucMesaji;

                if (sonucKodu == 0)
                    return response.@return.uetdsGrupRefNo;
                else
                    return "HATA " + sonucMesaji;//HATA MESAJI
            }
            else
            {
                var response = await UedtsService().seferGrupGuncelleAsync(User(), long.Parse(transportationService.RefNoTransportation), long.Parse(transportationGroup.RefNoTransportationGroup), grupBilgileriInput);

                sonucKodu = response.@return.sonucKodu;
                sonucMesaji = response.@return.sonucMesaji;

                if (sonucKodu == 0)
                    return "Grup Güncellendi";
                else
                    return "HATA " + sonucMesaji;//HATA MESAJI
            }
        }
        public async Task<string> GrupGuncelleAsync(TransportationGroup transportationGroup, long seferRefNumber, long grupRefNumber)
        {
            // UETDS grup bilgileri nesnesini dolduruyoruz
            var grupBilgileriInput = new uetdsAriziGrupBilgileriInput
            {
                grupAdi = transportationGroup.GroupName,
                grupAciklama = transportationGroup.Description,
                baslangicUlke = transportationGroup.StartCountryFK.CountryCode,
                baslangicIl = long.Parse(transportationGroup.StartCityFK.PlateCode),
                baslangicIlce = transportationGroup.StartDistrictFK.DistrictCode,
                baslangicYer = transportationGroup.StartPlace,
                bitisUlke = transportationGroup.EndCountryFK.CountryCode,
                bitisIl = long.Parse(transportationGroup.EndCityFK.PlateCode),
                bitisIlce = transportationGroup.EndDistrictFK.DistrictCode,
                bitisYer = transportationGroup.EndPlace,
                grupUcret = transportationGroup.TransportationFee.ToString()
            };

            // UETDS servisi üzerinden grup güncelleme işlemi gerçekleştiriliyor
            var result = await UedtsService().seferGrupGuncelleAsync(
                User(), // Kullanıcı bilgileri
                seferRefNumber, // UETDS Sefer Referans Numarası
                grupRefNumber, // UETDS Grup Referans Numarası
                grupBilgileriInput // Grup bilgileri
            );

            // Sonuç bilgileri
            int sonucKodu = result.@return.sonucKodu;
            string sonucMesaji = result.@return.sonucMesaji;
            string grupRefNo = result.@return.uetdsGrupRefNo;

            // Başarı durumu kontrol ediliyor
            if (sonucKodu == 0)
            {
                return grupRefNo; // Güncellenen grup referans numarasını döndür
            }
            else
            {
                return $"HATA {sonucMesaji}"; // Hata mesajını döndür
            }
        }
        public async Task<string> GrupIptalAsync(long seferRefNumber, long grupRefNumber)
        {
            // İptal açıklamasını burada belirtebilirsiniz.
            string iptalAciklama = "Grup, teknik bir sebepten dolayı iptal edilmiştir.";

            // UETDS sefer grup iptal servis çağrısı
            var result = await UedtsService().seferGrupIptalAsync(
                User(),                     // Kullanıcı bilgileri
                seferRefNumber,             // Uetds Sefer Referans Numarası
                grupRefNumber,              // Grup Referans Numarası
                iptalAciklama               // İptal açıklaması
            );

            // Sonuç bilgileri
            int sonucKodu = result.@return.sonucKodu;
            string sonucMesaji = result.@return.sonucMesaji;

            // İşlem durumu kontrol ediliyor
            if (sonucKodu == 0)
            {
                return "Grup iptal işlemi başarılı."; // Başarı mesajı
            }
            else
            {
                return $"HATA: {sonucMesaji}"; // Hata mesajı
            }
        }
        public async Task<string> PersonelEkleAsync(List<TransportationPersonnel> transportationPersonnel, long seferRefNumber)
        {
            List<string> wrongPersonnel = new List<string>();

            foreach (TransportationPersonnel personnel in transportationPersonnel)
            {
                List<uetdsAriziSeferPersonelBilgileriInput> personnelList = new List<uetdsAriziSeferPersonelBilgileriInput>();

                uetdsAriziSeferPersonelBilgileriInput personelBilgileriInput = new uetdsAriziSeferPersonelBilgileriInput();

                personelBilgileriInput.adi = personnel.UserFK.Name;
                personelBilgileriInput.soyadi = personnel.UserFK.Surname;
                personelBilgileriInput.tcKimlikPasaportNo = personnel.UserFK.IdentityNo == null ? personnel.UserFK.PassportNo : personnel.UserFK.IdentityNo;
                personelBilgileriInput.turKodu = (int)personnel.StaffType;
                personelBilgileriInput.telefon = personnel.UserFK.Gsm;
                personelBilgileriInput.uyrukUlke = personnel.UserFK.CountryFK.CountryCode;
                personelBilgileriInput.cinsiyet = personnel.UserFK.Gender.ToString();
                personelBilgileriInput.adres = ""; //TODO - Konuşulacak

                personnelList.Add(personelBilgileriInput);
                var res = await UedtsService().personelEkleAsync(User(), seferRefNumber, personnelList.ToArray());

                if (res.@return.sonucKodu == 0)
                {
                    personnel.StaffStatus = EnumStaffStatus.Gecerli;

                    _transportationPersonnelWriteRepository.Update(personnel);
                    await _transportationPersonnelWriteRepository.SaveAsync();
                }
                else
                {
                    wrongPersonnel.Add(personnel.UserFK.FullName);
                }
            }

            if (wrongPersonnel.Count == 0)
            {
                return "";
            }
            else
            {
                string hataliPersonellerMesaj = "HATA. ";

                foreach (string personel in wrongPersonnel)
                {
                    hataliPersonellerMesaj += personel + " ";
                }

                hataliPersonellerMesaj += "bilgili personel veya personellerde hata var. Lütfen bilgileri kontrol edip tekrar deneyin. Diğer personeller eklenmiştir.";
                return hataliPersonellerMesaj;
            }
        }
        public async Task<string> PersonelIptalAsync(TransportationPersonnel transportationPersonnel, long seferRefNumber)
        {
            // İptal açıklamasını burada belirtebilirsiniz.
            string iptalAciklama = "Personel, teknik bir sebepten dolayı iptal edilmiştir.";

            // UETDS personel iptal input nesnesi oluşturuluyor
            var personelIptalInput = new uetdsPersonelIptalInput
            {
                personelTCKimlikPasaportNo = transportationPersonnel.UserFK.IdentityNo == null ? transportationPersonnel.UserFK.PassportNo : transportationPersonnel.UserFK.IdentityNo, // Personelin kimlik/pasaport numarası
                uetdsSeferReferansNo = seferRefNumber,                                        // UETDS Sefer Referans Numarası
                iptalAciklama = iptalAciklama                                                // İptal açıklaması
            };

            // UETDS personel iptal servis çağrısı
            var result = await UedtsService().personelIptalAsync(User(), personelIptalInput);

            // Sonuç bilgileri
            int sonucKodu = result.@return.sonucKodu;
            string sonucMesaji = result.@return.sonucMesaji;

            // İşlem durumu kontrol ediliyor
            if (sonucKodu == 0)
            {
                return "Personel iptal işlemi başarılı."; // Başarı mesajı
            }
            else
            {
                return $"HATA: {sonucMesaji}"; // Hata mesajı
            }
        }
        public async Task<string> YolcuEkleAsync(List<TransportationPassenger> passengers, long seferRefNo, long grupRefNo)
        {
            List<TransportationPassenger> failedPassengers = new List<TransportationPassenger>();

            foreach (var passenger in passengers)
            {
                uetdsAriziSeferYolcuBilgileriInput yolcuBilgileriInput = new uetdsAriziSeferYolcuBilgileriInput();

                // Eğer yolcu daha önce eklendiyse, iptal edilip tekrar eklenecek
                if (!string.IsNullOrEmpty(passenger.RefNoTransportationPassenger))
                {
                    var iptalInput = new uetdsAriziYolcuIptalInput();
                    var cancelResponse = await UedtsService().yolcuIptalAsync(User(),seferRefNo,iptalInput);

                    if (cancelResponse.@return.sonucKodu != 0)
                    {
                        return cancelResponse.@return.sonucMesaji;
                    }

                    // Yolcu bilgilerini güncelle
                    passenger.RefNoTransportationPassenger = null;
                    passenger.PassengerStatus = EnumPassengerStatus.Taslak;

                    _transportationPassengerWriteRepository.Update(passenger);
                    await _transportationPassengerWriteRepository.SaveAsync();
                }

                // Yolcu bilgileri dolduruluyor
                yolcuBilgileriInput.adi = passenger.FirstName;
                yolcuBilgileriInput.soyadi = passenger.LastName;
                yolcuBilgileriInput.tcKimlikPasaportNo = passenger.IdentityNo;
                yolcuBilgileriInput.uyrukUlke = passenger.Country;
                yolcuBilgileriInput.cinsiyet = passenger.Gender.ToString();
                yolcuBilgileriInput.grupId = grupRefNo;
                yolcuBilgileriInput.telefonNo = passenger.Phone ?? "";

                // Yolcu ekleme işlemi
                var response = await UedtsService().yolcuEkleAsync(User(),seferRefNo,yolcuBilgileriInput);

                if (response.@return.sonucKodu == 0)
                {
                    // Başarılı işlem, yolcu bilgileri güncelleniyor
                    passenger.RefNoTransportationPassenger = response.@return.uetdsYolcuRefNo;
                    passenger.PassengerStatus = EnumPassengerStatus.Gecerli;

                    _transportationPassengerWriteRepository.Update(passenger);
                    await _transportationPassengerWriteRepository.SaveAsync();
                }
                else
                {
                    // Hata alan yolcular listeye ekleniyor
                    failedPassengers.Add(passenger);
                }
            }

            // Hatalı yolcular için mesaj hazırlanıyor
            if (failedPassengers.Count == 0)
            {
                return "";
            }
            else
            {
                var errorMessage = new StringBuilder();
                errorMessage.AppendLine("HATA: Aşağıdaki yolcularda hata oluştu:");

                foreach (var failedPassenger in failedPassengers)
                {
                    errorMessage.AppendLine($"{failedPassenger.FirstName} {failedPassenger.LastName}");
                }

                errorMessage.AppendLine("Lütfen bilgileri kontrol edip tekrar deneyin. Diğer yolcular başarıyla eklenmiştir.");
                return errorMessage.ToString();
            }
        }
        public async Task<string> YolcuIptalAsync(long seferRefNumber, long refNoTransportationPassenger)
        {
            // İptal açıklamasını burada belirtebilirsiniz.
            string iptalAciklama = "Yolcu, teknik bir sebepten dolayı iptal edilmiştir.";

            // UETDS yolcu iptal servis çağrısı
            var result = await UedtsService().yolcuIptalUetdsYolcuRefNoIleAsync(
                User(),seferRefNumber,refNoTransportationPassenger, iptalAciklama
            );

            // Sonuç bilgileri
            int sonucKodu = result.@return.sonucKodu;
            string sonucMesaji = result.@return.sonucMesaji;

            // İşlem durumu kontrol ediliyor
            if (sonucKodu == 0)
            {
                return "Yolcu iptal işlemi başarılı."; // Başarı mesajı
            }
            else
            {
                return $"HATA: {sonucMesaji}"; // Hata mesajı
            }
        }
        #endregion


    }
}

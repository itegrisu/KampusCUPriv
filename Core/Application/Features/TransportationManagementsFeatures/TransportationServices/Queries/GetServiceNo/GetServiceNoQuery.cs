using Application.Features.TransportationManagementsFeatures.Transportations.Queries.GetTransportationNo;
using Application.Repositories.TransportationRepos.TransportationRepo;
using Application.Repositories.TransportationRepos.TransportationServiceRepo;
using Domain.Entities.TransportationManagements;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.TransportationManagementsFeatures.TransportationServices.Queries.GetServiceNo
{
    public class GetServiceNoQuery : IRequest<GetServiceNoResponse>
    {
        public string TransportationNo { get; set; }
        public class GetServiceNoQueryHandler : IRequestHandler<GetServiceNoQuery, GetServiceNoResponse>
        {
            private readonly ITransportationServiceReadRepository _transportationServiceReadRepository;

            public GetServiceNoQueryHandler(ITransportationServiceReadRepository transportationServiceReadRepository)
            {
                _transportationServiceReadRepository = transportationServiceReadRepository;
            }
            public async Task<GetServiceNoResponse> Handle(GetServiceNoQuery request, CancellationToken cancellationToken)
            {
                List<TransportationService> requests = _transportationServiceReadRepository.GetAll().OrderByDescending(x => x.CreatedDate).ToList();

                // Burada temel alacağınız TransportationNo'yu almanız gerekiyor.
                string baseTransportationNo = request.TransportationNo; // Örnek değer, bunu request üzerinden alabilirsiniz.

                return new()
                {
                    ServiceNo = GenerateDocumentNumber(requests, baseTransportationNo)
                };
            }

            static string GenerateDocumentNumber(List<TransportationService> requests, string baseTransportationNo)
            {
                // İlk olarak gelen TransportationNo'nun geçerliliğini kontrol edelim.
                if (string.IsNullOrEmpty(baseTransportationNo))
                    throw new ArgumentException("Base Transportation No cannot be null or empty.");

                // Gelen TransportationNo'nun prefix kısmını belirliyoruz.
                string prefix = baseTransportationNo + "-";
                int maxSuffix = 0;

                // Prefix ile başlayan TransportationNo değerlerini bul ve mevcut en büyük suffix değerini tespit et.
                foreach (var request in requests)
                {
                    if (request.ServiceNo.StartsWith(prefix))
                    {
                        string suffixString = request.ServiceNo.Substring(prefix.Length);
                        if (int.TryParse(suffixString, out int suffix))
                        {
                            if (suffix > maxSuffix)
                            {
                                maxSuffix = suffix;
                            }
                        }
                    }
                }

                // Yeni suffix değeri mevcut maksimum değeri bir artırarak oluşturulur.
                int newSuffix = maxSuffix + 1;
                return prefix + newSuffix.ToString("D3"); // D3 formatı 3 haneli sıfırlarla doldurulmuş sayı oluşturur.
            }


        }
    }

}

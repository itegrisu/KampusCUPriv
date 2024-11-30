using Application.Repositories.TransportationRepos.TransportationRepo;
using Domain.Entities.TransportationManagements;
using MediatR;

namespace Application.Features.TransportationManagementsFeatures.Transportations.Queries.GetTransportationNo
{
    public class GetTransportationNoQuery : IRequest<GetTransportationNoResponse>
    {
        public class GetTransportationNoQueryHandler : IRequestHandler<GetTransportationNoQuery, GetTransportationNoResponse>
        {
            private readonly ITransportationReadRepository _transportationReadRepository;

            public GetTransportationNoQueryHandler(ITransportationReadRepository transportationReadRepository)
            {
                _transportationReadRepository = transportationReadRepository;
            }

            public async Task<GetTransportationNoResponse> Handle(GetTransportationNoQuery request, CancellationToken cancellationToken)
            {

                List<Transportation> requests = _transportationReadRepository.GetAll().OrderByDescending(x => x.CreatedDate).ToList();

                return new()
                {
                    TransportationNo = GenerateDocumentNumber(requests)
                };
            }
            static string GenerateDocumentNumber(List<Transportation> requests)
            {
                int currentYear = DateTime.Now.Year;
                string prefix = "UT" + currentYear.ToString().Substring(2) + "-" + DateTime.Now.Month.ToString().Substring(2) + DateTime.Now.Day.ToString().Substring(2);
                int maxSuffix = 0;

                foreach (var request in requests)
                {
                    if (request.TransportationNo.StartsWith(prefix))
                    {
                        string suffixString = request.TransportationNo.Substring(prefix.Length);
                        if (int.TryParse(suffixString, out int suffix))
                        {
                            if (suffix > maxSuffix)
                            {
                                maxSuffix = suffix;
                            }
                        }
                    }
                }

                int newSuffix = maxSuffix + 1;
                return prefix + newSuffix.ToString("D3"); // D4 formatı 4 haneli sıfırlarla doldurulmuş sayı oluşturur
            }

        }
    }
}


using Application.Features.TransportationManagementFeatures.Transportations.Queries.GetByGid;
using Application.Features.TransportationManagementFeatures.Transportations.Rules;
using Application.Repositories.OfferManagementRepos.OfferTransactionRepo;
using Application.Repositories.TransportationRepos.TransportationRepo;
using AutoMapper;
using Domain.Entities.OfferManagements;
using Domain.Entities.TransportationManagements;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X = Domain.Entities.TransportationManagements;

namespace Application.Features.TransportationManagementsFeatures.Transportations.Queries.GetTransportationNo
{
    public class GetTransportationNoQuery : IRequest<GetTransportationNoResponse>
    {
        public class GetTransportationNoQueryHandler : IRequestHandler<GetTransportationNoQuery, GetTransportationNoResponse>
        {
            private readonly IMapper _mapper;
            private readonly ITransportationReadRepository _transportationReadRepository;
            private readonly TransportationBusinessRules _transportationBusinessRules;

            public GetTransportationNoQueryHandler(IMapper mapper, ITransportationReadRepository transportationReadRepository, TransportationBusinessRules transportationBusinessRules)
            {
                _mapper = mapper;
                _transportationReadRepository = transportationReadRepository;
                _transportationBusinessRules = transportationBusinessRules;
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


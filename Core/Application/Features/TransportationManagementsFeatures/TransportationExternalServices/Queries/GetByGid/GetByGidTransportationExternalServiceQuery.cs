using Application.Features.TransportationManagementFeatures.TransportationExternalServices.Rules;
using Application.Repositories.TransportationRepos.TransportationExternalServiceRepo;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using X = Domain.Entities.TransportationManagements;

namespace Application.Features.TransportationManagementFeatures.TransportationExternalServices.Queries.GetByGid
{
    public class GetByGidTransportationExternalServiceQuery : IRequest<GetByGidTransportationExternalServiceResponse>
    {
        public Guid Gid { get; set; }

        public class GetByGidTransportationExternalServiceQueryHandler : IRequestHandler<GetByGidTransportationExternalServiceQuery, GetByGidTransportationExternalServiceResponse>
        {
            private readonly IMapper _mapper;
            private readonly ITransportationExternalServiceReadRepository _transportationExternalServiceReadRepository;
            private readonly TransportationExternalServiceBusinessRules _transportationExternalServiceBusinessRules;

            public GetByGidTransportationExternalServiceQueryHandler(IMapper mapper, ITransportationExternalServiceReadRepository transportationExternalServiceReadRepository, TransportationExternalServiceBusinessRules transportationExternalServiceBusinessRules)
            {
                _mapper = mapper;
                _transportationExternalServiceReadRepository = transportationExternalServiceReadRepository;
                _transportationExternalServiceBusinessRules = transportationExternalServiceBusinessRules;
            }

            public async Task<GetByGidTransportationExternalServiceResponse> Handle(GetByGidTransportationExternalServiceQuery request, CancellationToken cancellationToken)
            {
                X.TransportationExternalService? transportationExternalService = await _transportationExternalServiceReadRepository.GetAsync(predicate: uc => uc.Gid == request.Gid, cancellationToken: cancellationToken, include: x => x.Include(x => x.FeeCurrencyFK).Include(x => x.OrganizationFK).Include(x => x.SCCompanyFK));
                    //unutma
					//includes varsa eklenecek - Orn: Altta
					//include: i => i.Include(i => i.AcademicTitleFK).Include(i => i.UniversityFK)
                await _transportationExternalServiceBusinessRules.TransportationExternalServiceShouldExistWhenSelected(transportationExternalService);

                GetByGidTransportationExternalServiceResponse response = _mapper.Map<GetByGidTransportationExternalServiceResponse>(transportationExternalService);
                return response;
            }
        }
    }
}
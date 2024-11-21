using Application.Features.TransportationManagementFeatures.TransportationServices.Rules;
using Application.Repositories.TransportationRepos.TransportationServiceRepo;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using X = Domain.Entities.TransportationManagements;

namespace Application.Features.TransportationManagementFeatures.TransportationServices.Queries.GetByGid
{
    public class GetByGidTransportationServiceQuery : IRequest<GetByGidTransportationServiceResponse>
    {
        public Guid Gid { get; set; }

        public class GetByGidTransportationServiceQueryHandler : IRequestHandler<GetByGidTransportationServiceQuery, GetByGidTransportationServiceResponse>
        {
            private readonly IMapper _mapper;
            private readonly ITransportationServiceReadRepository _transportationServiceReadRepository;
            private readonly TransportationServiceBusinessRules _transportationServiceBusinessRules;

            public GetByGidTransportationServiceQueryHandler(IMapper mapper, ITransportationServiceReadRepository transportationServiceReadRepository, TransportationServiceBusinessRules transportationServiceBusinessRules)
            {
                _mapper = mapper;
                _transportationServiceReadRepository = transportationServiceReadRepository;
                _transportationServiceBusinessRules = transportationServiceBusinessRules;
            }

            public async Task<GetByGidTransportationServiceResponse> Handle(GetByGidTransportationServiceQuery request, CancellationToken cancellationToken)
            {
                X.TransportationService? transportationService = await _transportationServiceReadRepository.GetAsync(predicate: uc => uc.Gid == request.Gid, cancellationToken: cancellationToken, include: x => x.Include(x => x.TransportationFK).Include(x => x.VehicleAllFK));
                    //unutma
					//includes varsa eklenecek - Orn: Altta
					//include: i => i.Include(i => i.AcademicTitleFK).Include(i => i.UniversityFK)
                await _transportationServiceBusinessRules.TransportationServiceShouldExistWhenSelected(transportationService);

                GetByGidTransportationServiceResponse response = _mapper.Map<GetByGidTransportationServiceResponse>(transportationService);
                return response;
            }
        }
    }
}
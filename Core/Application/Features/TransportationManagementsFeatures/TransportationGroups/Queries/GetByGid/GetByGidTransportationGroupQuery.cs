using Application.Features.TransportationManagementFeatures.TransportationGroups.Rules;
using Application.Repositories.TransportationRepos.TransportationGroupRepo;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using X = Domain.Entities.TransportationManagements;

namespace Application.Features.TransportationManagementFeatures.TransportationGroups.Queries.GetByGid
{
    public class GetByGidTransportationGroupQuery : IRequest<GetByGidTransportationGroupResponse>
    {
        public Guid Gid { get; set; }

        public class GetByGidTransportationGroupQueryHandler : IRequestHandler<GetByGidTransportationGroupQuery, GetByGidTransportationGroupResponse>
        {
            private readonly IMapper _mapper;
            private readonly ITransportationGroupReadRepository _transportationGroupReadRepository;
            private readonly TransportationGroupBusinessRules _transportationGroupBusinessRules;

            public GetByGidTransportationGroupQueryHandler(IMapper mapper, ITransportationGroupReadRepository transportationGroupReadRepository, TransportationGroupBusinessRules transportationGroupBusinessRules)
            {
                _mapper = mapper;
                _transportationGroupReadRepository = transportationGroupReadRepository;
                _transportationGroupBusinessRules = transportationGroupBusinessRules;
            }

            public async Task<GetByGidTransportationGroupResponse> Handle(GetByGidTransportationGroupQuery request, CancellationToken cancellationToken)
            {
                X.TransportationGroup? transportationGroup = await _transportationGroupReadRepository.GetAsync(predicate: uc => uc.Gid == request.Gid, cancellationToken: cancellationToken, include: x => x.Include(x => x.StartCountryFK).Include(x => x.StartCityFK).Include(x => x.StartDistrictFK).Include(x => x.EndCountryFK).Include(x => x.EndCityFK).Include(x => x.EndDistrictFK).Include(x => x.TransportationServiceFK));
                    
                await _transportationGroupBusinessRules.TransportationGroupShouldExistWhenSelected(transportationGroup);

                GetByGidTransportationGroupResponse response = _mapper.Map<GetByGidTransportationGroupResponse>(transportationGroup);
                return response;
            }
        }
    }
}
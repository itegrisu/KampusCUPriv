using AutoMapper;
using MediatR;
using X = Domain.Entities.TransportationManagements;
using Microsoft.EntityFrameworkCore;
using Application.Features.TransportationManagementFeatures.Transportations.Rules;
using Application.Repositories.TransportationRepos.TransportationRepo;

namespace Application.Features.TransportationManagementFeatures.Transportations.Queries.GetByGid
{
    public class GetByGidTransportationQuery : IRequest<GetByGidTransportationResponse>
    {
        public Guid Gid { get; set; }

        public class GetByGidTransportationQueryHandler : IRequestHandler<GetByGidTransportationQuery, GetByGidTransportationResponse>
        {
            private readonly IMapper _mapper;
            private readonly ITransportationReadRepository _transportationReadRepository;
            private readonly TransportationBusinessRules _transportationBusinessRules;

            public GetByGidTransportationQueryHandler(IMapper mapper, ITransportationReadRepository transportationReadRepository, TransportationBusinessRules transportationBusinessRules)
            {
                _mapper = mapper;
                _transportationReadRepository = transportationReadRepository;
                _transportationBusinessRules = transportationBusinessRules;
            }

            public async Task<GetByGidTransportationResponse> Handle(GetByGidTransportationQuery request, CancellationToken cancellationToken)
            {
                X.Transportation? transportation = await _transportationReadRepository.GetAsync(predicate: uc => uc.Gid == request.Gid, cancellationToken: cancellationToken,
                       include: x => x.Include(x => x.OrganizationFK));
                    //unutma
					//includes varsa eklenecek - Orn: Altta
					//include: i => i.Include(i => i.AcademicTitleFK).Include(i => i.UniversityFK)
                await _transportationBusinessRules.TransportationShouldExistWhenSelected(transportation);

                GetByGidTransportationResponse response = _mapper.Map<GetByGidTransportationResponse>(transportation);
                return response;
            }
        }
    }
}
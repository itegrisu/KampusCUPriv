using Application.Features.TransportationManagementFeatures.TransportationPersonnels.Rules;
using Application.Repositories.TransportationRepos.TransportationPersonnelRepo;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using X = Domain.Entities.TransportationManagements;

namespace Application.Features.TransportationManagementFeatures.TransportationPersonnels.Queries.GetByGid
{
    public class GetByGidTransportationPersonnelQuery : IRequest<GetByGidTransportationPersonnelResponse>
    {
        public Guid Gid { get; set; }

        public class GetByGidTransportationPersonnelQueryHandler : IRequestHandler<GetByGidTransportationPersonnelQuery, GetByGidTransportationPersonnelResponse>
        {
            private readonly IMapper _mapper;
            private readonly ITransportationPersonnelReadRepository _transportationPersonnelReadRepository;
            private readonly TransportationPersonnelBusinessRules _transportationPersonnelBusinessRules;

            public GetByGidTransportationPersonnelQueryHandler(IMapper mapper, ITransportationPersonnelReadRepository transportationPersonnelReadRepository, TransportationPersonnelBusinessRules transportationPersonnelBusinessRules)
            {
                _mapper = mapper;
                _transportationPersonnelReadRepository = transportationPersonnelReadRepository;
                _transportationPersonnelBusinessRules = transportationPersonnelBusinessRules;
            }

            public async Task<GetByGidTransportationPersonnelResponse> Handle(GetByGidTransportationPersonnelQuery request, CancellationToken cancellationToken)
            {
                X.TransportationPersonnel? transportationPersonnel = await _transportationPersonnelReadRepository.GetAsync(predicate: uc => uc.Gid == request.Gid, cancellationToken: cancellationToken, include: x => x.Include(x => x.TransportationServiceFK).Include(x => x.UserFK));
                    //unutma
					//includes varsa eklenecek - Orn: Altta
					//include: i => i.Include(i => i.AcademicTitleFK).Include(i => i.UniversityFK)
                await _transportationPersonnelBusinessRules.TransportationPersonnelShouldExistWhenSelected(transportationPersonnel);

                GetByGidTransportationPersonnelResponse response = _mapper.Map<GetByGidTransportationPersonnelResponse>(transportationPersonnel);
                return response;
            }
        }
    }
}
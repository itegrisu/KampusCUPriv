using AutoMapper;
using MediatR;
using X = Domain.Entities.PortalManagements;
using Microsoft.EntityFrameworkCore;
using Application.Repositories.PortalManagementRepos.PortalParameterRepo;
using Application.Features.PortalManagementFeatures.PortalParameters.Rules;

namespace Application.Features.PortalManagementFeatures.PortalParameters.Queries.GetByGid
{
    public class GetByGidPortalParameterQuery : IRequest<GetByGidPortalParameterResponse>
    {
        public Guid Gid { get; set; }

        public class GetByGidPortalParameterQueryHandler : IRequestHandler<GetByGidPortalParameterQuery, GetByGidPortalParameterResponse>
        {
            private readonly IMapper _mapper;
            private readonly IPortalParameterReadRepository _portalParameterReadRepository;
            private readonly PortalParameterBusinessRules _portalParameterBusinessRules;

            public GetByGidPortalParameterQueryHandler(IMapper mapper, IPortalParameterReadRepository portalParameterReadRepository, PortalParameterBusinessRules portalParameterBusinessRules)
            {
                _mapper = mapper;
                _portalParameterReadRepository = portalParameterReadRepository;
                _portalParameterBusinessRules = portalParameterBusinessRules;
            }

            public async Task<GetByGidPortalParameterResponse> Handle(GetByGidPortalParameterQuery request, CancellationToken cancellationToken)
            {
                X.PortalParameter? portalParameter = await _portalParameterReadRepository.GetAsync(predicate: uc => uc.Gid == request.Gid, cancellationToken: cancellationToken);
                    //unutma
					//includes varsa eklenecek - Orn: Altta
					//include: i => i.Include(i => i.AcademicTitleFK).Include(i => i.UniversityFK)
                await _portalParameterBusinessRules.PortalParameterShouldExistWhenSelected(portalParameter);

                GetByGidPortalParameterResponse response = _mapper.Map<GetByGidPortalParameterResponse>(portalParameter);
                return response;
            }
        }
    }
}
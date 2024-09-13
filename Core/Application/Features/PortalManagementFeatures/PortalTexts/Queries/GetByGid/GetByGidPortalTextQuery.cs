using AutoMapper;
using MediatR;
using X = Domain.Entities.PortalManagements;
using Microsoft.EntityFrameworkCore;
using Application.Repositories.PortalManagementRepos.PortalTextRepo;
using Application.Features.PortalManagementFeatures.PortalTexts.Rules;

namespace Application.Features.PortalManagementFeatures.PortalTexts.Queries.GetByGid
{
    public class GetByGidPortalTextQuery : IRequest<GetByGidPortalTextResponse>
    {
        public Guid Gid { get; set; }

        public class GetByGidPortalTextQueryHandler : IRequestHandler<GetByGidPortalTextQuery, GetByGidPortalTextResponse>
        {
            private readonly IMapper _mapper;
            private readonly IPortalTextReadRepository _portalTextReadRepository;
            private readonly PortalTextBusinessRules _portalTextBusinessRules;

            public GetByGidPortalTextQueryHandler(IMapper mapper, IPortalTextReadRepository portalTextReadRepository, PortalTextBusinessRules portalTextBusinessRules)
            {
                _mapper = mapper;
                _portalTextReadRepository = portalTextReadRepository;
                _portalTextBusinessRules = portalTextBusinessRules;
            }

            public async Task<GetByGidPortalTextResponse> Handle(GetByGidPortalTextQuery request, CancellationToken cancellationToken)
            {
                X.PortalText? portalText = await _portalTextReadRepository.GetAsync(predicate: uc => uc.Gid == request.Gid, cancellationToken: cancellationToken);
                    //unutma
					//includes varsa eklenecek - Orn: Altta
					//include: i => i.Include(i => i.AcademicTitleFK).Include(i => i.UniversityFK)
                await _portalTextBusinessRules.PortalTextShouldExistWhenSelected(portalText);

                GetByGidPortalTextResponse response = _mapper.Map<GetByGidPortalTextResponse>(portalText);
                return response;
            }
        }
    }
}
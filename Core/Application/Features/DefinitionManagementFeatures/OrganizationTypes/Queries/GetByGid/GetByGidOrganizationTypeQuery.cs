using AutoMapper;
using MediatR;
using X = Domain.Entities.DefinitionManagements;
using Microsoft.EntityFrameworkCore;
using Application.Repositories.DefinitionManagementRepos.OrganizationTypeRepo;
using Application.Features.DefinitionManagementFeatures.OrganizationTypes.Rules;

namespace Application.Features.DefinitionManagementFeatures.OrganizationTypes.Queries.GetByGid
{
    public class GetByGidOrganizationTypeQuery : IRequest<GetByGidOrganizationTypeResponse>
    {
        public Guid Gid { get; set; }

        public class GetByGidOrganizationTypeQueryHandler : IRequestHandler<GetByGidOrganizationTypeQuery, GetByGidOrganizationTypeResponse>
        {
            private readonly IMapper _mapper;
            private readonly IOrganizationTypeReadRepository _organizationTypeReadRepository;
            private readonly OrganizationTypeBusinessRules _organizationTypeBusinessRules;

            public GetByGidOrganizationTypeQueryHandler(IMapper mapper, IOrganizationTypeReadRepository organizationTypeReadRepository, OrganizationTypeBusinessRules organizationTypeBusinessRules)
            {
                _mapper = mapper;
                _organizationTypeReadRepository = organizationTypeReadRepository;
                _organizationTypeBusinessRules = organizationTypeBusinessRules;
            }

            public async Task<GetByGidOrganizationTypeResponse> Handle(GetByGidOrganizationTypeQuery request, CancellationToken cancellationToken)
            {
                X.OrganizationType? organizationType = await _organizationTypeReadRepository.GetAsync(predicate: uc => uc.Gid == request.Gid, cancellationToken: cancellationToken);
                    //unutma
					//includes varsa eklenecek - Orn: Altta
					//include: i => i.Include(i => i.AcademicTitleFK).Include(i => i.UniversityFK)
                await _organizationTypeBusinessRules.OrganizationTypeShouldExistWhenSelected(organizationType);

                GetByGidOrganizationTypeResponse response = _mapper.Map<GetByGidOrganizationTypeResponse>(organizationType);
                return response;
            }
        }
    }
}
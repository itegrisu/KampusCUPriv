using Application.Features.OrganizationManagementFeatures.OrganizationGroups.Rules;
using Application.Repositories.OrganizationManagementRepos.OrganizationGroupRepo;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using X = Domain.Entities.OrganizationManagements;

namespace Application.Features.OrganizationManagementFeatures.OrganizationGroups.Queries.GetByGid
{
    public class GetByGidOrganizationGroupQuery : IRequest<GetByGidOrganizationGroupResponse>
    {
        public Guid Gid { get; set; }

        public class GetByGidOrganizationGroupQueryHandler : IRequestHandler<GetByGidOrganizationGroupQuery, GetByGidOrganizationGroupResponse>
        {
            private readonly IMapper _mapper;
            private readonly IOrganizationGroupReadRepository _organizationGroupReadRepository;
            private readonly OrganizationGroupBusinessRules _organizationGroupBusinessRules;

            public GetByGidOrganizationGroupQueryHandler(IMapper mapper, IOrganizationGroupReadRepository organizationGroupReadRepository, OrganizationGroupBusinessRules organizationGroupBusinessRules)
            {
                _mapper = mapper;
                _organizationGroupReadRepository = organizationGroupReadRepository;
                _organizationGroupBusinessRules = organizationGroupBusinessRules;
            }

            public async Task<GetByGidOrganizationGroupResponse> Handle(GetByGidOrganizationGroupQuery request, CancellationToken cancellationToken)
            {
                X.OrganizationGroup? organizationGroup = await _organizationGroupReadRepository.GetAsync(predicate: uc => uc.Gid == request.Gid, cancellationToken: cancellationToken, include: x => x.Include(x => x.OrganizationFK));

                await _organizationGroupBusinessRules.OrganizationGroupShouldExistWhenSelected(organizationGroup);

                GetByGidOrganizationGroupResponse response = _mapper.Map<GetByGidOrganizationGroupResponse>(organizationGroup);
                return response;
            }
        }
    }
}
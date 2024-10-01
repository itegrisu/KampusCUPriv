using Application.Features.OrganizationManagementFeatures.OrganizationItems.Rules;
using Application.Repositories.OrganizationManagementRepos.OrganizationItemRepo;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using X = Domain.Entities.OrganizationManagements;

namespace Application.Features.OrganizationManagementFeatures.OrganizationItems.Queries.GetByGid
{
    public class GetByGidOrganizationItemQuery : IRequest<GetByGidOrganizationItemResponse>
    {
        public Guid Gid { get; set; }

        public class GetByGidOrganizationItemQueryHandler : IRequestHandler<GetByGidOrganizationItemQuery, GetByGidOrganizationItemResponse>
        {
            private readonly IMapper _mapper;
            private readonly IOrganizationItemReadRepository _organizationItemReadRepository;
            private readonly OrganizationItemBusinessRules _organizationItemBusinessRules;

            public GetByGidOrganizationItemQueryHandler(IMapper mapper, IOrganizationItemReadRepository organizationItemReadRepository, OrganizationItemBusinessRules organizationItemBusinessRules)
            {
                _mapper = mapper;
                _organizationItemReadRepository = organizationItemReadRepository;
                _organizationItemBusinessRules = organizationItemBusinessRules;
            }

            public async Task<GetByGidOrganizationItemResponse> Handle(GetByGidOrganizationItemQuery request, CancellationToken cancellationToken)
            {
                X.OrganizationItem? organizationItem = await _organizationItemReadRepository.GetAsync(predicate: uc => uc.Gid == request.Gid, cancellationToken: cancellationToken, include: x => x.Include(x => x.MainResponsibleUserFK).Include(x => x.OrganizationGroupFK));

                await _organizationItemBusinessRules.OrganizationItemShouldExistWhenSelected(organizationItem);

                GetByGidOrganizationItemResponse response = _mapper.Map<GetByGidOrganizationItemResponse>(organizationItem);
                return response;
            }
        }
    }
}
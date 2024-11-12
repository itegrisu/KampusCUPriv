using Application.Features.OrganizationManagementFeatures.OrganizationItemMessages.Rules;
using Application.Repositories.OrganizationManagementRepos.OrganizationItemMessageRepo;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using X = Domain.Entities.OrganizationManagements;

namespace Application.Features.OrganizationManagementFeatures.OrganizationItemMessages.Queries.GetByGid
{
    public class GetByGidOrganizationItemMessageQuery : IRequest<GetByGidOrganizationItemMessageResponse>
    {
        public Guid Gid { get; set; }

        public class GetByGidOrganizationItemMessageQueryHandler : IRequestHandler<GetByGidOrganizationItemMessageQuery, GetByGidOrganizationItemMessageResponse>
        {
            private readonly IMapper _mapper;
            private readonly IOrganizationItemMessageReadRepository _organizationItemMessageReadRepository;
            private readonly OrganizationItemMessageBusinessRules _organizationItemMessageBusinessRules;

            public GetByGidOrganizationItemMessageQueryHandler(IMapper mapper, IOrganizationItemMessageReadRepository organizationItemMessageReadRepository, OrganizationItemMessageBusinessRules organizationItemMessageBusinessRules)
            {
                _mapper = mapper;
                _organizationItemMessageReadRepository = organizationItemMessageReadRepository;
                _organizationItemMessageBusinessRules = organizationItemMessageBusinessRules;
            }

            public async Task<GetByGidOrganizationItemMessageResponse> Handle(GetByGidOrganizationItemMessageQuery request, CancellationToken cancellationToken)
            {
                X.OrganizationItemMessage? organizationItemMessage = await _organizationItemMessageReadRepository.GetAsync(predicate: uc => uc.Gid == request.Gid,
                 include: i => i.Include(i => i.UserFK).Include(i => i.OrganizationItemFK), cancellationToken: cancellationToken);

                await _organizationItemMessageBusinessRules.OrganizationItemMessageShouldExistWhenSelected(organizationItemMessage);

                GetByGidOrganizationItemMessageResponse response = _mapper.Map<GetByGidOrganizationItemMessageResponse>(organizationItemMessage);
                return response;
            }
        }
    }
}
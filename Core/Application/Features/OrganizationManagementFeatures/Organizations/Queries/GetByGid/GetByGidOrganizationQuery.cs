using Application.Features.OrganizationManagementFeatures.Organizations.Rules;
using Application.Repositories.OrganizationManagementRepos.OrganizationRepo;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using X = Domain.Entities.OrganizationManagements;

namespace Application.Features.OrganizationManagementFeatures.Organizations.Queries.GetByGid
{
    public class GetByGidOrganizationQuery : IRequest<GetByGidOrganizationResponse>
    {
        public Guid Gid { get; set; }

        public class GetByGidOrganizationQueryHandler : IRequestHandler<GetByGidOrganizationQuery, GetByGidOrganizationResponse>
        {
            private readonly IMapper _mapper;
            private readonly IOrganizationReadRepository _organizationReadRepository;
            private readonly OrganizationBusinessRules _organizationBusinessRules;

            public GetByGidOrganizationQueryHandler(IMapper mapper, IOrganizationReadRepository organizationReadRepository, OrganizationBusinessRules organizationBusinessRules)
            {
                _mapper = mapper;
                _organizationReadRepository = organizationReadRepository;
                _organizationBusinessRules = organizationBusinessRules;
            }

            public async Task<GetByGidOrganizationResponse> Handle(GetByGidOrganizationQuery request, CancellationToken cancellationToken)
            {
                X.Organization organization = await _organizationReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, include: x => x.Include(x => x.SCCompanyFK).Include(x => x.OrganizationTypeFK).Include(x => x.ResponsibleUserFK));

                await _organizationBusinessRules.OrganizationShouldExistWhenSelected(organization);

                GetByGidOrganizationResponse response = _mapper.Map<GetByGidOrganizationResponse>(organization);
                return response;
            }
        }
    }
}
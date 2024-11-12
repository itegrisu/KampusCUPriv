using Application.Features.OrganizationManagementFeatures.OrganizationItemFiles.Rules;
using Application.Repositories.OrganizationManagementRepos.OrganizationItemFileRepo;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using X = Domain.Entities.OrganizationManagements;

namespace Application.Features.OrganizationManagementFeatures.OrganizationItemFiles.Queries.GetByGid
{
    public class GetByGidOrganizationItemFileQuery : IRequest<GetByGidOrganizationItemFileResponse>
    {
        public Guid Gid { get; set; }

        public class GetByGidOrganizationItemFileQueryHandler : IRequestHandler<GetByGidOrganizationItemFileQuery, GetByGidOrganizationItemFileResponse>
        {
            private readonly IMapper _mapper;
            private readonly IOrganizationItemFileReadRepository _organizationItemFileReadRepository;
            private readonly OrganizationItemFileBusinessRules _organizationItemFileBusinessRules;

            public GetByGidOrganizationItemFileQueryHandler(IMapper mapper, IOrganizationItemFileReadRepository organizationItemFileReadRepository, OrganizationItemFileBusinessRules organizationItemFileBusinessRules)
            {
                _mapper = mapper;
                _organizationItemFileReadRepository = organizationItemFileReadRepository;
                _organizationItemFileBusinessRules = organizationItemFileBusinessRules;
            }

            public async Task<GetByGidOrganizationItemFileResponse> Handle(GetByGidOrganizationItemFileQuery request, CancellationToken cancellationToken)
            {
                X.OrganizationItemFile? organizationItemFile = await _organizationItemFileReadRepository.GetAsync(predicate: uc => uc.Gid == request.Gid,
                include: i => i.Include(i => i.OrganizationItemFK), cancellationToken: cancellationToken);
                await _organizationItemFileBusinessRules.OrganizationItemFileShouldExistWhenSelected(organizationItemFile);

                GetByGidOrganizationItemFileResponse response = _mapper.Map<GetByGidOrganizationItemFileResponse>(organizationItemFile);
                return response;
            }
        }
    }
}
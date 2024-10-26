using AutoMapper;
using MediatR;
using X = Domain.Entities.OrganizationManagements;
using Microsoft.EntityFrameworkCore;
using Application.Repositories.OrganizationManagementRepos.OrganizationFileRepo;
using Application.Features.OrganizationManagementFeatures.OrganizationFiles.Rules;

namespace Application.Features.OrganizationManagementFeatures.OrganizationFiles.Queries.GetByGid
{
    public class GetByGidOrganizationFileQuery : IRequest<GetByGidOrganizationFileResponse>
    {
        public Guid Gid { get; set; }

        public class GetByGidOrganizationFileQueryHandler : IRequestHandler<GetByGidOrganizationFileQuery, GetByGidOrganizationFileResponse>
        {
            private readonly IMapper _mapper;
            private readonly IOrganizationFileReadRepository _organizationFileReadRepository;
            private readonly OrganizationFileBusinessRules _organizationFileBusinessRules;

            public GetByGidOrganizationFileQueryHandler(IMapper mapper, IOrganizationFileReadRepository organizationFileReadRepository, OrganizationFileBusinessRules organizationFileBusinessRules)
            {
                _mapper = mapper;
                _organizationFileReadRepository = organizationFileReadRepository;
                _organizationFileBusinessRules = organizationFileBusinessRules;
            }

            public async Task<GetByGidOrganizationFileResponse> Handle(GetByGidOrganizationFileQuery request, CancellationToken cancellationToken)
            {
                X.OrganizationFile? organizationFile = await _organizationFileReadRepository.GetAsync(predicate: uc => uc.Gid == request.Gid, cancellationToken: cancellationToken, include: x => x.Include(x => x.OrganizationFK));
                    //unutma
					//includes varsa eklenecek - Orn: Altta
					//include: i => i.Include(i => i.AcademicTitleFK).Include(i => i.UniversityFK)
                await _organizationFileBusinessRules.OrganizationFileShouldExistWhenSelected(organizationFile);

                GetByGidOrganizationFileResponse response = _mapper.Map<GetByGidOrganizationFileResponse>(organizationFile);
                return response;
            }
        }
    }
}
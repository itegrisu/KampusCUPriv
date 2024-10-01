using Application.Features.OrganizationManagementFeatures.OrganizationGroups.Constants;
using Application.Features.OrganizationManagementFeatures.OrganizationGroups.Queries.GetByGid;
using Application.Features.OrganizationManagementFeatures.OrganizationGroups.Rules;
using Application.Repositories.OrganizationManagementRepos.OrganizationGroupRepo;
using AutoMapper;
using Domain.Entities.OrganizationManagements;
using MediatR;
using Microsoft.EntityFrameworkCore;
using X = Domain.Entities.OrganizationManagements;


namespace Application.Features.OrganizationManagementFeatures.OrganizationGroups.Commands.Create;

public class CreateOrganizationGroupCommand : IRequest<CreatedOrganizationGroupResponse>
{
    public Guid GidOrganizationFK { get; set; }
    public string GroupName { get; set; }




    public class CreateOrganizationGroupCommandHandler : IRequestHandler<CreateOrganizationGroupCommand, CreatedOrganizationGroupResponse>
    {
        private readonly IMapper _mapper;
        private readonly IOrganizationGroupWriteRepository _organizationGroupWriteRepository;
        private readonly IOrganizationGroupReadRepository _organizationGroupReadRepository;
        private readonly OrganizationGroupBusinessRules _organizationGroupBusinessRules;

        public CreateOrganizationGroupCommandHandler(IMapper mapper, IOrganizationGroupWriteRepository organizationGroupWriteRepository,
                                         OrganizationGroupBusinessRules organizationGroupBusinessRules, IOrganizationGroupReadRepository organizationGroupReadRepository)
        {
            _mapper = mapper;
            _organizationGroupWriteRepository = organizationGroupWriteRepository;
            _organizationGroupBusinessRules = organizationGroupBusinessRules;
            _organizationGroupReadRepository = organizationGroupReadRepository;
        }

        public async Task<CreatedOrganizationGroupResponse> Handle(CreateOrganizationGroupCommand request, CancellationToken cancellationToken)
        {
            await _organizationGroupBusinessRules.OrganizationShouldExistWhenSelected(request.GidOrganizationFK);
            await _organizationGroupBusinessRules.OrganizationGroupNameShouldUniqe(request.GroupName);

            List<OrganizationGroup> organizationGroups = await _organizationGroupReadRepository.GetAll().ToListAsync();
            int maxRowNo = organizationGroups.Count() == 0 ? 0 : organizationGroups.Max(x => x.RowNo);


            X.OrganizationGroup organizationGroup = _mapper.Map<X.OrganizationGroup>(request);
            organizationGroup.RowNo = maxRowNo + 1;

            await _organizationGroupWriteRepository.AddAsync(organizationGroup);
            await _organizationGroupWriteRepository.SaveAsync();

            X.OrganizationGroup savedOrganizationGroup = await _organizationGroupReadRepository.GetAsync(predicate: x => x.Gid == organizationGroup.Gid, include: x => x.Include(x => x.OrganizationFK));
            //INCLUDES Buraya Gelecek include varsa eklenecek
            //include: x => x.Include(x => x.UserFK));

            GetByGidOrganizationGroupResponse obj = _mapper.Map<GetByGidOrganizationGroupResponse>(savedOrganizationGroup);
            return new()
            {
                Title = OrganizationGroupsBusinessMessages.ProcessCompleted,
                Message = OrganizationGroupsBusinessMessages.SuccessCreatedOrganizationGroupMessage,
                IsValid = true,
                Obj = obj
            };
        }
    }
}
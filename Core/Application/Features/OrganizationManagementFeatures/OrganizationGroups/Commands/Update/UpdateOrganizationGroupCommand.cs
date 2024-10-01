using Application.Features.OrganizationManagementFeatures.OrganizationGroups.Constants;
using Application.Features.OrganizationManagementFeatures.OrganizationGroups.Queries.GetByGid;
using Application.Features.OrganizationManagementFeatures.OrganizationGroups.Rules;
using Application.Repositories.OrganizationManagementRepos.OrganizationGroupRepo;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using X = Domain.Entities.OrganizationManagements;

namespace Application.Features.OrganizationManagementFeatures.OrganizationGroups.Commands.Update;

public class UpdateOrganizationGroupCommand : IRequest<UpdatedOrganizationGroupResponse>
{
    public Guid Gid { get; set; }

    public Guid GidOrganizationFK { get; set; }

    public string GroupName { get; set; }




    public class UpdateOrganizationGroupCommandHandler : IRequestHandler<UpdateOrganizationGroupCommand, UpdatedOrganizationGroupResponse>
    {
        private readonly IMapper _mapper;
        private readonly IOrganizationGroupWriteRepository _organizationGroupWriteRepository;
        private readonly IOrganizationGroupReadRepository _organizationGroupReadRepository;
        private readonly OrganizationGroupBusinessRules _organizationGroupBusinessRules;

        public UpdateOrganizationGroupCommandHandler(IMapper mapper, IOrganizationGroupWriteRepository organizationGroupWriteRepository,
                                         OrganizationGroupBusinessRules organizationGroupBusinessRules, IOrganizationGroupReadRepository organizationGroupReadRepository)
        {
            _mapper = mapper;
            _organizationGroupWriteRepository = organizationGroupWriteRepository;
            _organizationGroupBusinessRules = organizationGroupBusinessRules;
            _organizationGroupReadRepository = organizationGroupReadRepository;
        }

        public async Task<UpdatedOrganizationGroupResponse> Handle(UpdateOrganizationGroupCommand request, CancellationToken cancellationToken)
        {
            X.OrganizationGroup? organizationGroup = await _organizationGroupReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken);
            //INCLUDES Buraya Gelecek include varsa eklenecek
            await _organizationGroupBusinessRules.OrganizationGroupShouldExistWhenSelected(organizationGroup);
            await _organizationGroupBusinessRules.OrganizationShouldExistWhenSelected(request.GidOrganizationFK);
            await _organizationGroupBusinessRules.OrganizationGroupNameShouldUniqe(request.GroupName, request.Gid);
            organizationGroup = _mapper.Map(request, organizationGroup);

            _organizationGroupWriteRepository.Update(organizationGroup!);
            await _organizationGroupWriteRepository.SaveAsync();
            X.OrganizationGroup updatedOrganizationGroup = await _organizationGroupReadRepository.GetAsync(predicate: x => x.Gid == organizationGroup.Gid, include: x => x.Include(x => x.OrganizationFK));
            GetByGidOrganizationGroupResponse obj = _mapper.Map<GetByGidOrganizationGroupResponse>(updatedOrganizationGroup);

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
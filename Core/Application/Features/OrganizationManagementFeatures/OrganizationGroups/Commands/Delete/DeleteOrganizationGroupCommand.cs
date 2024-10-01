using Application.Features.OrganizationManagementFeatures.OrganizationGroups.Constants;
using Application.Features.OrganizationManagementFeatures.OrganizationGroups.Rules;
using Application.Repositories.OrganizationManagementRepos.OrganizationGroupRepo;
using AutoMapper;
using X = Domain.Entities.OrganizationManagements;
using MediatR;

namespace Application.Features.OrganizationManagementFeatures.OrganizationGroups.Commands.Delete;

public class DeleteOrganizationGroupCommand : IRequest<DeletedOrganizationGroupResponse>
{
	public Guid Gid { get; set; }

    public class DeleteOrganizationGroupCommandHandler : IRequestHandler<DeleteOrganizationGroupCommand, DeletedOrganizationGroupResponse>
    {
        private readonly IMapper _mapper;
        private readonly IOrganizationGroupReadRepository _organizationGroupReadRepository;
        private readonly IOrganizationGroupWriteRepository _organizationGroupWriteRepository;
        private readonly OrganizationGroupBusinessRules _organizationGroupBusinessRules;

        public DeleteOrganizationGroupCommandHandler(IMapper mapper, IOrganizationGroupReadRepository organizationGroupReadRepository,
                                         OrganizationGroupBusinessRules organizationGroupBusinessRules, IOrganizationGroupWriteRepository organizationGroupWriteRepository)
        {
            _mapper = mapper;
            _organizationGroupReadRepository = organizationGroupReadRepository;
            _organizationGroupBusinessRules = organizationGroupBusinessRules;
            _organizationGroupWriteRepository = organizationGroupWriteRepository;
        }

        public async Task<DeletedOrganizationGroupResponse> Handle(DeleteOrganizationGroupCommand request, CancellationToken cancellationToken)
        {
            X.OrganizationGroup? organizationGroup = await _organizationGroupReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken);
            await _organizationGroupBusinessRules.OrganizationGroupShouldExistWhenSelected(organizationGroup);
            organizationGroup.DataState = Core.Enum.DataState.Deleted;

            _organizationGroupWriteRepository.Update(organizationGroup);
            await _organizationGroupWriteRepository.SaveAsync();

            return new()
            {
                Title = OrganizationGroupsBusinessMessages.ProcessCompleted,
                Message = OrganizationGroupsBusinessMessages.SuccessDeletedOrganizationGroupMessage,
                IsValid = true
            };
        }
    }
}
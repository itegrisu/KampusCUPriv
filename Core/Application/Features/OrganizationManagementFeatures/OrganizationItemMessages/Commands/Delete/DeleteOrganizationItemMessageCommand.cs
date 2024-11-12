using Application.Features.OrganizationManagementFeatures.OrganizationItemMessages.Constants;
using Application.Features.OrganizationManagementFeatures.OrganizationItemMessages.Rules;
using Application.Repositories.OrganizationManagementRepos.OrganizationItemMessageRepo;
using AutoMapper;
using X = Domain.Entities.OrganizationManagements;
using MediatR;

namespace Application.Features.OrganizationManagementFeatures.OrganizationItemMessages.Commands.Delete;

public class DeleteOrganizationItemMessageCommand : IRequest<DeletedOrganizationItemMessageResponse>
{
	public Guid Gid { get; set; }

    public class DeleteOrganizationItemMessageCommandHandler : IRequestHandler<DeleteOrganizationItemMessageCommand, DeletedOrganizationItemMessageResponse>
    {
        private readonly IMapper _mapper;
        private readonly IOrganizationItemMessageReadRepository _organizationItemMessageReadRepository;
        private readonly IOrganizationItemMessageWriteRepository _organizationItemMessageWriteRepository;
        private readonly OrganizationItemMessageBusinessRules _organizationItemMessageBusinessRules;

        public DeleteOrganizationItemMessageCommandHandler(IMapper mapper, IOrganizationItemMessageReadRepository organizationItemMessageReadRepository,
                                         OrganizationItemMessageBusinessRules organizationItemMessageBusinessRules, IOrganizationItemMessageWriteRepository organizationItemMessageWriteRepository)
        {
            _mapper = mapper;
            _organizationItemMessageReadRepository = organizationItemMessageReadRepository;
            _organizationItemMessageBusinessRules = organizationItemMessageBusinessRules;
            _organizationItemMessageWriteRepository = organizationItemMessageWriteRepository;
        }

        public async Task<DeletedOrganizationItemMessageResponse> Handle(DeleteOrganizationItemMessageCommand request, CancellationToken cancellationToken)
        {
            X.OrganizationItemMessage? organizationItemMessage = await _organizationItemMessageReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken);
            await _organizationItemMessageBusinessRules.OrganizationItemMessageShouldExistWhenSelected(organizationItemMessage);
            organizationItemMessage.DataState = Core.Enum.DataState.Deleted;

            _organizationItemMessageWriteRepository.Update(organizationItemMessage);
            await _organizationItemMessageWriteRepository.SaveAsync();

            return new()
            {
                Title = OrganizationItemMessagesBusinessMessages.ProcessCompleted,
                Message = OrganizationItemMessagesBusinessMessages.SuccessDeletedOrganizationItemMessageMessage,
                IsValid = true
            };
        }
    }
}
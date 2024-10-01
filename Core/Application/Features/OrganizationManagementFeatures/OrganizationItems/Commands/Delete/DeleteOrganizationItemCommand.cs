using Application.Features.OrganizationManagementFeatures.OrganizationItems.Constants;
using Application.Features.OrganizationManagementFeatures.OrganizationItems.Rules;
using Application.Repositories.OrganizationManagementRepos.OrganizationItemRepo;
using AutoMapper;
using X = Domain.Entities.OrganizationManagements;
using MediatR;

namespace Application.Features.OrganizationManagementFeatures.OrganizationItems.Commands.Delete;

public class DeleteOrganizationItemCommand : IRequest<DeletedOrganizationItemResponse>
{
	public Guid Gid { get; set; }

    public class DeleteOrganizationItemCommandHandler : IRequestHandler<DeleteOrganizationItemCommand, DeletedOrganizationItemResponse>
    {
        private readonly IMapper _mapper;
        private readonly IOrganizationItemReadRepository _organizationItemReadRepository;
        private readonly IOrganizationItemWriteRepository _organizationItemWriteRepository;
        private readonly OrganizationItemBusinessRules _organizationItemBusinessRules;

        public DeleteOrganizationItemCommandHandler(IMapper mapper, IOrganizationItemReadRepository organizationItemReadRepository,
                                         OrganizationItemBusinessRules organizationItemBusinessRules, IOrganizationItemWriteRepository organizationItemWriteRepository)
        {
            _mapper = mapper;
            _organizationItemReadRepository = organizationItemReadRepository;
            _organizationItemBusinessRules = organizationItemBusinessRules;
            _organizationItemWriteRepository = organizationItemWriteRepository;
        }

        public async Task<DeletedOrganizationItemResponse> Handle(DeleteOrganizationItemCommand request, CancellationToken cancellationToken)
        {
            X.OrganizationItem? organizationItem = await _organizationItemReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken);
            await _organizationItemBusinessRules.OrganizationItemShouldExistWhenSelected(organizationItem);
            organizationItem.DataState = Core.Enum.DataState.Deleted;

            _organizationItemWriteRepository.Update(organizationItem);
            await _organizationItemWriteRepository.SaveAsync();

            return new()
            {
                Title = OrganizationItemsBusinessMessages.ProcessCompleted,
                Message = OrganizationItemsBusinessMessages.SuccessDeletedOrganizationItemMessage,
                IsValid = true
            };
        }
    }
}
using Application.Features.DefinitionManagementFeatures.OrganizationTypes.Constants;
using Application.Features.DefinitionManagementFeatures.OrganizationTypes.Rules;
using Application.Repositories.DefinitionManagementRepos.OrganizationTypeRepo;
using AutoMapper;
using X = Domain.Entities.DefinitionManagements;
using MediatR;

namespace Application.Features.DefinitionManagementFeatures.OrganizationTypes.Commands.Delete;

public class DeleteOrganizationTypeCommand : IRequest<DeletedOrganizationTypeResponse>
{
	public Guid Gid { get; set; }

    public class DeleteOrganizationTypeCommandHandler : IRequestHandler<DeleteOrganizationTypeCommand, DeletedOrganizationTypeResponse>
    {
        private readonly IMapper _mapper;
        private readonly IOrganizationTypeReadRepository _organizationTypeReadRepository;
        private readonly IOrganizationTypeWriteRepository _organizationTypeWriteRepository;
        private readonly OrganizationTypeBusinessRules _organizationTypeBusinessRules;

        public DeleteOrganizationTypeCommandHandler(IMapper mapper, IOrganizationTypeReadRepository organizationTypeReadRepository,
                                         OrganizationTypeBusinessRules organizationTypeBusinessRules, IOrganizationTypeWriteRepository organizationTypeWriteRepository)
        {
            _mapper = mapper;
            _organizationTypeReadRepository = organizationTypeReadRepository;
            _organizationTypeBusinessRules = organizationTypeBusinessRules;
            _organizationTypeWriteRepository = organizationTypeWriteRepository;
        }

        public async Task<DeletedOrganizationTypeResponse> Handle(DeleteOrganizationTypeCommand request, CancellationToken cancellationToken)
        {
            X.OrganizationType? organizationType = await _organizationTypeReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken);
            await _organizationTypeBusinessRules.OrganizationTypeShouldExistWhenSelected(organizationType);
            organizationType.DataState = Core.Enum.DataState.Deleted;

            _organizationTypeWriteRepository.Update(organizationType);
            await _organizationTypeWriteRepository.SaveAsync();

            return new()
            {
                Title = OrganizationTypesBusinessMessages.ProcessCompleted,
                Message = OrganizationTypesBusinessMessages.SuccessDeletedOrganizationTypeMessage,
                IsValid = true
            };
        }
    }
}
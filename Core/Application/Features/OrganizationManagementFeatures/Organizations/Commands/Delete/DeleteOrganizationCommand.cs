using Application.Features.OrganizationManagementFeatures.Organizations.Constants;
using Application.Features.OrganizationManagementFeatures.Organizations.Rules;
using Application.Repositories.OrganizationManagementRepos.OrganizationRepo;
using AutoMapper;
using X = Domain.Entities.OrganizationManagements;
using MediatR;

namespace Application.Features.OrganizationManagementFeatures.Organizations.Commands.Delete;

public class DeleteOrganizationCommand : IRequest<DeletedOrganizationResponse>
{
	public Guid Gid { get; set; }

    public class DeleteOrganizationCommandHandler : IRequestHandler<DeleteOrganizationCommand, DeletedOrganizationResponse>
    {
        private readonly IMapper _mapper;
        private readonly IOrganizationReadRepository _organizationReadRepository;
        private readonly IOrganizationWriteRepository _organizationWriteRepository;
        private readonly OrganizationBusinessRules _organizationBusinessRules;

        public DeleteOrganizationCommandHandler(IMapper mapper, IOrganizationReadRepository organizationReadRepository,
                                         OrganizationBusinessRules organizationBusinessRules, IOrganizationWriteRepository organizationWriteRepository)
        {
            _mapper = mapper;
            _organizationReadRepository = organizationReadRepository;
            _organizationBusinessRules = organizationBusinessRules;
            _organizationWriteRepository = organizationWriteRepository;
        }

        public async Task<DeletedOrganizationResponse> Handle(DeleteOrganizationCommand request, CancellationToken cancellationToken)
        {
            X.Organization? organization = await _organizationReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken);
            await _organizationBusinessRules.OrganizationShouldExistWhenSelected(organization);
            organization.DataState = Core.Enum.DataState.Deleted;

            _organizationWriteRepository.Update(organization);
            await _organizationWriteRepository.SaveAsync();

            return new()
            {
                Title = OrganizationsBusinessMessages.ProcessCompleted,
                Message = OrganizationsBusinessMessages.SuccessDeletedOrganizationMessage,
                IsValid = true
            };
        }
    }
}
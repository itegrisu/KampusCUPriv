using Application.Features.OrganizationManagementFeatures.OrganizationItemFiles.Constants;
using Application.Features.OrganizationManagementFeatures.OrganizationItemFiles.Rules;
using Application.Repositories.OrganizationManagementRepos.OrganizationItemFileRepo;
using AutoMapper;
using X = Domain.Entities.OrganizationManagements;
using MediatR;

namespace Application.Features.OrganizationManagementFeatures.OrganizationItemFiles.Commands.Delete;

public class DeleteOrganizationItemFileCommand : IRequest<DeletedOrganizationItemFileResponse>
{
	public Guid Gid { get; set; }

    public class DeleteOrganizationItemFileCommandHandler : IRequestHandler<DeleteOrganizationItemFileCommand, DeletedOrganizationItemFileResponse>
    {
        private readonly IMapper _mapper;
        private readonly IOrganizationItemFileReadRepository _organizationItemFileReadRepository;
        private readonly IOrganizationItemFileWriteRepository _organizationItemFileWriteRepository;
        private readonly OrganizationItemFileBusinessRules _organizationItemFileBusinessRules;

        public DeleteOrganizationItemFileCommandHandler(IMapper mapper, IOrganizationItemFileReadRepository organizationItemFileReadRepository,
                                         OrganizationItemFileBusinessRules organizationItemFileBusinessRules, IOrganizationItemFileWriteRepository organizationItemFileWriteRepository)
        {
            _mapper = mapper;
            _organizationItemFileReadRepository = organizationItemFileReadRepository;
            _organizationItemFileBusinessRules = organizationItemFileBusinessRules;
            _organizationItemFileWriteRepository = organizationItemFileWriteRepository;
        }

        public async Task<DeletedOrganizationItemFileResponse> Handle(DeleteOrganizationItemFileCommand request, CancellationToken cancellationToken)
        {
            X.OrganizationItemFile? organizationItemFile = await _organizationItemFileReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken);
            await _organizationItemFileBusinessRules.OrganizationItemFileShouldExistWhenSelected(organizationItemFile);
            organizationItemFile.DataState = Core.Enum.DataState.Deleted;

            _organizationItemFileWriteRepository.Update(organizationItemFile);
            await _organizationItemFileWriteRepository.SaveAsync();

            return new()
            {
                Title = OrganizationItemFilesBusinessMessages.ProcessCompleted,
                Message = OrganizationItemFilesBusinessMessages.SuccessDeletedOrganizationItemFileMessage,
                IsValid = true
            };
        }
    }
}
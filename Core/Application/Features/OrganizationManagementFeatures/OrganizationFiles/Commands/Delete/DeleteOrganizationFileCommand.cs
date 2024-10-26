using Application.Features.OrganizationManagementFeatures.OrganizationFiles.Constants;
using Application.Features.OrganizationManagementFeatures.OrganizationFiles.Rules;
using Application.Repositories.OrganizationManagementRepos.OrganizationFileRepo;
using AutoMapper;
using X = Domain.Entities.OrganizationManagements;
using MediatR;

namespace Application.Features.OrganizationManagementFeatures.OrganizationFiles.Commands.Delete;

public class DeleteOrganizationFileCommand : IRequest<DeletedOrganizationFileResponse>
{
	public Guid Gid { get; set; }

    public class DeleteOrganizationFileCommandHandler : IRequestHandler<DeleteOrganizationFileCommand, DeletedOrganizationFileResponse>
    {
        private readonly IMapper _mapper;
        private readonly IOrganizationFileReadRepository _organizationFileReadRepository;
        private readonly IOrganizationFileWriteRepository _organizationFileWriteRepository;
        private readonly OrganizationFileBusinessRules _organizationFileBusinessRules;

        public DeleteOrganizationFileCommandHandler(IMapper mapper, IOrganizationFileReadRepository organizationFileReadRepository,
                                         OrganizationFileBusinessRules organizationFileBusinessRules, IOrganizationFileWriteRepository organizationFileWriteRepository)
        {
            _mapper = mapper;
            _organizationFileReadRepository = organizationFileReadRepository;
            _organizationFileBusinessRules = organizationFileBusinessRules;
            _organizationFileWriteRepository = organizationFileWriteRepository;
        }

        public async Task<DeletedOrganizationFileResponse> Handle(DeleteOrganizationFileCommand request, CancellationToken cancellationToken)
        {
            X.OrganizationFile? organizationFile = await _organizationFileReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken);
            await _organizationFileBusinessRules.OrganizationFileShouldExistWhenSelected(organizationFile);
            organizationFile.DataState = Core.Enum.DataState.Deleted;

            _organizationFileWriteRepository.Update(organizationFile);
            await _organizationFileWriteRepository.SaveAsync();

            return new()
            {
                Title = OrganizationFilesBusinessMessages.ProcessCompleted,
                Message = OrganizationFilesBusinessMessages.SuccessDeletedOrganizationFileMessage,
                IsValid = true
            };
        }
    }
}
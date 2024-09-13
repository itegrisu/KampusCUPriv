using Application.Features.DefinitionManagementFeatures.DocumentTypes.Constants;
using Application.Features.DefinitionManagementFeatures.DocumentTypes.Rules;
using Application.Repositories.DefinitionManagementRepos.DocumentTypeRepo;
using AutoMapper;
using X = Domain.Entities.DefinitionManagements;
using MediatR;

namespace Application.Features.DefinitionManagementFeatures.DocumentTypes.Commands.Delete;

public class DeleteDocumentTypeCommand : IRequest<DeletedDocumentTypeResponse>
{
	public Guid Gid { get; set; }

    public class DeleteDocumentTypeCommandHandler : IRequestHandler<DeleteDocumentTypeCommand, DeletedDocumentTypeResponse>
    {
        private readonly IMapper _mapper;
        private readonly IDocumentTypeReadRepository _documentTypeReadRepository;
        private readonly IDocumentTypeWriteRepository _documentTypeWriteRepository;
        private readonly DocumentTypeBusinessRules _documentTypeBusinessRules;

        public DeleteDocumentTypeCommandHandler(IMapper mapper, IDocumentTypeReadRepository documentTypeReadRepository,
                                         DocumentTypeBusinessRules documentTypeBusinessRules, IDocumentTypeWriteRepository documentTypeWriteRepository)
        {
            _mapper = mapper;
            _documentTypeReadRepository = documentTypeReadRepository;
            _documentTypeBusinessRules = documentTypeBusinessRules;
            _documentTypeWriteRepository = documentTypeWriteRepository;
        }

        public async Task<DeletedDocumentTypeResponse> Handle(DeleteDocumentTypeCommand request, CancellationToken cancellationToken)
        {
            X.DocumentType? documentType = await _documentTypeReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken);
            await _documentTypeBusinessRules.DocumentTypeShouldExistWhenSelected(documentType);
            documentType.DataState = Core.Enum.DataState.Deleted;

            _documentTypeWriteRepository.Update(documentType);
            await _documentTypeWriteRepository.SaveAsync();

            return new()
            {
                Title = DocumentTypesBusinessMessages.ProcessCompleted,
                Message = DocumentTypesBusinessMessages.SuccessDeletedDocumentTypeMessage,
                IsValid = true
            };
        }
    }
}
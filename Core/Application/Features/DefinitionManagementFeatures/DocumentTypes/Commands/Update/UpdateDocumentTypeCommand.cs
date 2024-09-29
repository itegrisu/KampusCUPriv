using Application.Features.DefinitionManagementFeatures.DocumentTypes.Constants;
using Application.Features.DefinitionManagementFeatures.DocumentTypes.Queries.GetByGid;
using Application.Features.DefinitionManagementFeatures.DocumentTypes.Rules;
using Application.Repositories.DefinitionManagementRepos.DocumentTypeRepo;
using AutoMapper;
using MediatR;
using X = Domain.Entities.DefinitionManagements;

namespace Application.Features.DefinitionManagementFeatures.DocumentTypes.Commands.Update;

public class UpdateDocumentTypeCommand : IRequest<UpdatedDocumentTypeResponse>
{
    public Guid Gid { get; set; }
    public string Name { get; set; }

    public class UpdateDocumentTypeCommandHandler : IRequestHandler<UpdateDocumentTypeCommand, UpdatedDocumentTypeResponse>
    {
        private readonly IMapper _mapper;
        private readonly IDocumentTypeWriteRepository _documentTypeWriteRepository;
        private readonly IDocumentTypeReadRepository _documentTypeReadRepository;
        private readonly DocumentTypeBusinessRules _documentTypeBusinessRules;

        public UpdateDocumentTypeCommandHandler(IMapper mapper, IDocumentTypeWriteRepository documentTypeWriteRepository,
                                         DocumentTypeBusinessRules documentTypeBusinessRules, IDocumentTypeReadRepository documentTypeReadRepository)
        {
            _mapper = mapper;
            _documentTypeWriteRepository = documentTypeWriteRepository;
            _documentTypeBusinessRules = documentTypeBusinessRules;
            _documentTypeReadRepository = documentTypeReadRepository;
        }

        public async Task<UpdatedDocumentTypeResponse> Handle(UpdateDocumentTypeCommand request, CancellationToken cancellationToken)
        {
            X.DocumentType? documentType = await _documentTypeReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken);
            //INCLUDES Buraya Gelecek include varsa eklenecek
            await _documentTypeBusinessRules.DocumentTypeShouldExistWhenSelected(documentType);
            await _documentTypeBusinessRules.DocumentNameIsUnique(request.Name, request.Gid);
            documentType = _mapper.Map(request, documentType);

            _documentTypeWriteRepository.Update(documentType!);
            await _documentTypeWriteRepository.SaveAsync();
            GetByGidDocumentTypeResponse obj = _mapper.Map<GetByGidDocumentTypeResponse>(documentType);

            return new()
            {
                Title = DocumentTypesBusinessMessages.ProcessCompleted,
                Message = DocumentTypesBusinessMessages.SuccessCreatedDocumentTypeMessage,
                IsValid = true,
                Obj = obj
            };
        }
    }
}
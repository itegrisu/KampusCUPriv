using Application.Features.DefinitionManagementFeatures.DocumentTypes.Constants;
using Application.Features.DefinitionManagementFeatures.DocumentTypes.Queries.GetByGid;
using Application.Features.DefinitionManagementFeatures.DocumentTypes.Rules;
using Application.Repositories.DefinitionManagementRepos.DocumentTypeRepo;
using AutoMapper;
using MediatR;
using X = Domain.Entities.DefinitionManagements;

namespace Application.Features.DefinitionManagementFeatures.DocumentTypes.Commands.Create;

public class CreateDocumentTypeCommand : IRequest<CreatedDocumentTypeResponse>
{

    public string BelgeAdi { get; set; }

    public class CreateDocumentTypeCommandHandler : IRequestHandler<CreateDocumentTypeCommand, CreatedDocumentTypeResponse>
    {
        private readonly IMapper _mapper;
        private readonly IDocumentTypeWriteRepository _documentTypeWriteRepository;
        private readonly IDocumentTypeReadRepository _documentTypeReadRepository;
        private readonly DocumentTypeBusinessRules _documentTypeBusinessRules;

        public CreateDocumentTypeCommandHandler(IMapper mapper, IDocumentTypeWriteRepository documentTypeWriteRepository,
                                         DocumentTypeBusinessRules documentTypeBusinessRules, IDocumentTypeReadRepository documentTypeReadRepository)
        {
            _mapper = mapper;
            _documentTypeWriteRepository = documentTypeWriteRepository;
            _documentTypeBusinessRules = documentTypeBusinessRules;
            _documentTypeReadRepository = documentTypeReadRepository;
        }

        public async Task<CreatedDocumentTypeResponse> Handle(CreateDocumentTypeCommand request, CancellationToken cancellationToken)
        {

            await _documentTypeBusinessRules.DocumentNameIsUnique(request.BelgeAdi);

            X.DocumentType documentType = _mapper.Map<X.DocumentType>(request);

            await _documentTypeWriteRepository.AddAsync(documentType);
            await _documentTypeWriteRepository.SaveAsync();

            X.DocumentType savedDocumentType = await _documentTypeReadRepository.GetAsync(predicate: x => x.Gid == documentType.Gid);


            GetByGidDocumentTypeResponse obj = _mapper.Map<GetByGidDocumentTypeResponse>(savedDocumentType);
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
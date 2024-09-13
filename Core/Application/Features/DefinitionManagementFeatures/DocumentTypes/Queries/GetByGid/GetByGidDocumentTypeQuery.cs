using Application.Features.DefinitionManagementFeatures.DocumentTypes.Rules;
using Application.Repositories.DefinitionManagementRepos.DocumentTypeRepo;
using AutoMapper;
using MediatR;
using X = Domain.Entities.DefinitionManagements;

namespace Application.Features.DefinitionManagementFeatures.DocumentTypes.Queries.GetByGid
{
    public class GetByGidDocumentTypeQuery : IRequest<GetByGidDocumentTypeResponse>
    {
        public Guid Gid { get; set; }

        public class GetByGidDocumentTypeQueryHandler : IRequestHandler<GetByGidDocumentTypeQuery, GetByGidDocumentTypeResponse>
        {
            private readonly IMapper _mapper;
            private readonly IDocumentTypeReadRepository _documentTypeReadRepository;
            private readonly DocumentTypeBusinessRules _documentTypeBusinessRules;

            public GetByGidDocumentTypeQueryHandler(IMapper mapper, IDocumentTypeReadRepository documentTypeReadRepository, DocumentTypeBusinessRules documentTypeBusinessRules)
            {
                _mapper = mapper;
                _documentTypeReadRepository = documentTypeReadRepository;
                _documentTypeBusinessRules = documentTypeBusinessRules;
            }

            public async Task<GetByGidDocumentTypeResponse> Handle(GetByGidDocumentTypeQuery request, CancellationToken cancellationToken)
            {
                X.DocumentType? documentType = await _documentTypeReadRepository.GetAsync(predicate: uc => uc.Gid == request.Gid, cancellationToken: cancellationToken);

                await _documentTypeBusinessRules.DocumentTypeShouldExistWhenSelected(documentType);

                GetByGidDocumentTypeResponse response = _mapper.Map<GetByGidDocumentTypeResponse>(documentType);
                return response;
            }
        }
    }
}
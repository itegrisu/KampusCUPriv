using Application.Helpers.PaginationHelpers;
using Application.Repositories.DefinitionManagementRepos.DocumentTypeRepo;
using AutoMapper;
using Core.Application.Request;
using Core.Application.Responses;
using Core.Persistence.Paging;
using MediatR;
using X = Domain.Entities.DefinitionManagements;

namespace Application.Features.DefinitionManagementFeatures.DocumentTypes.Queries.GetList;

public class GetListDocumentTypeQuery : IRequest<GetListResponse<GetListDocumentTypeListItemDto>>
{
    public PageRequest PageRequest { get; set; }

    public class GetListDocumentTypeQueryHandler : IRequestHandler<GetListDocumentTypeQuery, GetListResponse<GetListDocumentTypeListItemDto>>
    {
        private readonly IDocumentTypeReadRepository _documentTypeReadRepository;
        private readonly IMapper _mapper;
        private readonly NoPagination<X.DocumentType, GetListDocumentTypeListItemDto> _noPagination;

        public GetListDocumentTypeQueryHandler(IDocumentTypeReadRepository documentTypeReadRepository, IMapper mapper, NoPagination<X.DocumentType, GetListDocumentTypeListItemDto> noPagination)
        {
            _documentTypeReadRepository = documentTypeReadRepository;
            _mapper = mapper;
            _noPagination = noPagination;
        }

        public async Task<GetListResponse<GetListDocumentTypeListItemDto>> Handle(GetListDocumentTypeQuery request, CancellationToken cancellationToken)
        {
            if (request.PageRequest.PageIndex == -1)
                return await _noPagination.NoPaginationData(cancellationToken);


            IPaginate<X.DocumentType> documentTypes = await _documentTypeReadRepository.GetListAllAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListDocumentTypeListItemDto> response = _mapper.Map<GetListResponse<GetListDocumentTypeListItemDto>>(documentTypes);
            return response;
        }
    }
}
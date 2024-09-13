using Application.Helpers.PaginationHelpers;
using Application.Repositories.DefinitionManagementRepos.ForeignLanguageRepo;
using AutoMapper;
using Core.Application.Request;
using Core.Application.Responses;
using Core.Persistence.Paging;
using MediatR;
using X = Domain.Entities.DefinitionManagements;

namespace Application.Features.DefinitionManagementFeatures.ForeignLanguages.Queries.GetList;

public class GetListForeignLanguageQuery : IRequest<GetListResponse<GetListForeignLanguageListItemDto>>
{
    public PageRequest PageRequest { get; set; }

    public class GetListForeignLanguageQueryHandler : IRequestHandler<GetListForeignLanguageQuery, GetListResponse<GetListForeignLanguageListItemDto>>
    {
        private readonly IForeignLanguageReadRepository _foreignLanguageReadRepository;
        private readonly IMapper _mapper;
        private readonly NoPagination<X.ForeignLanguage, GetListForeignLanguageListItemDto> _noPagination;

        public GetListForeignLanguageQueryHandler(IForeignLanguageReadRepository foreignLanguageReadRepository, IMapper mapper, NoPagination<X.ForeignLanguage, GetListForeignLanguageListItemDto> noPagination)
        {
            _foreignLanguageReadRepository = foreignLanguageReadRepository;
            _mapper = mapper;
            _noPagination = noPagination;
        }

        public async Task<GetListResponse<GetListForeignLanguageListItemDto>> Handle(GetListForeignLanguageQuery request, CancellationToken cancellationToken)
        {
            if (request.PageRequest.PageIndex == -1)
                return await _noPagination.NoPaginationData(cancellationToken);

            IPaginate<X.ForeignLanguage> foreignLanguages = await _foreignLanguageReadRepository.GetListAllAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListForeignLanguageListItemDto> response = _mapper.Map<GetListResponse<GetListForeignLanguageListItemDto>>(foreignLanguages);
            return response;
        }
    }
}
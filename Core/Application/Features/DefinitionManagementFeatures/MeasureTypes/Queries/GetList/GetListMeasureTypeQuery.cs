using Application.Helpers.PaginationHelpers;
using Application.Repositories.DefinitionManagementRepos.MeasureTypeRepo;
using AutoMapper;
using Core.Application.Request;
using Core.Application.Responses;
using Core.Persistence.Paging;
using MediatR;
using X = Domain.Entities.DefinitionManagements;

namespace Application.Features.DefinitionManagementFeatures.MeasureTypes.Queries.GetList;

public class GetListMeasureTypeQuery : IRequest<GetListResponse<GetListMeasureTypeListItemDto>>
{
    public PageRequest PageRequest { get; set; }

    public class GetListMeasureTypeQueryHandler : IRequestHandler<GetListMeasureTypeQuery, GetListResponse<GetListMeasureTypeListItemDto>>
    {
        private readonly IMeasureTypeReadRepository _measureTypeReadRepository;
        private readonly IMapper _mapper;
        private readonly NoPagination<X.MeasureType, GetListMeasureTypeListItemDto> _noPagination;

        public GetListMeasureTypeQueryHandler(IMeasureTypeReadRepository measureTypeReadRepository, IMapper mapper, NoPagination<X.MeasureType, GetListMeasureTypeListItemDto> noPagination)
        {
            _measureTypeReadRepository = measureTypeReadRepository;
            _mapper = mapper;
            _noPagination = noPagination;
        }

        public async Task<GetListResponse<GetListMeasureTypeListItemDto>> Handle(GetListMeasureTypeQuery request, CancellationToken cancellationToken)
        {
            if (request.PageRequest.PageIndex == -1)
                return await _noPagination.NoPaginationData(cancellationToken);

            IPaginate<X.MeasureType> measureTypes = await _measureTypeReadRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListMeasureTypeListItemDto> response = _mapper.Map<GetListResponse<GetListMeasureTypeListItemDto>>(measureTypes);
            return response;
        }
    }
}
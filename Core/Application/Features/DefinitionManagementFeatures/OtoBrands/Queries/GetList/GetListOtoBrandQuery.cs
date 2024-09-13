using Application.Helpers.PaginationHelpers;
using Application.Repositories.DefinitionManagementRepos.OtoBrandRepo;
using AutoMapper;
using Core.Application.Request;
using Core.Application.Responses;
using Core.Persistence.Paging;
using MediatR;
using X = Domain.Entities.DefinitionManagements;

namespace Application.Features.DefinitionManagementFeatures.OtoBrands.Queries.GetList;

public class GetListOtoBrandQuery : IRequest<GetListResponse<GetListOtoBrandListItemDto>>
{
    public PageRequest PageRequest { get; set; }

    public class GetListOtoBrandQueryHandler : IRequestHandler<GetListOtoBrandQuery, GetListResponse<GetListOtoBrandListItemDto>>
    {
        private readonly IOtoBrandReadRepository _otoBrandReadRepository;
        private readonly IMapper _mapper;
        private readonly NoPagination<X.OtoBrand, GetListOtoBrandListItemDto> _noPagination;

        public GetListOtoBrandQueryHandler(IOtoBrandReadRepository otoBrandReadRepository, IMapper mapper, NoPagination<X.OtoBrand, GetListOtoBrandListItemDto> noPagination)
        {
            _otoBrandReadRepository = otoBrandReadRepository;
            _mapper = mapper;
            _noPagination = noPagination;
        }

        public async Task<GetListResponse<GetListOtoBrandListItemDto>> Handle(GetListOtoBrandQuery request, CancellationToken cancellationToken)
        {
            if (request.PageRequest.PageIndex == -1)
                return await _noPagination.NoPaginationData(cancellationToken);

            IPaginate<X.OtoBrand> otoBrands = await _otoBrandReadRepository.GetListAllAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListOtoBrandListItemDto> response = _mapper.Map<GetListResponse<GetListOtoBrandListItemDto>>(otoBrands);
            return response;
        }
    }
}
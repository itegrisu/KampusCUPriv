using Application.Helpers.PaginationHelpers;
using Application.Repositories.DefinitionManagementRepos.StockCategoryRepo;
using AutoMapper;
using Core.Application.Request;
using Core.Application.Responses;
using Core.Persistence.Paging;
using X = Domain.Entities.DefinitionManagements;
using MediatR;
using System.Linq.Expressions;

namespace Application.Features.DefinitionManagementFeatures.StockCategories.Queries.GetList;

public class GetListStockCategoryQuery : IRequest<GetListResponse<GetListStockCategoryListItemDto>>
{
    public PageRequest PageRequest { get; set; }

    public class GetListStockCategoryQueryHandler : IRequestHandler<GetListStockCategoryQuery, GetListResponse<GetListStockCategoryListItemDto>>
    {
        private readonly IStockCategoryReadRepository _stockCategoryReadRepository;
        private readonly IMapper _mapper;
        private readonly NoPagination<X.StockCategory, GetListStockCategoryListItemDto> _noPagination;

        public GetListStockCategoryQueryHandler(IStockCategoryReadRepository stockCategoryReadRepository, IMapper mapper, NoPagination<X.StockCategory, GetListStockCategoryListItemDto> noPagination)
        {
            _stockCategoryReadRepository = stockCategoryReadRepository;
            _mapper = mapper;
            _noPagination = noPagination;
        }

        public async Task<GetListResponse<GetListStockCategoryListItemDto>> Handle(GetListStockCategoryQuery request, CancellationToken cancellationToken)
        {
            if (request.PageRequest.PageIndex == -1)
                //unutma
				//includes varsa eklenecek - Orn: Altta
				//return await _noPagination.NoPaginationData(cancellationToken, 
                //    includes: new Expression<Func<StockCategory, object>>[]
                //    {
                //       x => x.UserFK,
                //       x=> x.StockCategoryMembers
                //    });
				return await _noPagination.NoPaginationData(cancellationToken);
            IPaginate<X.StockCategory> stockCategorys = await _stockCategoryReadRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListStockCategoryListItemDto> response = _mapper.Map<GetListResponse<GetListStockCategoryListItemDto>>(stockCategorys);
            return response;
        }
    }
}
using Application.Helpers.PaginationHelpers;
using AutoMapper;
using Core.Application.Request;
using Core.Application.Responses;
using Core.Persistence.Paging;
using X = Domain.Entities.DefinitionManagements;
using MediatR;
using System.Linq.Expressions;
using Application.Repositories.DefinitionManagementRepo.CategoryRepo;
using Application.Abstractions.Redis;

namespace Application.Features.DefinitionFeatures.Categories.Queries.GetList;

public class GetListCategoryQuery : IRequest<GetListResponse<GetListCategoryListItemDto>>
{
    public PageRequest PageRequest { get; set; }

    public class GetListCategoryQueryHandler : IRequestHandler<GetListCategoryQuery, GetListResponse<GetListCategoryListItemDto>>
    {
        private readonly ICategoryReadRepository _categoryReadRepository;
        private readonly IMapper _mapper;
        private readonly NoPagination<X.Category, GetListCategoryListItemDto> _noPagination;
        private readonly IRedisCacheService _redisCacheService;
        public GetListCategoryQueryHandler(ICategoryReadRepository categoryReadRepository, IMapper mapper, NoPagination<X.Category, GetListCategoryListItemDto> noPagination, IRedisCacheService redisCacheService)
        {
            _categoryReadRepository = categoryReadRepository;
            _mapper = mapper;
            _noPagination = noPagination;
            _redisCacheService = redisCacheService;
        }

        public async Task<GetListResponse<GetListCategoryListItemDto>> Handle(GetListCategoryQuery request, CancellationToken cancellationToken)
        {
            // 1) Cache key olu�tur
            string key = $"Categories_{request.PageRequest.PageIndex}_{request.PageRequest.PageSize}";

            // 2) Cache kontrol�
            var cached = await _redisCacheService.GetAsync<GetListResponse<GetListCategoryListItemDto>>(key);
            if (cached is not null)
                return cached;

            // 3) As�l veri sorgusu
            GetListResponse<GetListCategoryListItemDto> response;
            if (request.PageRequest.PageIndex == -1)
            {
                response = await _noPagination.NoPaginationData(
                    cancellationToken,
                    orderBy: x => x.Name);
            }
            else
            {
                IPaginate<X.Category> categorys = await _categoryReadRepository.GetListAsync(
                    index: request.PageRequest.PageIndex,
                    size: request.PageRequest.PageSize,
                    cancellationToken: cancellationToken,
                    orderBy: x => x.OrderBy(o => o.Name)
                );

                response = _mapper.Map<GetListResponse<GetListCategoryListItemDto>>(categorys);
            }

            // 4) Sonu�lar� cache�e yaz
            await _redisCacheService.SetAsync(key, response, TimeSpan.FromMinutes(5));

            return response;
        }
    }
}
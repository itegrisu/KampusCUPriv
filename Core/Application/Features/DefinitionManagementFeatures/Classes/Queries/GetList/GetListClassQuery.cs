using Application.Helpers.PaginationHelpers;
using AutoMapper;
using Core.Application.Request;
using Core.Application.Responses;
using Core.Persistence.Paging;
using X = Domain.Entities.DefinitionManagements;
using MediatR;
using System.Linq.Expressions;
using Application.Repositories.DefinitionManagementRepo.ClassRepo;
using Application.Abstractions.Redis;

namespace Application.Features.DefinitionFeatures.Classes.Queries.GetList;

public class GetListClassQuery : IRequest<GetListResponse<GetListClassListItemDto>>
{
    public PageRequest PageRequest { get; set; }

    public class GetListClassQueryHandler : IRequestHandler<GetListClassQuery, GetListResponse<GetListClassListItemDto>>
    {
        private readonly IClassReadRepository _classReadRepository;
        private readonly IMapper _mapper;
        private readonly NoPagination<X.Class, GetListClassListItemDto> _noPagination;
        private readonly IRedisCacheService _redisCacheService;

        public GetListClassQueryHandler(IClassReadRepository classReadRepository, IMapper mapper, NoPagination<X.Class, GetListClassListItemDto> noPagination, IRedisCacheService redisCacheService)
        {
            _classReadRepository = classReadRepository;
            _mapper = mapper;
            _noPagination = noPagination;
            _redisCacheService = redisCacheService;
        }

        public async Task<GetListResponse<GetListClassListItemDto>> Handle(GetListClassQuery request, CancellationToken cancellationToken)
        {
            // 1) Cache key oluþtur
            string key = $"Classes_{request.PageRequest.PageIndex}_{request.PageRequest.PageSize}";

            // 2) Cache kontrolü
            var cached = await _redisCacheService.GetAsync<GetListResponse<GetListClassListItemDto>>(key);
            if (cached is not null)
                return cached;

            // 3) Asýl veri sorgusu
            GetListResponse<GetListClassListItemDto> response;
            if (request.PageRequest.PageIndex == -1)
            {
                response = await _noPagination.NoPaginationData(
                    cancellationToken,
                    orderBy: x => x.Name);
            }
            else
            {
                IPaginate<X.Class> classs = await _classReadRepository.GetListAsync(
                    index: request.PageRequest.PageIndex,
                    size: request.PageRequest.PageSize,
                    cancellationToken: cancellationToken,
                    orderBy: x => x.OrderBy(o => o.Name)
                );
                response = _mapper.Map<GetListResponse<GetListClassListItemDto>>(classs);
            }

            // 4) Sonuçlarý cache’e yaz
            await _redisCacheService.SetAsync(key, response, TimeSpan.FromMinutes(5));

            return response;
        }
    }
}
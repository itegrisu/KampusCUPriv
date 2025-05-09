using Application.Abstractions.Redis;
using Application.Features.ClubFeatures.Clubs.Queries.GetList;
using Application.Helpers.PaginationHelpers;
using Application.Repositories.ClubManagementRepos.ClubRepo;
using AutoMapper;
using Core.Application.Request;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities.ClubManagements;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using X = Domain.Entities.ClubManagements;

namespace Application.Features.ClubManagementFeatures.Clubs.Queries.GetByCount
{
    public class GetByCountListClubQuery : IRequest<GetListResponse<GetByCountListClubListItemDto>>
    {
        public int Count { get; set; }
        public class GetByCountListClubQueryHandler : IRequestHandler<GetByCountListClubQuery, GetListResponse<GetByCountListClubListItemDto>>
        {
            private readonly IClubReadRepository _clubReadRepository;
            private readonly IMapper _mapper;
            private readonly NoPagination<X.Club, GetByCountListClubListItemDto> _noPagination;
            private readonly IRedisCacheService _redisCacheService;
            public GetByCountListClubQueryHandler(IClubReadRepository clubReadRepository, IMapper mapper, NoPagination<X.Club, GetByCountListClubListItemDto> noPagination, IRedisCacheService redisCacheService)
            {
                _clubReadRepository = clubReadRepository;
                _mapper = mapper;
                _noPagination = noPagination;
                _redisCacheService = redisCacheService;
            }

            public async Task<GetListResponse<GetByCountListClubListItemDto>> Handle(GetByCountListClubQuery request, CancellationToken cancellationToken)
            {   
                // 1) Cache key oluştur
                string key = $"Clubs_Top_{request.Count}";

                // 2) Cache kontrolü
                var cached = await _redisCacheService.GetAsync<GetListResponse<GetByCountListClubListItemDto>>(key);
                if (cached is not null)
                    return cached;

                // 3) Asıl veri sorgusu
                IPaginate<X.Club> clubs = await _clubReadRepository.GetListAsync(
                    index: 0,
                    size: request.Count,
                    orderByDesc: x => x.OrderByDescending(e => e.CreatedDate),
                    cancellationToken: cancellationToken,
                    include: x => x.Include(x => x.UserFK).Include(x => x.CategoryFK)
                );
                var response = _mapper.Map<GetListResponse<GetByCountListClubListItemDto>>(clubs);

                // 4) Sonuçları cache’e yaz
                await _redisCacheService.SetAsync(key, response, TimeSpan.FromMinutes(5));

                return response;
            }
        }
    }
}

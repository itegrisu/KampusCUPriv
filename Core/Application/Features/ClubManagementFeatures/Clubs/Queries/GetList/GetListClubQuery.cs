using Application.Helpers.PaginationHelpers;
using AutoMapper;
using Core.Application.Request;
using Core.Application.Responses;
using Core.Persistence.Paging;
using X = Domain.Entities.ClubManagements;
using MediatR;
using System.Linq.Expressions;
using Application.Repositories.ClubManagementRepos.ClubRepo;
using Microsoft.EntityFrameworkCore;
using Domain.Entities.ClubManagements;
using Application.Repositories.ClubManagementRepos.StudentClubRepo;
using Application.Repositories.CommunicationManagementRepo.EventRepo;
using Application.Abstractions.Redis;

namespace Application.Features.ClubFeatures.Clubs.Queries.GetList;

public class GetListClubQuery : IRequest<GetListResponse<GetListClubListItemDto>>
{
    public PageRequest PageRequest { get; set; }

    public class GetListClubQueryHandler : IRequestHandler<GetListClubQuery, GetListResponse<GetListClubListItemDto>>
    {
        private readonly IClubReadRepository _clubReadRepository;
        private readonly IStudentClubReadRepository _studentClubReadRepository;
        private readonly IEventReadRepository _eventReadRepository;
        private readonly IMapper _mapper;
        private readonly NoPagination<X.Club, GetListClubListItemDto> _noPagination;
        private readonly IRedisCacheService _redisCacheService;
        public GetListClubQueryHandler(IClubReadRepository clubReadRepository, IMapper mapper, NoPagination<X.Club, GetListClubListItemDto> noPagination, IStudentClubReadRepository studentClubReadRepository, IEventReadRepository eventReadRepository, IRedisCacheService redisCacheService)
        {
            _clubReadRepository = clubReadRepository;
            _mapper = mapper;
            _noPagination = noPagination;
            _studentClubReadRepository = studentClubReadRepository;
            _eventReadRepository = eventReadRepository;
            _redisCacheService = redisCacheService;
        }

        public async Task<GetListResponse<GetListClubListItemDto>> Handle(GetListClubQuery request, CancellationToken cancellationToken)
        {
            // 1) Cache key oluþtur
            string key = $"Clubs_{request.PageRequest.PageIndex}_{request.PageRequest.PageSize}";

            // 2) Cache kontrolü
            var cached = await _redisCacheService.GetAsync<GetListResponse<GetListClubListItemDto>>(key);
            if (cached is not null)
                return cached;

            // 3) Asýl veri sorgusu
            GetListResponse<GetListClubListItemDto> response;
            if (request.PageRequest.PageIndex == -1)
            {
                response = await _noPagination.NoPaginationData(cancellationToken,
                    includes: new Expression<Func<Club, object>>[]
                    {
                    x => x.UserFK,
                    x => x.CategoryFK
                    });
            }
            else
            {
                IPaginate<X.Club> clubs = await _clubReadRepository.GetListAsync(
                    index: request.PageRequest.PageIndex,
                    size: request.PageRequest.PageSize,
                    cancellationToken: cancellationToken,
                    include: x => x.Include(x => x.UserFK).Include(x => x.CategoryFK)
                );
                response = _mapper.Map<GetListResponse<GetListClubListItemDto>>(clubs);
            }

            foreach (var item in response.Items)
            {
                var studentClubs = await _studentClubReadRepository
                    .GetListAsync(sc => sc.GidClubFK == item.Gid, cancellationToken: cancellationToken);
                item.MemberCount = studentClubs.Count;

                var events = await _eventReadRepository
                    .GetListAsync(e => e.GidClubFK == item.Gid, cancellationToken: cancellationToken);
                item.EventCount = events.Count;
            }

            // 4) Sonuçlarý cache’e yaz
            await _redisCacheService.SetAsync(key, response, TimeSpan.FromMinutes(5));

            return response;
        }
    }
}
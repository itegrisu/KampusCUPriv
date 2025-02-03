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

            public GetByCountListClubQueryHandler(IClubReadRepository clubReadRepository, IMapper mapper, NoPagination<X.Club, GetByCountListClubListItemDto> noPagination)
            {
                _clubReadRepository = clubReadRepository;
                _mapper = mapper;
                _noPagination = noPagination;
            }

            public async Task<GetListResponse<GetByCountListClubListItemDto>> Handle(GetByCountListClubQuery request, CancellationToken cancellationToken)
            {               
                IPaginate<X.Club> clubs = await _clubReadRepository.GetListAsync(
                    index: 0,
                    size: request.Count,
                    orderByDesc: x => x.OrderByDescending(e => e.CreatedDate),
                    cancellationToken: cancellationToken,
                    include: x => x.Include(x => x.UserFK).Include(x => x.CategoryFK)
                );

                GetListResponse<GetByCountListClubListItemDto> response = _mapper.Map<GetListResponse<GetByCountListClubListItemDto>>(clubs);
                return response;
            }
        }
    }
}

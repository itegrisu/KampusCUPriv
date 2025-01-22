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

namespace Application.Features.ClubFeatures.Clubs.Queries.GetList;

public class GetListClubQuery : IRequest<GetListResponse<GetListClubListItemDto>>
{
    public PageRequest PageRequest { get; set; }

    public class GetListClubQueryHandler : IRequestHandler<GetListClubQuery, GetListResponse<GetListClubListItemDto>>
    {
        private readonly IClubReadRepository _clubReadRepository;
        private readonly IMapper _mapper;
        private readonly NoPagination<X.Club, GetListClubListItemDto> _noPagination;

        public GetListClubQueryHandler(IClubReadRepository clubReadRepository, IMapper mapper, NoPagination<X.Club, GetListClubListItemDto> noPagination)
        {
            _clubReadRepository = clubReadRepository;
            _mapper = mapper;
            _noPagination = noPagination;
        }

        public async Task<GetListResponse<GetListClubListItemDto>> Handle(GetListClubQuery request, CancellationToken cancellationToken)
        {
            if (request.PageRequest.PageIndex == -1)
                return await _noPagination.NoPaginationData(cancellationToken,
                    includes: new Expression<Func<Club, object>>[]
                    {
                       x => x.UserFK,
                       x => x.CategoryFK
                    });
            IPaginate<X.Club> clubs = await _clubReadRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken,
                include: x => x.Include(x => x.UserFK).Include(x => x.CategoryFK)
            );

            GetListResponse<GetListClubListItemDto> response = _mapper.Map<GetListResponse<GetListClubListItemDto>>(clubs);
            return response;
        }
    }
}
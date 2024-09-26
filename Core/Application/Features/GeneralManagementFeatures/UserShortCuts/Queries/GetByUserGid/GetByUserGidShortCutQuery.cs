using Application.Helpers.PaginationHelpers;
using AutoMapper;
using Core.Application.Request;
using Core.Application.Responses;
using Core.Persistence.Paging;
using MediatR;
using System.Linq.Expressions;
using Application.Repositories.GeneralManagementRepos.UserShortCutRepo;
using Domain.Entities.GeneralManagements;

namespace Application.Features.GeneralManagementFeatures.UserShortCuts.Queries.GetByUserGid;

public class GetByUserGidShortCutQuery : IRequest<GetListResponse<GetByUserGidShortCutListItemDto>>
{
    public int PageIndex { get; set; } = 0;
    public int PageSize { get; set; } = 10;
    public Guid Gid { get; set; }
    public class GetByUserGidShortCutQueryHandler : IRequestHandler<GetByUserGidShortCutQuery, GetListResponse<GetByUserGidShortCutListItemDto>>
    {
        private readonly IUserShortCutReadRepository _userShortCutReadRepository;
        private readonly IMapper _mapper;
        private readonly NoPagination<UserShortCut, GetByUserGidShortCutListItemDto> _noPagination;

        public GetByUserGidShortCutQueryHandler(IUserShortCutReadRepository userShortCutReadRepository, IMapper mapper, NoPagination<UserShortCut, GetByUserGidShortCutListItemDto> noPagination)
        {
            _userShortCutReadRepository = userShortCutReadRepository;
            _mapper = mapper;
            _noPagination = noPagination;
        }

        public async Task<GetListResponse<GetByUserGidShortCutListItemDto>> Handle(GetByUserGidShortCutQuery request, CancellationToken cancellationToken)
        {
            if (request.PageIndex == -1)
            {
                return await _noPagination.NoPaginationData(cancellationToken,
                   predicate: x => x.GidUserFK == request.Gid,
                   includes: new Expression<Func<UserShortCut, object>>[]
                   {
                      x=>x.UserFK

                   },
                   orderBy: x => x.RowNo

                   );
            }
            IPaginate<UserShortCut> userShortCuts = await _userShortCutReadRepository.GetListAllAsync(
                index: request.PageIndex,
                size: request.PageSize,
                cancellationToken: cancellationToken,
                orderBy: x => x.OrderBy(x => x.RowNo)
            );

            GetListResponse<GetByUserGidShortCutListItemDto> response = _mapper.Map<GetListResponse<GetByUserGidShortCutListItemDto>>(userShortCuts);
            return response;
        }
    }
}
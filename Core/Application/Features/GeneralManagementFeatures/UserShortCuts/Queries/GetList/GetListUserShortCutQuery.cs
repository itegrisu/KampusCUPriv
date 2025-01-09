using Application.Helpers.PaginationHelpers;
using Application.Repositories.GeneralManagementRepos.UserShortCutRepo;
using AutoMapper;
using Core.Application.Request;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities.GeneralManagements;
using MediatR;
using System.Linq.Expressions;

namespace Application.Features.GeneralManagementFeatures.UserShortCuts.Queries.GetList;

public class GetListUserShortCutQuery : IRequest<GetListResponse<GetListUserShortCutListItemDto>>
{
    public PageRequest PageRequest { get; set; }

    public class GetListUserShortCutQueryHandler : IRequestHandler<GetListUserShortCutQuery, GetListResponse<GetListUserShortCutListItemDto>>
    {
        private readonly IUserShortCutReadRepository _userShortCutReadRepository;
        private readonly IMapper _mapper;
        private readonly NoPagination<UserShortCut, GetListUserShortCutListItemDto> _noPagination;

        public GetListUserShortCutQueryHandler(IUserShortCutReadRepository userShortCutReadRepository, IMapper mapper, NoPagination<UserShortCut, GetListUserShortCutListItemDto> noPagination)
        {
            _userShortCutReadRepository = userShortCutReadRepository;
            _mapper = mapper;
            _noPagination = noPagination;
        }

        public async Task<GetListResponse<GetListUserShortCutListItemDto>> Handle(GetListUserShortCutQuery request, CancellationToken cancellationToken)
        {
            if (request.PageRequest.PageIndex == -1)
            {
                return await _noPagination.NoPaginationData(cancellationToken,
                    orderBy: x => x.RowNo,
                   includes: new Expression<Func<UserShortCut, object>>[]
                   {
                      x=>x.UserFK

                   });
            }
            IPaginate<UserShortCut> userShortCuts = await _userShortCutReadRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                 orderBy: x => x.OrderBy(x => x.RowNo),
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListUserShortCutListItemDto> response = _mapper.Map<GetListResponse<GetListUserShortCutListItemDto>>(userShortCuts);
            return response;
        }
    }
}
using Application.Helpers.PaginationHelpers;
using AutoMapper;
using Core.Application.Request;
using Core.Application.Responses;
using Core.Persistence.Paging;
using MediatR;
using System.Linq.Expressions;
using Application.Repositories.GeneralManagementRepos.UserShortCutRepo;
using Domain.Entities.GeneralManagements;

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
                   includes: new Expression<Func<UserShortCut, object>>[]
                   {
                      x=>x.UserFK

                   });
            }
            IPaginate<UserShortCut> userShortCuts = await _userShortCutReadRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListUserShortCutListItemDto> response = _mapper.Map<GetListResponse<GetListUserShortCutListItemDto>>(userShortCuts);
            return response;
        }
    }
}

using AutoMapper;
using MediatR;
using Core.Application.Responses;
using Core.Application.Request;
using Core.Persistence.Paging;
using Domain.Entities.AuthManagements;
using Application.Helpers.PaginationHelpers;
using Application.Repositories.AuthManagementRepos.AuthPageRepo;

namespace Application.Features.AuthManagementFeatures.AuthPages.Queries.GetList;

public class GetListAuthPageQuery : IRequest<GetListResponse<GetListAuthPageListItemDto>>
{
    public PageRequest PageRequest { get; set; }

    public class GetListAuthPageQueryHandler : IRequestHandler<GetListAuthPageQuery, GetListResponse<GetListAuthPageListItemDto>>
    {
        private readonly IAuthPageReadRepository _authPageReadRepository;
        private readonly IMapper _mapper;
        private readonly NoPagination<AuthPage, GetListAuthPageListItemDto> _noPagination;
        public GetListAuthPageQueryHandler(IAuthPageReadRepository authPageReadRepository, IMapper mapper, NoPagination<AuthPage, GetListAuthPageListItemDto> noPagination)
        {
            _authPageReadRepository = authPageReadRepository;
            _mapper = mapper;
            _noPagination = noPagination;
        }

        public async Task<GetListResponse<GetListAuthPageListItemDto>> Handle(GetListAuthPageQuery request, CancellationToken cancellationToken)
        {
            if (request.PageRequest.PageIndex == -1)
                return await _noPagination.NoPaginationAllData(cancellationToken, predicate: x => x.DataState == Core.Enum.DataState.Active || x.DataState == Core.Enum.DataState.Deleted,
                orderBy: x => x.RowNo);


            IPaginate<AuthPage> authPages = await _authPageReadRepository.GetListAllAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                predicate: x => x.DataState == Core.Enum.DataState.Active || x.DataState == Core.Enum.DataState.Deleted,
                orderBy: o => o.OrderBy(o => o.RowNo),
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListAuthPageListItemDto> response = _mapper.Map<GetListResponse<GetListAuthPageListItemDto>>(authPages);
            return response;
        }
    }
}
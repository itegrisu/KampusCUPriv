using Application.Helpers.PaginationHelpers;
using Application.Repositories.FinanceManagementRepos.FinanceExpenseGroupRepo;
using AutoMapper;
using Core.Application.Request;
using Core.Application.Responses;
using Core.Persistence.Paging;
using MediatR;
using X = Domain.Entities.FinanceManagements;

namespace Application.Features.FinanceManagementFeatures.FinanceExpenseGroups.Queries.GetList;

public class GetListFinanceExpenseGroupQuery : IRequest<GetListResponse<GetListFinanceExpenseGroupListItemDto>>
{
    public PageRequest PageRequest { get; set; }

    public class GetListFinanceExpenseGroupQueryHandler : IRequestHandler<GetListFinanceExpenseGroupQuery, GetListResponse<GetListFinanceExpenseGroupListItemDto>>
    {
        private readonly IFinanceExpenseGroupReadRepository _financeExpenseGroupReadRepository;
        private readonly IMapper _mapper;
        private readonly NoPagination<X.FinanceExpenseGroup, GetListFinanceExpenseGroupListItemDto> _noPagination;

        public GetListFinanceExpenseGroupQueryHandler(IFinanceExpenseGroupReadRepository financeExpenseGroupReadRepository, IMapper mapper, NoPagination<X.FinanceExpenseGroup, GetListFinanceExpenseGroupListItemDto> noPagination)
        {
            _financeExpenseGroupReadRepository = financeExpenseGroupReadRepository;
            _mapper = mapper;
            _noPagination = noPagination;
        }

        public async Task<GetListResponse<GetListFinanceExpenseGroupListItemDto>> Handle(GetListFinanceExpenseGroupQuery request, CancellationToken cancellationToken)
        {
            if (request.PageRequest.PageIndex == -1)
                return await _noPagination.NoPaginationData(cancellationToken, orderBy: x => x.RowNo);

            IPaginate<X.FinanceExpenseGroup> financeExpenseGroups = await _financeExpenseGroupReadRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                orderBy: x => x.OrderBy(x => x.RowNo),
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListFinanceExpenseGroupListItemDto> response = _mapper.Map<GetListResponse<GetListFinanceExpenseGroupListItemDto>>(financeExpenseGroups);
            return response;
        }
    }
}
using Application.Helpers.PaginationHelpers;
using Application.Repositories.FinanceManagementRepos.FinanceExpenseGroupRepo;
using AutoMapper;
using Core.Application.Request;
using Core.Application.Responses;
using Core.Persistence.Paging;
using X = Domain.Entities.FinanceManagements;
using MediatR;
using System.Linq.Expressions;

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
                //unutma
				//includes varsa eklenecek - Orn: Altta
				//return await _noPagination.NoPaginationData(cancellationToken, 
                //    includes: new Expression<Func<FinanceExpenseGroup, object>>[]
                //    {
                //       x => x.UserFK,
                //       x=> x.FinanceExpenseGroupMembers
                //    });
				return await _noPagination.NoPaginationData(cancellationToken);
            IPaginate<X.FinanceExpenseGroup> financeExpenseGroups = await _financeExpenseGroupReadRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListFinanceExpenseGroupListItemDto> response = _mapper.Map<GetListResponse<GetListFinanceExpenseGroupListItemDto>>(financeExpenseGroups);
            return response;
        }
    }
}
using Application.Helpers.PaginationHelpers;
using Application.Repositories.FinanceManagementRepos.FinanceExpenseDetailRepo;
using AutoMapper;
using Core.Application.Request;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities.FinanceManagements;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using X = Domain.Entities.FinanceManagements;

namespace Application.Features.FinanceManagementFeatures.FinanceExpenseDetails.Queries.GetList;

public class GetListFinanceExpenseDetailQuery : IRequest<GetListResponse<GetListFinanceExpenseDetailListItemDto>>
{
    public PageRequest PageRequest { get; set; }

    public class GetListFinanceExpenseDetailQueryHandler : IRequestHandler<GetListFinanceExpenseDetailQuery, GetListResponse<GetListFinanceExpenseDetailListItemDto>>
    {
        private readonly IFinanceExpenseDetailReadRepository _financeExpenseDetailReadRepository;
        private readonly IMapper _mapper;
        private readonly NoPagination<X.FinanceExpenseDetail, GetListFinanceExpenseDetailListItemDto> _noPagination;

        public GetListFinanceExpenseDetailQueryHandler(IFinanceExpenseDetailReadRepository financeExpenseDetailReadRepository, IMapper mapper, NoPagination<X.FinanceExpenseDetail, GetListFinanceExpenseDetailListItemDto> noPagination)
        {
            _financeExpenseDetailReadRepository = financeExpenseDetailReadRepository;
            _mapper = mapper;
            _noPagination = noPagination;
        }

        public async Task<GetListResponse<GetListFinanceExpenseDetailListItemDto>> Handle(GetListFinanceExpenseDetailQuery request, CancellationToken cancellationToken)
        {
            if (request.PageRequest.PageIndex == -1)
            {
                return await _noPagination.NoPaginationData(cancellationToken,
                    includes: new Expression<Func<FinanceExpenseDetail, object>>[]
                    {
                       X => X.ControlPersonnelFK,
                       X => X.CurrencyFK,
                       X => X.FinanceExpenseFK,
                       X => X.SpendPersonnelFK
                    });
            }
            IPaginate<X.FinanceExpenseDetail> financeExpenseDetails = await _financeExpenseDetailReadRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken,
                include: x => x.Include(x => x.ControlPersonnelFK).Include(x => x.CurrencyFK).Include(x => x.FinanceExpenseFK).Include(x => x.SpendPersonnelFK)
            );

            GetListResponse<GetListFinanceExpenseDetailListItemDto> response = _mapper.Map<GetListResponse<GetListFinanceExpenseDetailListItemDto>>(financeExpenseDetails);
            return response;
        }
    }
}
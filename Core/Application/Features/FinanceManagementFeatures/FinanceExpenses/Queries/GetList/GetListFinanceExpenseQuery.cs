using Application.Features.FinanceManagementFeatures.FinanceExpenses.Queries.GetByUserGid;
using Application.Helpers.PaginationHelpers;
using Application.Repositories.FinanceManagementRepos.FinanceExpenseDetailRepo;
using Application.Repositories.FinanceManagementRepos.FinanceExpenseRepo;
using AutoMapper;
using Core.Application.Request;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities.FinanceManagements;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using X = Domain.Entities.FinanceManagements;

namespace Application.Features.FinanceManagementFeatures.FinanceExpenses.Queries.GetList;

public class GetListFinanceExpenseQuery : IRequest<GetListResponse<GetListFinanceExpenseListItemDto>>
{
    public PageRequest PageRequest { get; set; }

    public class GetListFinanceExpenseQueryHandler : IRequestHandler<GetListFinanceExpenseQuery, GetListResponse<GetListFinanceExpenseListItemDto>>
    {
        private readonly IFinanceExpenseReadRepository _financeExpenseReadRepository;
        private readonly IFinanceExpenseDetailReadRepository _financeExpenseDetailReadRepository;
        private readonly IMapper _mapper;
        private readonly NoPagination<X.FinanceExpense, GetListFinanceExpenseListItemDto> _noPagination;

        public GetListFinanceExpenseQueryHandler(IFinanceExpenseReadRepository financeExpenseReadRepository, IMapper mapper, NoPagination<X.FinanceExpense, GetListFinanceExpenseListItemDto> noPagination, IFinanceExpenseDetailReadRepository financeExpenseDetailReadRepository)
        {
            _financeExpenseReadRepository = financeExpenseReadRepository;
            _mapper = mapper;
            _noPagination = noPagination;
            _financeExpenseDetailReadRepository = financeExpenseDetailReadRepository;
        }

        public async Task<GetListResponse<GetListFinanceExpenseListItemDto>> Handle(GetListFinanceExpenseQuery request, CancellationToken cancellationToken)
        {

            if (request.PageRequest.PageIndex == -1)
            {
                return await _noPagination.NoPaginationData(cancellationToken,
                 includes: new Expression<Func<FinanceExpense, object>>[]
                 {
                       x=>x.FinanceExpenseGroupFK,
                       x=>x.CurrencyFK,
                       x=>x.MoneySenderPersonnelFK,
                       x=>x.ApprovalReceiverFK,
                       x=>x.OrganizationFK
                 });
            }

            IPaginate<X.FinanceExpense> financeExpenses = await _financeExpenseReadRepository.GetListAsync(
                   index: request.PageRequest.PageIndex,
                   size: request.PageRequest.PageSize,
                   cancellationToken: cancellationToken,
                   include: x => x.Include(x => x.FinanceExpenseGroupFK)
                                  .Include(x => x.CurrencyFK)
                                  .Include(x => x.MoneySenderPersonnelFK)
                                  .Include(x => x.ApprovalReceiverFK)
                                  .Include(x => x.OrganizationFK)
                                  .Include(x => x.FinanceExpenseDetails) // Finans detaylarýný dahil ediyoruz.
               );

            // DTO'ya mapleme iþlemi
            var financeExpenseDtoList = financeExpenses.Items.Select(expense =>
            {
                var dto = _mapper.Map<GetByUserGidListWithDateRangeFinanceExpenseListItemDto>(expense);
                dto.TotalFee = expense.FinanceExpenseDetails?.Sum(detail => detail.Fee) ?? 0;
                return dto;
            }).ToList();


            GetListResponse<GetListFinanceExpenseListItemDto> response = _mapper.Map<GetListResponse<GetListFinanceExpenseListItemDto>>(financeExpenses);
            return response;
        }
    }
}
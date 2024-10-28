using Application.Features.FinanceManagementFeatures.FinanceExpenses.Queries.GetList;
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using X = Domain.Entities.FinanceManagements;

namespace Application.Features.FinanceManagementFeatures.FinanceExpenses.Queries.GetByUserGid
{
    public class GetByUserGidWithDateRangeListFinanceExpenseQuery : IRequest<GetListResponse<GetByUserGidListWithDateRangeFinanceExpenseListItemDto>>
    {
        public int PageIndex { get; set; } = 0;
        public int PageSize { get; set; } = 10;
        public Guid? Gid { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public class GetByUserGidWithDateRangeListFinanceExpenseQueryHandler : IRequestHandler<GetByUserGidWithDateRangeListFinanceExpenseQuery, GetListResponse<GetByUserGidListWithDateRangeFinanceExpenseListItemDto>>
        {
            private readonly IFinanceExpenseReadRepository _financeExpenseReadRepository;
            private readonly IFinanceExpenseDetailReadRepository _financeExpenseDetailReadRepository;
            private readonly IMapper _mapper;
            private readonly NoPagination<X.FinanceExpense, GetByUserGidListWithDateRangeFinanceExpenseListItemDto> _noPagination;

            public GetByUserGidWithDateRangeListFinanceExpenseQueryHandler(IFinanceExpenseReadRepository financeExpenseReadRepository, IMapper mapper, NoPagination<X.FinanceExpense, GetByUserGidListWithDateRangeFinanceExpenseListItemDto> noPagination, IFinanceExpenseDetailReadRepository financeExpenseDetailReadRepository)
            {
                _financeExpenseReadRepository = financeExpenseReadRepository;
                _mapper = mapper;
                _noPagination = noPagination;
                _financeExpenseDetailReadRepository = financeExpenseDetailReadRepository;
            }

            public async Task<GetListResponse<GetByUserGidListWithDateRangeFinanceExpenseListItemDto>> Handle(GetByUserGidWithDateRangeListFinanceExpenseQuery request, CancellationToken cancellationToken)
            {
                if (request.Gid != null)
                {
                    IPaginate<X.FinanceExpense> financeExpenses = await _financeExpenseReadRepository.GetListAsync(
                        index: request.PageIndex,
                        size: request.PageSize,
                        cancellationToken: cancellationToken,
                        include: x => x.Include(x => x.FinanceExpenseGroupFK).Include(x => x.CurrencyFK).Include(x => x.MoneySenderPersonnelFK).Include(x => x.ApprovalReceiverFK).Include(x => x.OrganizationFK),
                        predicate: x => x.TransactionDate >= request.StartDate &&
                                       x.TransactionDate <= request.EndDate &&
                                       x.GidMoneyReceivePersonnelFK == request.Gid
                        );
                    GetListResponse<GetByUserGidListWithDateRangeFinanceExpenseListItemDto> response = _mapper.Map<GetListResponse<GetByUserGidListWithDateRangeFinanceExpenseListItemDto>>(financeExpenses);

                    foreach (var item in response.Items)
                    {
                        var financeExpenseDetails = await _financeExpenseReadRepository.GetListAsync(
                            predicate: x => x.GidExpenseGroupFK == item.GidExpenseGroupFK,
                            include: x => x.Include(x => x.FinanceExpenseDetails)
                        );

                        item.TotalFee = financeExpenseDetails.Items
                            .SelectMany(x => x.FinanceExpenseDetails)
                            .Sum(detail => detail.Fee);
                    }

                    return response;
                }
                else
                {
                    IPaginate<X.FinanceExpense> financeExpenses = await _financeExpenseReadRepository.GetListAsync(
                        index: request.PageIndex,
                        size: request.PageSize,
                        cancellationToken: cancellationToken,
                        include: x => x.Include(x => x.FinanceExpenseGroupFK).Include(x => x.CurrencyFK).Include(x => x.MoneySenderPersonnelFK).Include(x => x.ApprovalReceiverFK).Include(x => x.OrganizationFK),
                        predicate: x => x.TransactionDate >= request.StartDate &&
                                       x.TransactionDate <= request.EndDate

                        );
                    GetListResponse<GetByUserGidListWithDateRangeFinanceExpenseListItemDto> response = _mapper.Map<GetListResponse<GetByUserGidListWithDateRangeFinanceExpenseListItemDto>>(financeExpenses);


                    foreach (var item in response.Items)
                    {
                        var financeExpenseDetails = await _financeExpenseDetailReadRepository.GetListAsync(
                            predicate: x => x.GidExpenseFK == item.Gid && x.ApprovalStatus == Domain.Enums.EnumApprovalStatus.KabulEdildi,
                            include: x => x.Include(x => x.FinanceExpenseFK)
                        );

                        item.TotalFee = financeExpenseDetails.Items.Sum(detail => detail.Fee);
                    }

                    return response;
                }
            }
        }
    }
}

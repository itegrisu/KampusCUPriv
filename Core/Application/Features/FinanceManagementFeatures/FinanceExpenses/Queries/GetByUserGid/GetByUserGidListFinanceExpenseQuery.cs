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
    public class GetByUserGidListFinanceExpenseQuery : IRequest<GetListResponse<GetByUserGidListFinanceExpenseListItemDto>>
    {

        public int PageIndex { get; set; } = 0;
        public int PageSize { get; set; } = 10;
        public Guid Gid { get; set; }
        public class GetByUserGidListFinanceExpenseQueryHandler : IRequestHandler<GetByUserGidListFinanceExpenseQuery, GetListResponse<GetByUserGidListFinanceExpenseListItemDto>>
        {
            private readonly IFinanceExpenseReadRepository _financeExpenseReadRepository;
            private readonly IFinanceExpenseDetailReadRepository _financeExpenseDetailReadRepository;
            private readonly IMapper _mapper;
            private readonly NoPagination<X.FinanceExpense, GetByUserGidListFinanceExpenseListItemDto> _noPagination;

            public GetByUserGidListFinanceExpenseQueryHandler(IFinanceExpenseReadRepository financeExpenseReadRepository, IMapper mapper, NoPagination<X.FinanceExpense, GetByUserGidListFinanceExpenseListItemDto> noPagination, IFinanceExpenseDetailReadRepository financeExpenseDetailReadRepository)
            {
                _financeExpenseReadRepository = financeExpenseReadRepository;
                _mapper = mapper;
                _noPagination = noPagination;
                _financeExpenseDetailReadRepository = financeExpenseDetailReadRepository;
            }

            public async Task<GetListResponse<GetByUserGidListFinanceExpenseListItemDto>> Handle(GetByUserGidListFinanceExpenseQuery request, CancellationToken cancellationToken)
            {
                IPaginate<X.FinanceExpense> financeExpenses = await _financeExpenseReadRepository.GetListAsync(
                    index: request.PageIndex,
                    size: request.PageSize,
                    cancellationToken: cancellationToken,
                    predicate: x => x.GidMoneyReceivePersonnelFK == request.Gid,
                    include: x => x.Include(x => x.FinanceExpenseGroupFK)
                                  .Include(x => x.CurrencyFK)
                                  .Include(x => x.MoneySenderPersonnelFK)
                                  .Include(x => x.ApprovalReceiverFK)
                                  .Include(x => x.OrganizationFK)
                                  .Include(x => x.FinanceExpenseDetails)
                );

                GetListResponse<GetByUserGidListFinanceExpenseListItemDto> response = _mapper.Map<GetListResponse<GetByUserGidListFinanceExpenseListItemDto>>(financeExpenses);

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

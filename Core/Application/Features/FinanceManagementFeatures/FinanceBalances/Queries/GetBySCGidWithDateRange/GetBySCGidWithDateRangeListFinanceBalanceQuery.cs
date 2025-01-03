using Application.Features.FinanceManagementFeatures.FinanceBalances.Queries.GetList;
using Application.Helpers.PaginationHelpers;
using Application.Repositories.FinanceManagementRepos.FinanceBalanceRepo;
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

namespace Application.Features.FinanceManagementFeatures.FinanceBalances.Queries.GetBySCGidWithDateRange
{
    public class GetBySCGidWithDateRangeListFinanceBalanceQuery : IRequest<GetListResponse<GetBySCGidWithDateRangeListFinanceBalanceListItemDto>>
    {
        public int PageIndex { get; set; } = 0;
        public int PageSize { get; set; } = 10;
        public Guid? Gid { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public class GetBySCGidWithDateRangeListFinanceBalanceQueryHandler : IRequestHandler<GetBySCGidWithDateRangeListFinanceBalanceQuery, GetListResponse<GetBySCGidWithDateRangeListFinanceBalanceListItemDto>>
        {
            private readonly IFinanceBalanceReadRepository _financeBalanceReadRepository;
            private readonly IMapper _mapper;
            private readonly NoPagination<X.FinanceBalance, GetBySCGidWithDateRangeListFinanceBalanceListItemDto> _noPagination;

            public GetBySCGidWithDateRangeListFinanceBalanceQueryHandler(IFinanceBalanceReadRepository financeBalanceReadRepository, IMapper mapper, NoPagination<X.FinanceBalance, GetBySCGidWithDateRangeListFinanceBalanceListItemDto> noPagination)
            {
                _financeBalanceReadRepository = financeBalanceReadRepository;
                _mapper = mapper;
                _noPagination = noPagination;
            }

            public async Task<GetListResponse<GetBySCGidWithDateRangeListFinanceBalanceListItemDto>> Handle(GetBySCGidWithDateRangeListFinanceBalanceQuery request, CancellationToken cancellationToken)
            {
                if (request.Gid != null)
                    return await _noPagination.NoPaginationData(cancellationToken,
                         predicate: x => x.PaymentDate >= request.StartDate &&
                                       x.ExpirationDate <= request.EndDate &&
                                       x.GidSupplierCustomerFK == request.Gid,
                        includes: new Expression<Func<FinanceBalance, object>>[]
                        {
                       x => x.CurrencyFK,
                       x => x.SCCompanyFK,
                       x => x.TransportationExternalServiceFK,
                       x => x.TransportationFK,
                       x => x.VehicleTransactionFK,
                       x => x.VehicleTransactionFK.VehicleAllFK
                        });

                IPaginate<X.FinanceBalance> financeBalances = await _financeBalanceReadRepository.GetListAsync(
                    index: request.PageIndex,
                    size: request.PageSize,
                    cancellationToken: cancellationToken,
                     predicate: x => x.PaymentDate >= request.StartDate &&
                                       x.ExpirationDate <= request.EndDate,                                      
                     include: x => x.Include(x => x.CurrencyFK).Include(x => x.SCCompanyFK).Include(x => x.TransportationExternalServiceFK).Include(x => x.TransportationFK).Include(x => x.VehicleTransactionFK).Include(x => x.VehicleTransactionFK).ThenInclude(x => x.VehicleAllFK)
                );

                GetListResponse<GetBySCGidWithDateRangeListFinanceBalanceListItemDto> response = _mapper.Map<GetListResponse<GetBySCGidWithDateRangeListFinanceBalanceListItemDto>>(financeBalances);
                return response;
            }
        }
    }
}

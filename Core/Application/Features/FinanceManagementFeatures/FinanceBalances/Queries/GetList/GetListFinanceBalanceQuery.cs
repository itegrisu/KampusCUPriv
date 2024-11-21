using Application.Helpers.PaginationHelpers;
using Application.Repositories.FinanceManagementRepos.FinanceBalanceRepo;
using AutoMapper;
using Core.Application.Request;
using Core.Application.Responses;
using Core.Persistence.Paging;
using X = Domain.Entities.FinanceManagements;
using MediatR;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Domain.Entities.FinanceManagements;

namespace Application.Features.FinanceManagementFeatures.FinanceBalances.Queries.GetList;

public class GetListFinanceBalanceQuery : IRequest<GetListResponse<GetListFinanceBalanceListItemDto>>
{
    public PageRequest PageRequest { get; set; }

    public class GetListFinanceBalanceQueryHandler : IRequestHandler<GetListFinanceBalanceQuery, GetListResponse<GetListFinanceBalanceListItemDto>>
    {
        private readonly IFinanceBalanceReadRepository _financeBalanceReadRepository;
        private readonly IMapper _mapper;
        private readonly NoPagination<X.FinanceBalance, GetListFinanceBalanceListItemDto> _noPagination;

        public GetListFinanceBalanceQueryHandler(IFinanceBalanceReadRepository financeBalanceReadRepository, IMapper mapper, NoPagination<X.FinanceBalance, GetListFinanceBalanceListItemDto> noPagination)
        {
            _financeBalanceReadRepository = financeBalanceReadRepository;
            _mapper = mapper;
            _noPagination = noPagination;
        }

        public async Task<GetListResponse<GetListFinanceBalanceListItemDto>> Handle(GetListFinanceBalanceQuery request, CancellationToken cancellationToken)
        {
            if (request.PageRequest.PageIndex == -1)
                return await _noPagination.NoPaginationData(cancellationToken,
                    includes: new Expression<Func<FinanceBalance, object>>[]
                    {
                       x => x.CurrencyFK,
                       x => x.SCCompanyFK,
                       x => x.TransportationExternalServiceFK,
                       x => x.TransportationFK,
                       x => x.VehicleTransactionFK,
                       x => x.VehicleTransactionFK.VehicleAllFK
                    });
            //return await _noPagination.NoPaginationData(cancellationToken);
            IPaginate<X.FinanceBalance> financeBalances = await _financeBalanceReadRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken,
                 include: x => x.Include(x => x.CurrencyFK).Include(x => x.SCCompanyFK).Include(x => x.TransportationExternalServiceFK).Include(x => x.TransportationFK).Include(x => x.VehicleTransactionFK).Include(x => x.VehicleTransactionFK).ThenInclude(x => x.VehicleAllFK)
            );

            GetListResponse<GetListFinanceBalanceListItemDto> response = _mapper.Map<GetListResponse<GetListFinanceBalanceListItemDto>>(financeBalances);
            return response;
        }
    }
}
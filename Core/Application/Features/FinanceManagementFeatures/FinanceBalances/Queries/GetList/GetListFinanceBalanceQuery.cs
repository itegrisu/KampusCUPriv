using Application.Helpers.PaginationHelpers;
using Application.Repositories.FinanceManagementRepos.FinanceBalanceRepo;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities.FinanceManagements;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using X = Domain.Entities.FinanceManagements;

namespace Application.Features.FinanceManagementFeatures.FinanceBalances.Queries.GetList;

public class GetListFinanceBalanceQuery : IRequest<GetListResponse<GetListFinanceBalanceListItemDto>>
{
    public Guid BalanceResourceTypeGid { get; set; } //VehicleTransactionFK veya TransportationFK veya TransportationExternalServiceFK. Farkýný balanceResourceType'dan anlayabiliriz.
    public EnumBalanceResourceType BalanceResourceType { get; set; }
    public int PageIndex { get; set; } = 0;
    public int PageSize { get; set; } = 10;

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
            Expression<Func<FinanceBalance, bool>> predicate = null;
            if (request.BalanceResourceType == EnumBalanceResourceType.Araclar)
                predicate = x => x.GidVehicleTransactionFK == request.BalanceResourceTypeGid;
            else if (request.BalanceResourceType == EnumBalanceResourceType.Ulasim)
                predicate = x => x.GidTransportationFK == request.BalanceResourceTypeGid;
            else if (request.BalanceResourceType == EnumBalanceResourceType.DisUlasim)
                predicate = x => x.GidTransportationExternalServiceFK == request.BalanceResourceTypeGid;

            if (request.PageIndex == -1)
                return await _noPagination.NoPaginationData(cancellationToken,
                    predicate: predicate,
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
                index: request.PageIndex,
                size: request.PageSize,
                predicate: predicate,
                cancellationToken: cancellationToken,
                 include: x => x.Include(x => x.CurrencyFK).Include(x => x.SCCompanyFK).Include(x => x.TransportationExternalServiceFK).Include(x => x.TransportationFK).Include(x => x.VehicleTransactionFK).Include(x => x.VehicleTransactionFK).ThenInclude(x => x.VehicleAllFK)
            );

            GetListResponse<GetListFinanceBalanceListItemDto> response = _mapper.Map<GetListResponse<GetListFinanceBalanceListItemDto>>(financeBalances);
            return response;
        }
    }
}
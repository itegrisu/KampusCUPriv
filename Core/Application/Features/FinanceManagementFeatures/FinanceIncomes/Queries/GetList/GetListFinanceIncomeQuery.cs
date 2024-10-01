using Application.Helpers.PaginationHelpers;
using Application.Repositories.FinanceManagementRepos.FinanceIncomeRepo;
using AutoMapper;
using Core.Application.Request;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities.FinanceManagements;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using X = Domain.Entities.FinanceManagements;

namespace Application.Features.FinanceManagementFeatures.FinanceIncomes.Queries.GetList;

public class GetListFinanceIncomeQuery : IRequest<GetListResponse<GetListFinanceIncomeListItemDto>>
{
    public PageRequest PageRequest { get; set; }

    public class GetListFinanceIncomeQueryHandler : IRequestHandler<GetListFinanceIncomeQuery, GetListResponse<GetListFinanceIncomeListItemDto>>
    {
        private readonly IFinanceIncomeReadRepository _financeIncomeReadRepository;
        private readonly IMapper _mapper;
        private readonly NoPagination<X.FinanceIncome, GetListFinanceIncomeListItemDto> _noPagination;

        public GetListFinanceIncomeQueryHandler(IFinanceIncomeReadRepository financeIncomeReadRepository, IMapper mapper, NoPagination<X.FinanceIncome, GetListFinanceIncomeListItemDto> noPagination)
        {
            _financeIncomeReadRepository = financeIncomeReadRepository;
            _mapper = mapper;
            _noPagination = noPagination;
        }

        public async Task<GetListResponse<GetListFinanceIncomeListItemDto>> Handle(GetListFinanceIncomeQuery request, CancellationToken cancellationToken)
        {
            if (request.PageRequest.PageIndex == -1)
            {
                return await _noPagination.NoPaginationData(cancellationToken,
                    includes: new Expression<Func<FinanceIncome, object>>[]
                    {
                       x=>x.FinanceIncomeGroupFK,
                       x=>x.CurrencyFK
                    });
            }

            IPaginate<X.FinanceIncome> financeIncomes = await _financeIncomeReadRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken,
                 include: x => x.Include(x => x.FinanceIncomeGroupFK).Include(x => x.CurrencyFK)
            );

            GetListResponse<GetListFinanceIncomeListItemDto> response = _mapper.Map<GetListResponse<GetListFinanceIncomeListItemDto>>(financeIncomes);
            return response;
        }
    }
}
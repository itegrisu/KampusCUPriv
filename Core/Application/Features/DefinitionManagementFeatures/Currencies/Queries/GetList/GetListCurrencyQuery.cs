using Application.Helpers.PaginationHelpers;
using Application.Repositories.DefinitionManagementRepos.CurrencyRepo;
using AutoMapper;
using Core.Application.Request;
using Core.Application.Responses;
using Core.Persistence.Paging;
using MediatR;
using X = Domain.Entities.DefinitionManagements;

namespace Application.Features.DefinitionManagementFeatures.Currencies.Queries.GetList;

public class GetListCurrencyQuery : IRequest<GetListResponse<GetListCurrencyListItemDto>>
{
    public PageRequest PageRequest { get; set; }

    public class GetListCurrencyQueryHandler : IRequestHandler<GetListCurrencyQuery, GetListResponse<GetListCurrencyListItemDto>>
    {
        private readonly ICurrencyReadRepository _currencyReadRepository;
        private readonly IMapper _mapper;
        private readonly NoPagination<X.Currency, GetListCurrencyListItemDto> _noPagination;

        public GetListCurrencyQueryHandler(ICurrencyReadRepository currencyReadRepository, IMapper mapper, NoPagination<X.Currency, GetListCurrencyListItemDto> noPagination)
        {
            _currencyReadRepository = currencyReadRepository;
            _mapper = mapper;
            _noPagination = noPagination;
        }

        public async Task<GetListResponse<GetListCurrencyListItemDto>> Handle(GetListCurrencyQuery request, CancellationToken cancellationToken)
        {
            if (request.PageRequest.PageIndex == -1)
                return await _noPagination.NoPaginationData(cancellationToken);


            IPaginate<X.Currency> currencys = await _currencyReadRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListCurrencyListItemDto> response = _mapper.Map<GetListResponse<GetListCurrencyListItemDto>>(currencys);
            return response;
        }
    }
}
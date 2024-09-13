using Application.Features.DefinitionManagementFeatures.Currencies.Rules;
using Application.Repositories.DefinitionManagementRepos.CurrencyRepo;
using AutoMapper;
using MediatR;
using X = Domain.Entities.DefinitionManagements;

namespace Application.Features.DefinitionManagementFeatures.Currencies.Queries.GetByGid
{
    public class GetByGidCurrencyQuery : IRequest<GetByGidCurrencyResponse>
    {
        public Guid Gid { get; set; }

        public class GetByGidCurrencyQueryHandler : IRequestHandler<GetByGidCurrencyQuery, GetByGidCurrencyResponse>
        {
            private readonly IMapper _mapper;
            private readonly ICurrencyReadRepository _currencyReadRepository;
            private readonly CurrencyBusinessRules _currencyBusinessRules;

            public GetByGidCurrencyQueryHandler(IMapper mapper, ICurrencyReadRepository currencyReadRepository, CurrencyBusinessRules currencyBusinessRules)
            {
                _mapper = mapper;
                _currencyReadRepository = currencyReadRepository;
                _currencyBusinessRules = currencyBusinessRules;
            }

            public async Task<GetByGidCurrencyResponse> Handle(GetByGidCurrencyQuery request, CancellationToken cancellationToken)
            {
                X.Currency? currency = await _currencyReadRepository.GetAsync(predicate: uc => uc.Gid == request.Gid, cancellationToken: cancellationToken);

                await _currencyBusinessRules.CurrencyShouldExistWhenSelected(currency);

                GetByGidCurrencyResponse response = _mapper.Map<GetByGidCurrencyResponse>(currency);
                return response;
            }
        }
    }
}
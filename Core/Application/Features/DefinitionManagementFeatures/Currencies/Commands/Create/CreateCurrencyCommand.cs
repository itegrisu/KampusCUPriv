using Application.Features.DefinitionManagementFeatures.Currencies.Constants;
using Application.Features.DefinitionManagementFeatures.Currencies.Queries.GetByGid;
using Application.Features.DefinitionManagementFeatures.Currencies.Rules;
using Application.Repositories.DefinitionManagementRepos.CurrencyRepo;
using AutoMapper;
using MediatR;
using X = Domain.Entities.DefinitionManagements;

namespace Application.Features.DefinitionManagementFeatures.Currencies.Commands.Create;

public class CreateCurrencyCommand : IRequest<CreatedCurrencyResponse>
{

    public string Name { get; set; }
    public string? Code { get; set; }
    public string? Symbol { get; set; }

    public class CreateCurrencyCommandHandler : IRequestHandler<CreateCurrencyCommand, CreatedCurrencyResponse>
    {
        private readonly IMapper _mapper;
        private readonly ICurrencyWriteRepository _currencyWriteRepository;
        private readonly ICurrencyReadRepository _currencyReadRepository;
        private readonly CurrencyBusinessRules _currencyBusinessRules;

        public CreateCurrencyCommandHandler(IMapper mapper, ICurrencyWriteRepository currencyWriteRepository,
                                         CurrencyBusinessRules currencyBusinessRules, ICurrencyReadRepository currencyReadRepository)
        {
            _mapper = mapper;
            _currencyWriteRepository = currencyWriteRepository;
            _currencyBusinessRules = currencyBusinessRules;
            _currencyReadRepository = currencyReadRepository;
        }

        public async Task<CreatedCurrencyResponse> Handle(CreateCurrencyCommand request, CancellationToken cancellationToken)
        {
            await _currencyBusinessRules.CheckCurrencyNameIsUnique(request.Name);
            await _currencyBusinessRules.CheckCurrencyCodeIsUnique(request.Code);
            await _currencyBusinessRules.CheckCurrencySymbolIsUnique(request.Symbol);

            X.Currency currency = _mapper.Map<X.Currency>(request);


            await _currencyWriteRepository.AddAsync(currency);
            await _currencyWriteRepository.SaveAsync();

            X.Currency savedCurrency = await _currencyReadRepository.GetAsync(predicate: x => x.Gid == currency.Gid);
            //INCLUDES Buraya Gelecek include varsa eklenecek
            //include: x => x.Include(x => x.UserFK));

            GetByGidCurrencyResponse obj = _mapper.Map<GetByGidCurrencyResponse>(savedCurrency);
            return new()
            {
                Title = CurrenciesBusinessMessages.ProcessCompleted,
                Message = CurrenciesBusinessMessages.SuccessCreatedCurrencyMessage,
                IsValid = true,
                Obj = obj
            };
        }
    }
}
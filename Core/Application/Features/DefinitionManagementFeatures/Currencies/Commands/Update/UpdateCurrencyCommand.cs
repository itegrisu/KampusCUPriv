using Application.Features.DefinitionManagementFeatures.Currencies.Constants;
using Application.Features.DefinitionManagementFeatures.Currencies.Queries.GetByGid;
using Application.Features.DefinitionManagementFeatures.Currencies.Rules;
using Application.Repositories.DefinitionManagementRepos.CurrencyRepo;
using AutoMapper;
using MediatR;
using X = Domain.Entities.DefinitionManagements;

namespace Application.Features.DefinitionManagementFeatures.Currencies.Commands.Update;

public class UpdateCurrencyCommand : IRequest<UpdatedCurrencyResponse>
{
    public Guid Gid { get; set; }
    public string DovizAdi { get; set; }
    public string? DovizKodu { get; set; }
    public string? DovizSimgesi { get; set; }


    public class UpdateCurrencyCommandHandler : IRequestHandler<UpdateCurrencyCommand, UpdatedCurrencyResponse>
    {
        private readonly IMapper _mapper;
        private readonly ICurrencyWriteRepository _currencyWriteRepository;
        private readonly ICurrencyReadRepository _currencyReadRepository;
        private readonly CurrencyBusinessRules _currencyBusinessRules;

        public UpdateCurrencyCommandHandler(IMapper mapper, ICurrencyWriteRepository currencyWriteRepository,
                                         CurrencyBusinessRules currencyBusinessRules, ICurrencyReadRepository currencyReadRepository)
        {
            _mapper = mapper;
            _currencyWriteRepository = currencyWriteRepository;
            _currencyBusinessRules = currencyBusinessRules;
            _currencyReadRepository = currencyReadRepository;
        }

        public async Task<UpdatedCurrencyResponse> Handle(UpdateCurrencyCommand request, CancellationToken cancellationToken)
        {
            X.Currency? currency = await _currencyReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken);
            //INCLUDES Buraya Gelecek include varsa eklenecek
            await _currencyBusinessRules.CurrencyShouldExistWhenSelected(currency);
            await _currencyBusinessRules.CheckCurrencyNameIsUnique(request.DovizAdi, request.Gid);
            await _currencyBusinessRules.CheckCurrencyCodeIsUnique(request.DovizKodu, request.Gid);
            await _currencyBusinessRules.CheckCurrencySymbolIsUnique(request.DovizSimgesi, request.Gid);
            currency = _mapper.Map(request, currency);

            _currencyWriteRepository.Update(currency!);
            await _currencyWriteRepository.SaveAsync();

            X.Currency updatedSaved = await _currencyReadRepository.GetAsync(predicate: x => x.Gid == currency.Gid);

            GetByGidCurrencyResponse obj = _mapper.Map<GetByGidCurrencyResponse>(updatedSaved);

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
using Application.Features.DefinitionManagementFeatures.Currencies.Constants;
using Application.Features.DefinitionManagementFeatures.Currencies.Rules;
using Application.Repositories.DefinitionManagementRepos.CurrencyRepo;
using AutoMapper;
using X = Domain.Entities.DefinitionManagements;
using MediatR;

namespace Application.Features.DefinitionManagementFeatures.Currencies.Commands.Delete;

public class DeleteCurrencyCommand : IRequest<DeletedCurrencyResponse>
{
	public Guid Gid { get; set; }

    public class DeleteCurrencyCommandHandler : IRequestHandler<DeleteCurrencyCommand, DeletedCurrencyResponse>
    {
        private readonly IMapper _mapper;
        private readonly ICurrencyReadRepository _currencyReadRepository;
        private readonly ICurrencyWriteRepository _currencyWriteRepository;
        private readonly CurrencyBusinessRules _currencyBusinessRules;

        public DeleteCurrencyCommandHandler(IMapper mapper, ICurrencyReadRepository currencyReadRepository,
                                         CurrencyBusinessRules currencyBusinessRules, ICurrencyWriteRepository currencyWriteRepository)
        {
            _mapper = mapper;
            _currencyReadRepository = currencyReadRepository;
            _currencyBusinessRules = currencyBusinessRules;
            _currencyWriteRepository = currencyWriteRepository;
        }

        public async Task<DeletedCurrencyResponse> Handle(DeleteCurrencyCommand request, CancellationToken cancellationToken)
        {
            X.Currency? currency = await _currencyReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken);
            await _currencyBusinessRules.CurrencyShouldExistWhenSelected(currency);
            currency.DataState = Core.Enum.DataState.Deleted;

            _currencyWriteRepository.Update(currency);
            await _currencyWriteRepository.SaveAsync();

            return new()
            {
                Title = CurrenciesBusinessMessages.ProcessCompleted,
                Message = CurrenciesBusinessMessages.SuccessDeletedCurrencyMessage,
                IsValid = true
            };
        }
    }
}
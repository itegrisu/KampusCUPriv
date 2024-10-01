using Application.Features.FinanceManagementFeatures.FinanceIncomes.Constants;
using Application.Features.FinanceManagementFeatures.FinanceIncomes.Rules;
using Application.Repositories.FinanceManagementRepos.FinanceIncomeRepo;
using AutoMapper;
using X = Domain.Entities.FinanceManagements;
using MediatR;

namespace Application.Features.FinanceManagementFeatures.FinanceIncomes.Commands.Delete;

public class DeleteFinanceIncomeCommand : IRequest<DeletedFinanceIncomeResponse>
{
	public Guid Gid { get; set; }

    public class DeleteFinanceIncomeCommandHandler : IRequestHandler<DeleteFinanceIncomeCommand, DeletedFinanceIncomeResponse>
    {
        private readonly IMapper _mapper;
        private readonly IFinanceIncomeReadRepository _financeIncomeReadRepository;
        private readonly IFinanceIncomeWriteRepository _financeIncomeWriteRepository;
        private readonly FinanceIncomeBusinessRules _financeIncomeBusinessRules;

        public DeleteFinanceIncomeCommandHandler(IMapper mapper, IFinanceIncomeReadRepository financeIncomeReadRepository,
                                         FinanceIncomeBusinessRules financeIncomeBusinessRules, IFinanceIncomeWriteRepository financeIncomeWriteRepository)
        {
            _mapper = mapper;
            _financeIncomeReadRepository = financeIncomeReadRepository;
            _financeIncomeBusinessRules = financeIncomeBusinessRules;
            _financeIncomeWriteRepository = financeIncomeWriteRepository;
        }

        public async Task<DeletedFinanceIncomeResponse> Handle(DeleteFinanceIncomeCommand request, CancellationToken cancellationToken)
        {
            X.FinanceIncome? financeIncome = await _financeIncomeReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken);
            await _financeIncomeBusinessRules.FinanceIncomeShouldExistWhenSelected(financeIncome);
            financeIncome.DataState = Core.Enum.DataState.Deleted;

            _financeIncomeWriteRepository.Update(financeIncome);
            await _financeIncomeWriteRepository.SaveAsync();

            return new()
            {
                Title = FinanceIncomesBusinessMessages.ProcessCompleted,
                Message = FinanceIncomesBusinessMessages.SuccessDeletedFinanceIncomeMessage,
                IsValid = true
            };
        }
    }
}
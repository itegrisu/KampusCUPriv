using Application.Features.FinanceManagementFeatures.FinanceExpenses.Constants;
using Application.Features.FinanceManagementFeatures.FinanceExpenses.Rules;
using Application.Repositories.FinanceManagementRepos.FinanceExpenseRepo;
using AutoMapper;
using X = Domain.Entities.FinanceManagements;
using MediatR;

namespace Application.Features.FinanceManagementFeatures.FinanceExpenses.Commands.Delete;

public class DeleteFinanceExpenseCommand : IRequest<DeletedFinanceExpenseResponse>
{
	public Guid Gid { get; set; }

    public class DeleteFinanceExpenseCommandHandler : IRequestHandler<DeleteFinanceExpenseCommand, DeletedFinanceExpenseResponse>
    {
        private readonly IMapper _mapper;
        private readonly IFinanceExpenseReadRepository _financeExpenseReadRepository;
        private readonly IFinanceExpenseWriteRepository _financeExpenseWriteRepository;
        private readonly FinanceExpenseBusinessRules _financeExpenseBusinessRules;

        public DeleteFinanceExpenseCommandHandler(IMapper mapper, IFinanceExpenseReadRepository financeExpenseReadRepository,
                                         FinanceExpenseBusinessRules financeExpenseBusinessRules, IFinanceExpenseWriteRepository financeExpenseWriteRepository)
        {
            _mapper = mapper;
            _financeExpenseReadRepository = financeExpenseReadRepository;
            _financeExpenseBusinessRules = financeExpenseBusinessRules;
            _financeExpenseWriteRepository = financeExpenseWriteRepository;
        }

        public async Task<DeletedFinanceExpenseResponse> Handle(DeleteFinanceExpenseCommand request, CancellationToken cancellationToken)
        {
            X.FinanceExpense? financeExpense = await _financeExpenseReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken);
            await _financeExpenseBusinessRules.FinanceExpenseShouldExistWhenSelected(financeExpense);
            financeExpense.DataState = Core.Enum.DataState.Deleted;

            _financeExpenseWriteRepository.Update(financeExpense);
            await _financeExpenseWriteRepository.SaveAsync();

            return new()
            {
                Title = FinanceExpensesBusinessMessages.ProcessCompleted,
                Message = FinanceExpensesBusinessMessages.SuccessDeletedFinanceExpenseMessage,
                IsValid = true
            };
        }
    }
}
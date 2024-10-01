using Application.Features.FinanceManagementFeatures.FinanceExpenses.Constants;
using Application.Features.FinanceManagementFeatures.FinanceExpenses.Queries.GetByGid;
using Application.Features.FinanceManagementFeatures.FinanceExpenses.Rules;
using Application.Repositories.FinanceManagementRepos.FinanceExpenseRepo;
using AutoMapper;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using X = Domain.Entities.FinanceManagements;

namespace Application.Features.FinanceManagementFeatures.FinanceExpenses.Commands.Create;

public class CreateFinanceExpenseCommand : IRequest<CreatedFinanceExpenseResponse>
{
    public Guid GidExpenseGroupFK { get; set; }
    public string? GidOrganizationFK { get; set; }
    public Guid GidMoneySenderPersonnelFK { get; set; }
    public Guid GidMoneyReceivePersonnelFK { get; set; }
    public Guid GidCurrencyFK { get; set; }
    public string? GidApprovalReceiverFK { get; set; }
    public string Title { get; set; }
    public decimal AmountSpent { get; set; }
    public DateTime TransactionDate { get; set; }
    public EnumExpenseStatus ExpenseStatus { get; set; }
    public string? Document { get; set; }
    public string? Description { get; set; }
    public EnumReceiverAcceptStatus ReceiverAcceptStatus { get; set; }
    public DateTime? ReceiverAcceptDate { get; set; }
    public DateTime? ReceiverRejectDate { get; set; }
    public string? ReceiverIpAddress { get; set; }



    public class CreateFinanceExpenseCommandHandler : IRequestHandler<CreateFinanceExpenseCommand, CreatedFinanceExpenseResponse>
    {
        private readonly IMapper _mapper;
        private readonly IFinanceExpenseWriteRepository _financeExpenseWriteRepository;
        private readonly IFinanceExpenseReadRepository _financeExpenseReadRepository;
        private readonly FinanceExpenseBusinessRules _financeExpenseBusinessRules;

        public CreateFinanceExpenseCommandHandler(IMapper mapper, IFinanceExpenseWriteRepository financeExpenseWriteRepository,
                                         FinanceExpenseBusinessRules financeExpenseBusinessRules, IFinanceExpenseReadRepository financeExpenseReadRepository)
        {
            _mapper = mapper;
            _financeExpenseWriteRepository = financeExpenseWriteRepository;
            _financeExpenseBusinessRules = financeExpenseBusinessRules;
            _financeExpenseReadRepository = financeExpenseReadRepository;
        }

        public async Task<CreatedFinanceExpenseResponse> Handle(CreateFinanceExpenseCommand request, CancellationToken cancellationToken)
        {
            await _financeExpenseBusinessRules.FinanceExpenseGroupShouldExistWhenSelected(request.GidExpenseGroupFK);
            await _financeExpenseBusinessRules.OrganizationShouldExistWhenSelected(request.GidOrganizationFK);
            await _financeExpenseBusinessRules.MoneySenderPersonnelShouldExistWhenSelected(request.GidMoneySenderPersonnelFK);
            await _financeExpenseBusinessRules.MoneyReceivePersonnelShouldExistWhenSelected(request.GidMoneyReceivePersonnelFK);
            await _financeExpenseBusinessRules.CurrencyShouldExistWhenSelected(request.GidCurrencyFK);
            await _financeExpenseBusinessRules.ApprovalReceiverShouldExistWhenSelected(request.GidApprovalReceiverFK);
            X.FinanceExpense financeExpense = _mapper.Map<X.FinanceExpense>(request);
            //financeExpense.RowNo = maxRowNo + 1;

            await _financeExpenseWriteRepository.AddAsync(financeExpense);
            await _financeExpenseWriteRepository.SaveAsync();

            X.FinanceExpense savedFinanceExpense = await _financeExpenseReadRepository.GetAsync(predicate: x => x.Gid == financeExpense.Gid, include: x => x.Include(x => x.FinanceExpenseGroupFK).Include(x => x.CurrencyFK).Include(x => x.MoneySenderPersonnelFK).Include(x => x.ApprovalReceiverFK).Include(x => x.OrganizationFK));
            //INCLUDES Buraya Gelecek include varsa eklenecek
            //include: x => x.Include(x => x.UserFK));

            GetByGidFinanceExpenseResponse obj = _mapper.Map<GetByGidFinanceExpenseResponse>(savedFinanceExpense);
            return new()
            {
                Title = FinanceExpensesBusinessMessages.ProcessCompleted,
                Message = FinanceExpensesBusinessMessages.SuccessCreatedFinanceExpenseMessage,
                IsValid = true,
                Obj = obj
            };
        }
    }
}
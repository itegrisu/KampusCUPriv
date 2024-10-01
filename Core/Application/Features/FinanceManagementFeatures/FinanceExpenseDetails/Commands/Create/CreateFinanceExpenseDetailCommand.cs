using Application.Features.FinanceManagementFeatures.FinanceExpenseDetails.Constants;
using Application.Features.FinanceManagementFeatures.FinanceExpenseDetails.Queries.GetByGid;
using Application.Features.FinanceManagementFeatures.FinanceExpenseDetails.Rules;
using Application.Repositories.FinanceManagementRepos.FinanceExpenseDetailRepo;
using AutoMapper;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using X = Domain.Entities.FinanceManagements;

namespace Application.Features.FinanceManagementFeatures.FinanceExpenseDetails.Commands.Create;

public class CreateFinanceExpenseDetailCommand : IRequest<CreatedFinanceExpenseDetailResponse>
{
    public Guid GidExpenseFK { get; set; }
    public Guid GidSpendPersonnelFK { get; set; }
    public Guid GidCurrencyFK { get; set; }
    public string? GidControlPersonnelFK { get; set; }

    public string SpentTitle { get; set; }
    public decimal Fee { get; set; }
    public DateTime TransactionDate { get; set; }
    public string? Document { get; set; }
    public string? Description { get; set; }
    public EnumApprovalStatus ApprovalStatus { get; set; }
    public DateTime? ControlDate { get; set; }
    public string? ControlDescription { get; set; }



    public class CreateFinanceExpenseDetailCommandHandler : IRequestHandler<CreateFinanceExpenseDetailCommand, CreatedFinanceExpenseDetailResponse>
    {
        private readonly IMapper _mapper;
        private readonly IFinanceExpenseDetailWriteRepository _financeExpenseDetailWriteRepository;
        private readonly IFinanceExpenseDetailReadRepository _financeExpenseDetailReadRepository;
        private readonly FinanceExpenseDetailBusinessRules _financeExpenseDetailBusinessRules;

        public CreateFinanceExpenseDetailCommandHandler(IMapper mapper, IFinanceExpenseDetailWriteRepository financeExpenseDetailWriteRepository,
                                         FinanceExpenseDetailBusinessRules financeExpenseDetailBusinessRules, IFinanceExpenseDetailReadRepository financeExpenseDetailReadRepository)
        {
            _mapper = mapper;
            _financeExpenseDetailWriteRepository = financeExpenseDetailWriteRepository;
            _financeExpenseDetailBusinessRules = financeExpenseDetailBusinessRules;
            _financeExpenseDetailReadRepository = financeExpenseDetailReadRepository;
        }

        public async Task<CreatedFinanceExpenseDetailResponse> Handle(CreateFinanceExpenseDetailCommand request, CancellationToken cancellationToken)
        {

            await _financeExpenseDetailBusinessRules.FinanceExpenseShouldExistWhenSelected(request.GidExpenseFK);
            await _financeExpenseDetailBusinessRules.SpendPersonnelShouldExistWhenSelected(request.GidSpendPersonnelFK);
            await _financeExpenseDetailBusinessRules.CurrencyShouldExistWhenSelected(request.GidCurrencyFK);
            await _financeExpenseDetailBusinessRules.ControlPersonnelShouldExistWhenSelected(request.GidControlPersonnelFK);

            X.FinanceExpenseDetail financeExpenseDetail = _mapper.Map<X.FinanceExpenseDetail>(request);
            //financeExpenseDetail.RowNo = maxRowNo + 1;

            await _financeExpenseDetailWriteRepository.AddAsync(financeExpenseDetail);
            await _financeExpenseDetailWriteRepository.SaveAsync();

            X.FinanceExpenseDetail savedFinanceExpenseDetail = await _financeExpenseDetailReadRepository.GetAsync(predicate: x => x.Gid == financeExpenseDetail.Gid, include: x => x.Include(x => x.ControlPersonnelFK).Include(x => x.CurrencyFK).Include(x => x.FinanceExpenseFK).Include(x => x.SpendPersonnelFK));
            //INCLUDES Buraya Gelecek include varsa eklenecek
            //include: x => x.Include(x => x.UserFK));

            GetByGidFinanceExpenseDetailResponse obj = _mapper.Map<GetByGidFinanceExpenseDetailResponse>(savedFinanceExpenseDetail);
            return new()
            {
                Title = FinanceExpenseDetailsBusinessMessages.ProcessCompleted,
                Message = FinanceExpenseDetailsBusinessMessages.SuccessCreatedFinanceExpenseDetailMessage,
                IsValid = true,
                Obj = obj
            };
        }
    }
}
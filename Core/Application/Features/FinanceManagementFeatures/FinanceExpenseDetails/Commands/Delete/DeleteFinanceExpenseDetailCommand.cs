using Application.Features.FinanceManagementFeatures.FinanceExpenseDetails.Constants;
using Application.Features.FinanceManagementFeatures.FinanceExpenseDetails.Rules;
using Application.Repositories.FinanceManagementRepos.FinanceExpenseDetailRepo;
using AutoMapper;
using X = Domain.Entities.FinanceManagements;
using MediatR;

namespace Application.Features.FinanceManagementFeatures.FinanceExpenseDetails.Commands.Delete;

public class DeleteFinanceExpenseDetailCommand : IRequest<DeletedFinanceExpenseDetailResponse>
{
	public Guid Gid { get; set; }

    public class DeleteFinanceExpenseDetailCommandHandler : IRequestHandler<DeleteFinanceExpenseDetailCommand, DeletedFinanceExpenseDetailResponse>
    {
        private readonly IMapper _mapper;
        private readonly IFinanceExpenseDetailReadRepository _financeExpenseDetailReadRepository;
        private readonly IFinanceExpenseDetailWriteRepository _financeExpenseDetailWriteRepository;
        private readonly FinanceExpenseDetailBusinessRules _financeExpenseDetailBusinessRules;

        public DeleteFinanceExpenseDetailCommandHandler(IMapper mapper, IFinanceExpenseDetailReadRepository financeExpenseDetailReadRepository,
                                         FinanceExpenseDetailBusinessRules financeExpenseDetailBusinessRules, IFinanceExpenseDetailWriteRepository financeExpenseDetailWriteRepository)
        {
            _mapper = mapper;
            _financeExpenseDetailReadRepository = financeExpenseDetailReadRepository;
            _financeExpenseDetailBusinessRules = financeExpenseDetailBusinessRules;
            _financeExpenseDetailWriteRepository = financeExpenseDetailWriteRepository;
        }

        public async Task<DeletedFinanceExpenseDetailResponse> Handle(DeleteFinanceExpenseDetailCommand request, CancellationToken cancellationToken)
        {
            X.FinanceExpenseDetail? financeExpenseDetail = await _financeExpenseDetailReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken);
            await _financeExpenseDetailBusinessRules.FinanceExpenseDetailShouldExistWhenSelected(financeExpenseDetail);
            financeExpenseDetail.DataState = Core.Enum.DataState.Deleted;

            _financeExpenseDetailWriteRepository.Update(financeExpenseDetail);
            await _financeExpenseDetailWriteRepository.SaveAsync();

            return new()
            {
                Title = FinanceExpenseDetailsBusinessMessages.ProcessCompleted,
                Message = FinanceExpenseDetailsBusinessMessages.SuccessDeletedFinanceExpenseDetailMessage,
                IsValid = true
            };
        }
    }
}
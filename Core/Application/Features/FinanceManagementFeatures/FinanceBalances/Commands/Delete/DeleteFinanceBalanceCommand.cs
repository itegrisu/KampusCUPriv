using Application.Features.FinanceManagementFeatures.FinanceBalances.Constants;
using Application.Features.FinanceManagementFeatures.FinanceBalances.Rules;
using Application.Repositories.FinanceManagementRepos.FinanceBalanceRepo;
using AutoMapper;
using X = Domain.Entities.FinanceManagements;
using MediatR;

namespace Application.Features.FinanceManagementFeatures.FinanceBalances.Commands.Delete;

public class DeleteFinanceBalanceCommand : IRequest<DeletedFinanceBalanceResponse>
{
	public Guid Gid { get; set; }
    public Guid UserGid {get; set; }

    public class DeleteFinanceBalanceCommandHandler : IRequestHandler<DeleteFinanceBalanceCommand, DeletedFinanceBalanceResponse>
    {
        private readonly IMapper _mapper;
        private readonly IFinanceBalanceReadRepository _financeBalanceReadRepository;
        private readonly IFinanceBalanceWriteRepository _financeBalanceWriteRepository;
        private readonly FinanceBalanceBusinessRules _financeBalanceBusinessRules;

        public DeleteFinanceBalanceCommandHandler(IMapper mapper, IFinanceBalanceReadRepository financeBalanceReadRepository,
                                         FinanceBalanceBusinessRules financeBalanceBusinessRules, IFinanceBalanceWriteRepository financeBalanceWriteRepository)
        {
            _mapper = mapper;
            _financeBalanceReadRepository = financeBalanceReadRepository;
            _financeBalanceBusinessRules = financeBalanceBusinessRules;
            _financeBalanceWriteRepository = financeBalanceWriteRepository;
        }

        public async Task<DeletedFinanceBalanceResponse> Handle(DeleteFinanceBalanceCommand request, CancellationToken cancellationToken)
        {
            await _financeBalanceBusinessRules.isSystemAdmin(request.UserGid);

            X.FinanceBalance? financeBalance = await _financeBalanceReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken);
            await _financeBalanceBusinessRules.FinanceBalanceShouldExistWhenSelected(financeBalance);
            financeBalance.DataState = Core.Enum.DataState.Deleted;

            _financeBalanceWriteRepository.Update(financeBalance);
            await _financeBalanceWriteRepository.SaveAsync();

            return new()
            {
                Title = FinanceBalancesBusinessMessages.ProcessCompleted,
                Message = FinanceBalancesBusinessMessages.SuccessDeletedFinanceBalanceMessage,
                IsValid = true
            };
        }
    }
}
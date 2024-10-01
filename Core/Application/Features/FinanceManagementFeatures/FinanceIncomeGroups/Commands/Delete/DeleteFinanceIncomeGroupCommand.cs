using Application.Features.FinanceManagementFeatures.FinanceIncomeGroups.Constants;
using Application.Features.FinanceManagementFeatures.FinanceIncomeGroups.Rules;
using Application.Repositories.FinanceManagementRepos.FinanceIncomeGroupRepo;
using AutoMapper;
using X = Domain.Entities.FinanceManagements;
using MediatR;

namespace Application.Features.FinanceManagementFeatures.FinanceIncomeGroups.Commands.Delete;

public class DeleteFinanceIncomeGroupCommand : IRequest<DeletedFinanceIncomeGroupResponse>
{
	public Guid Gid { get; set; }

    public class DeleteFinanceIncomeGroupCommandHandler : IRequestHandler<DeleteFinanceIncomeGroupCommand, DeletedFinanceIncomeGroupResponse>
    {
        private readonly IMapper _mapper;
        private readonly IFinanceIncomeGroupReadRepository _financeIncomeGroupReadRepository;
        private readonly IFinanceIncomeGroupWriteRepository _financeIncomeGroupWriteRepository;
        private readonly FinanceIncomeGroupBusinessRules _financeIncomeGroupBusinessRules;

        public DeleteFinanceIncomeGroupCommandHandler(IMapper mapper, IFinanceIncomeGroupReadRepository financeIncomeGroupReadRepository,
                                         FinanceIncomeGroupBusinessRules financeIncomeGroupBusinessRules, IFinanceIncomeGroupWriteRepository financeIncomeGroupWriteRepository)
        {
            _mapper = mapper;
            _financeIncomeGroupReadRepository = financeIncomeGroupReadRepository;
            _financeIncomeGroupBusinessRules = financeIncomeGroupBusinessRules;
            _financeIncomeGroupWriteRepository = financeIncomeGroupWriteRepository;
        }

        public async Task<DeletedFinanceIncomeGroupResponse> Handle(DeleteFinanceIncomeGroupCommand request, CancellationToken cancellationToken)
        {
            X.FinanceIncomeGroup? financeIncomeGroup = await _financeIncomeGroupReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken);
            await _financeIncomeGroupBusinessRules.FinanceIncomeGroupShouldExistWhenSelected(financeIncomeGroup);
            financeIncomeGroup.DataState = Core.Enum.DataState.Deleted;

            _financeIncomeGroupWriteRepository.Update(financeIncomeGroup);
            await _financeIncomeGroupWriteRepository.SaveAsync();

            return new()
            {
                Title = FinanceIncomeGroupsBusinessMessages.ProcessCompleted,
                Message = FinanceIncomeGroupsBusinessMessages.SuccessDeletedFinanceIncomeGroupMessage,
                IsValid = true
            };
        }
    }
}
using Application.Features.FinanceManagementFeatures.FinanceExpenseGroups.Constants;
using Application.Features.FinanceManagementFeatures.FinanceExpenseGroups.Rules;
using Application.Repositories.FinanceManagementRepos.FinanceExpenseGroupRepo;
using AutoMapper;
using X = Domain.Entities.FinanceManagements;
using MediatR;

namespace Application.Features.FinanceManagementFeatures.FinanceExpenseGroups.Commands.Delete;

public class DeleteFinanceExpenseGroupCommand : IRequest<DeletedFinanceExpenseGroupResponse>
{
	public Guid Gid { get; set; }

    public class DeleteFinanceExpenseGroupCommandHandler : IRequestHandler<DeleteFinanceExpenseGroupCommand, DeletedFinanceExpenseGroupResponse>
    {
        private readonly IMapper _mapper;
        private readonly IFinanceExpenseGroupReadRepository _financeExpenseGroupReadRepository;
        private readonly IFinanceExpenseGroupWriteRepository _financeExpenseGroupWriteRepository;
        private readonly FinanceExpenseGroupBusinessRules _financeExpenseGroupBusinessRules;

        public DeleteFinanceExpenseGroupCommandHandler(IMapper mapper, IFinanceExpenseGroupReadRepository financeExpenseGroupReadRepository,
                                         FinanceExpenseGroupBusinessRules financeExpenseGroupBusinessRules, IFinanceExpenseGroupWriteRepository financeExpenseGroupWriteRepository)
        {
            _mapper = mapper;
            _financeExpenseGroupReadRepository = financeExpenseGroupReadRepository;
            _financeExpenseGroupBusinessRules = financeExpenseGroupBusinessRules;
            _financeExpenseGroupWriteRepository = financeExpenseGroupWriteRepository;
        }

        public async Task<DeletedFinanceExpenseGroupResponse> Handle(DeleteFinanceExpenseGroupCommand request, CancellationToken cancellationToken)
        {
            X.FinanceExpenseGroup? financeExpenseGroup = await _financeExpenseGroupReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken);
            await _financeExpenseGroupBusinessRules.FinanceExpenseGroupShouldExistWhenSelected(financeExpenseGroup);
            financeExpenseGroup.DataState = Core.Enum.DataState.Deleted;

            _financeExpenseGroupWriteRepository.Update(financeExpenseGroup);
            await _financeExpenseGroupWriteRepository.SaveAsync();

            return new()
            {
                Title = FinanceExpenseGroupsBusinessMessages.ProcessCompleted,
                Message = FinanceExpenseGroupsBusinessMessages.SuccessDeletedFinanceExpenseGroupMessage,
                IsValid = true
            };
        }
    }
}
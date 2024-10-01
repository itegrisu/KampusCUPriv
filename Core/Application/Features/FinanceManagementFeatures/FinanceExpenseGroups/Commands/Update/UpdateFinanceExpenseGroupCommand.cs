using Application.Features.FinanceManagementFeatures.FinanceExpenseGroups.Constants;
using Application.Features.FinanceManagementFeatures.FinanceExpenseGroups.Queries.GetByGid;
using Application.Features.FinanceManagementFeatures.FinanceExpenseGroups.Rules;
using Application.Repositories.FinanceManagementRepos.FinanceExpenseGroupRepo;
using AutoMapper;
using Domain.Enums;
using MediatR;
using X = Domain.Entities.FinanceManagements;

namespace Application.Features.FinanceManagementFeatures.FinanceExpenseGroups.Commands.Update;

public class UpdateFinanceExpenseGroupCommand : IRequest<UpdatedFinanceExpenseGroupResponse>
{
    public Guid Gid { get; set; }


    public string Name { get; set; }
    public string? Description { get; set; }
    public EnumExpenseGroupStatus ExpenseGroupStatus { get; set; }


    public class UpdateFinanceExpenseGroupCommandHandler : IRequestHandler<UpdateFinanceExpenseGroupCommand, UpdatedFinanceExpenseGroupResponse>
    {
        private readonly IMapper _mapper;
        private readonly IFinanceExpenseGroupWriteRepository _financeExpenseGroupWriteRepository;
        private readonly IFinanceExpenseGroupReadRepository _financeExpenseGroupReadRepository;
        private readonly FinanceExpenseGroupBusinessRules _financeExpenseGroupBusinessRules;

        public UpdateFinanceExpenseGroupCommandHandler(IMapper mapper, IFinanceExpenseGroupWriteRepository financeExpenseGroupWriteRepository,
                                         FinanceExpenseGroupBusinessRules financeExpenseGroupBusinessRules, IFinanceExpenseGroupReadRepository financeExpenseGroupReadRepository)
        {
            _mapper = mapper;
            _financeExpenseGroupWriteRepository = financeExpenseGroupWriteRepository;
            _financeExpenseGroupBusinessRules = financeExpenseGroupBusinessRules;
            _financeExpenseGroupReadRepository = financeExpenseGroupReadRepository;
        }

        public async Task<UpdatedFinanceExpenseGroupResponse> Handle(UpdateFinanceExpenseGroupCommand request, CancellationToken cancellationToken)
        {
            X.FinanceExpenseGroup? financeExpenseGroup = await _financeExpenseGroupReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken);
            //INCLUDES Buraya Gelecek include varsa eklenecek
            await _financeExpenseGroupBusinessRules.FinanceExpenseGroupShouldExistWhenSelected(financeExpenseGroup);
            await _financeExpenseGroupBusinessRules.FinanceExpenseGroupShouldUnique(request.Name, request.Gid);
            financeExpenseGroup = _mapper.Map(request, financeExpenseGroup);

            _financeExpenseGroupWriteRepository.Update(financeExpenseGroup!);
            await _financeExpenseGroupWriteRepository.SaveAsync();
            GetByGidFinanceExpenseGroupResponse obj = _mapper.Map<GetByGidFinanceExpenseGroupResponse>(financeExpenseGroup);

            return new()
            {
                Title = FinanceExpenseGroupsBusinessMessages.ProcessCompleted,
                Message = FinanceExpenseGroupsBusinessMessages.SuccessCreatedFinanceExpenseGroupMessage,
                IsValid = true,
                Obj = obj
            };
        }
    }
}
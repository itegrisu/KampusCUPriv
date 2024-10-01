using Application.Features.FinanceManagementFeatures.FinanceIncomeGroups.Constants;
using Application.Features.FinanceManagementFeatures.FinanceIncomeGroups.Queries.GetByGid;
using Application.Features.FinanceManagementFeatures.FinanceIncomeGroups.Rules;
using Application.Repositories.FinanceManagementRepos.FinanceIncomeGroupRepo;
using AutoMapper;
using Domain.Enums;
using MediatR;
using X = Domain.Entities.FinanceManagements;

namespace Application.Features.FinanceManagementFeatures.FinanceIncomeGroups.Commands.Update;

public class UpdateFinanceIncomeGroupCommand : IRequest<UpdatedFinanceIncomeGroupResponse>
{
    public Guid Gid { get; set; }


    public string IncomeGroupName { get; set; }
    public string? Description { get; set; }
    public EnumIncomeGroupStatus IncomeGroupStatus { get; set; }




    public class UpdateFinanceIncomeGroupCommandHandler : IRequestHandler<UpdateFinanceIncomeGroupCommand, UpdatedFinanceIncomeGroupResponse>
    {
        private readonly IMapper _mapper;
        private readonly IFinanceIncomeGroupWriteRepository _financeIncomeGroupWriteRepository;
        private readonly IFinanceIncomeGroupReadRepository _financeIncomeGroupReadRepository;
        private readonly FinanceIncomeGroupBusinessRules _financeIncomeGroupBusinessRules;

        public UpdateFinanceIncomeGroupCommandHandler(IMapper mapper, IFinanceIncomeGroupWriteRepository financeIncomeGroupWriteRepository,
                                         FinanceIncomeGroupBusinessRules financeIncomeGroupBusinessRules, IFinanceIncomeGroupReadRepository financeIncomeGroupReadRepository)
        {
            _mapper = mapper;
            _financeIncomeGroupWriteRepository = financeIncomeGroupWriteRepository;
            _financeIncomeGroupBusinessRules = financeIncomeGroupBusinessRules;
            _financeIncomeGroupReadRepository = financeIncomeGroupReadRepository;
        }

        public async Task<UpdatedFinanceIncomeGroupResponse> Handle(UpdateFinanceIncomeGroupCommand request, CancellationToken cancellationToken)
        {
            X.FinanceIncomeGroup? financeIncomeGroup = await _financeIncomeGroupReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken);
            //INCLUDES Buraya Gelecek include varsa eklenecek
            await _financeIncomeGroupBusinessRules.FinanceIncomeGroupShouldExistWhenSelected(financeIncomeGroup);
            financeIncomeGroup = _mapper.Map(request, financeIncomeGroup);

            _financeIncomeGroupWriteRepository.Update(financeIncomeGroup!);
            await _financeIncomeGroupWriteRepository.SaveAsync();
            GetByGidFinanceIncomeGroupResponse obj = _mapper.Map<GetByGidFinanceIncomeGroupResponse>(financeIncomeGroup);

            return new()
            {
                Title = FinanceIncomeGroupsBusinessMessages.ProcessCompleted,
                Message = FinanceIncomeGroupsBusinessMessages.SuccessCreatedFinanceIncomeGroupMessage,
                IsValid = true,
                Obj = obj
            };
        }
    }
}
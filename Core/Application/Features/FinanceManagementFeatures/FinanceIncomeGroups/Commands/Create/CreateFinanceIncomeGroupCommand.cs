using Application.Features.FinanceManagementFeatures.FinanceIncomeGroups.Constants;
using Application.Features.FinanceManagementFeatures.FinanceIncomeGroups.Queries.GetByGid;
using Application.Features.FinanceManagementFeatures.FinanceIncomeGroups.Rules;
using Application.Repositories.FinanceManagementRepos.FinanceIncomeGroupRepo;
using AutoMapper;
using Domain.Entities.FinanceManagements;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using X = Domain.Entities.FinanceManagements;

namespace Application.Features.FinanceManagementFeatures.FinanceIncomeGroups.Commands.Create;

public class CreateFinanceIncomeGroupCommand : IRequest<CreatedFinanceIncomeGroupResponse>
{

    public string IncomeGroupName { get; set; }
    public string? Description { get; set; }
    public EnumIncomeGroupStatus IncomeGroupStatus { get; set; }


    public class CreateFinanceIncomeGroupCommandHandler : IRequestHandler<CreateFinanceIncomeGroupCommand, CreatedFinanceIncomeGroupResponse>
    {
        private readonly IMapper _mapper;
        private readonly IFinanceIncomeGroupWriteRepository _financeIncomeGroupWriteRepository;
        private readonly IFinanceIncomeGroupReadRepository _financeIncomeGroupReadRepository;
        private readonly FinanceIncomeGroupBusinessRules _financeIncomeGroupBusinessRules;

        public CreateFinanceIncomeGroupCommandHandler(IMapper mapper, IFinanceIncomeGroupWriteRepository financeIncomeGroupWriteRepository,
                                         FinanceIncomeGroupBusinessRules financeIncomeGroupBusinessRules, IFinanceIncomeGroupReadRepository financeIncomeGroupReadRepository)
        {
            _mapper = mapper;
            _financeIncomeGroupWriteRepository = financeIncomeGroupWriteRepository;
            _financeIncomeGroupBusinessRules = financeIncomeGroupBusinessRules;
            _financeIncomeGroupReadRepository = financeIncomeGroupReadRepository;
        }

        public async Task<CreatedFinanceIncomeGroupResponse> Handle(CreateFinanceIncomeGroupCommand request, CancellationToken cancellationToken)
        {

            List<FinanceIncomeGroup> financeIncomeGroups = await _financeIncomeGroupReadRepository.GetAll().ToListAsync();
            int maxRowNo = financeIncomeGroups.Count() == 0 ? 0 : financeIncomeGroups.Max(x => x.RowNo);

            X.FinanceIncomeGroup financeIncomeGroup = _mapper.Map<X.FinanceIncomeGroup>(request);
            financeIncomeGroup.RowNo = maxRowNo + 1;

            await _financeIncomeGroupWriteRepository.AddAsync(financeIncomeGroup);
            await _financeIncomeGroupWriteRepository.SaveAsync();

            X.FinanceIncomeGroup savedFinanceIncomeGroup = await _financeIncomeGroupReadRepository.GetAsync(predicate: x => x.Gid == financeIncomeGroup.Gid);

            GetByGidFinanceIncomeGroupResponse obj = _mapper.Map<GetByGidFinanceIncomeGroupResponse>(savedFinanceIncomeGroup);
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
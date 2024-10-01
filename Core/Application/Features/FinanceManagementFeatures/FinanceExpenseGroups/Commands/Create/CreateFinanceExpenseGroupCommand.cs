using Application.Features.FinanceManagementFeatures.FinanceExpenseGroups.Constants;
using Application.Features.FinanceManagementFeatures.FinanceExpenseGroups.Queries.GetByGid;
using Application.Features.FinanceManagementFeatures.FinanceExpenseGroups.Rules;
using Application.Repositories.FinanceManagementRepos.FinanceExpenseGroupRepo;
using AutoMapper;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using X = Domain.Entities.FinanceManagements;

namespace Application.Features.FinanceManagementFeatures.FinanceExpenseGroups.Commands.Create;

public class CreateFinanceExpenseGroupCommand : IRequest<CreatedFinanceExpenseGroupResponse>
{

    public string Name { get; set; }
    public string? Description { get; set; }
    public EnumExpenseGroupStatus ExpenseGroupStatus { get; set; }




    public class CreateFinanceExpenseGroupCommandHandler : IRequestHandler<CreateFinanceExpenseGroupCommand, CreatedFinanceExpenseGroupResponse>
    {
        private readonly IMapper _mapper;
        private readonly IFinanceExpenseGroupWriteRepository _financeExpenseGroupWriteRepository;
        private readonly IFinanceExpenseGroupReadRepository _financeExpenseGroupReadRepository;
        private readonly FinanceExpenseGroupBusinessRules _financeExpenseGroupBusinessRules;

        public CreateFinanceExpenseGroupCommandHandler(IMapper mapper, IFinanceExpenseGroupWriteRepository financeExpenseGroupWriteRepository,
                                         FinanceExpenseGroupBusinessRules financeExpenseGroupBusinessRules, IFinanceExpenseGroupReadRepository financeExpenseGroupReadRepository)
        {
            _mapper = mapper;
            _financeExpenseGroupWriteRepository = financeExpenseGroupWriteRepository;
            _financeExpenseGroupBusinessRules = financeExpenseGroupBusinessRules;
            _financeExpenseGroupReadRepository = financeExpenseGroupReadRepository;
        }

        public async Task<CreatedFinanceExpenseGroupResponse> Handle(CreateFinanceExpenseGroupCommand request, CancellationToken cancellationToken)
        {
            await _financeExpenseGroupBusinessRules.FinanceExpenseGroupShouldUnique(request.Name);
            List<X.FinanceExpenseGroup> financeExpenseGroups = await _financeExpenseGroupReadRepository.GetAll().ToListAsync();
            int maxRowNo = financeExpenseGroups.Count() == 0 ? 0 : financeExpenseGroups.Max(x => x.RowNo);

            X.FinanceExpenseGroup financeExpenseGroup = _mapper.Map<X.FinanceExpenseGroup>(request);
            financeExpenseGroup.RowNo = maxRowNo + 1;

            await _financeExpenseGroupWriteRepository.AddAsync(financeExpenseGroup);
            await _financeExpenseGroupWriteRepository.SaveAsync();

            X.FinanceExpenseGroup savedFinanceExpenseGroup = await _financeExpenseGroupReadRepository.GetAsync(predicate: x => x.Gid == financeExpenseGroup.Gid);
            //INCLUDES Buraya Gelecek include varsa eklenecek
            //include: x => x.Include(x => x.UserFK));

            GetByGidFinanceExpenseGroupResponse obj = _mapper.Map<GetByGidFinanceExpenseGroupResponse>(savedFinanceExpenseGroup);
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
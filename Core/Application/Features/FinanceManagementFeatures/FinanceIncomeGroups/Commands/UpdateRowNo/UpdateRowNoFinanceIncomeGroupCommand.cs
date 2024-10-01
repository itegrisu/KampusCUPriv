using Application.Features.FinanceManagementFeatures.FinanceIncomeGroups.Commands.Create;
using Application.Features.FinanceManagementFeatures.FinanceIncomeGroups.Rules;
using Application.Helpers.UpdateRowNo;
using Application.Repositories.FinanceManagementRepos.FinanceIncomeGroupRepo;
using AutoMapper;
using X = Domain.Entities.FinanceManagements;
using MediatR;

namespace Application.Features.FinanceManagementFeatures.FinanceIncomeGroups.Commands.UpdateRowNo
{
    public class UpdateRowNoFinanceIncomeGroupCommand : IRequest<UpdateRowNoFinanceIncomeGroupResponse>
    {
        public Guid Gid { get; set; }
        public bool IsUp { get; set; }

        public class UpdateRowNoFinanceIncomeGroupCommandHandler : IRequestHandler<UpdateRowNoFinanceIncomeGroupCommand, UpdateRowNoFinanceIncomeGroupResponse>
        {
            private readonly IMapper _mapper;
            private readonly IFinanceIncomeGroupWriteRepository _financeIncomeGroupWriteRepository;
            private readonly IFinanceIncomeGroupReadRepository _financeIncomeGroupReadRepository;
            private readonly FinanceIncomeGroupBusinessRules _financeIncomeGroupBusinessRules;
            private readonly UpdateRowNoHelper<UpdateRowNoFinanceIncomeGroupResponse, X.FinanceIncomeGroup> _updateRowNoHelper;

            public UpdateRowNoFinanceIncomeGroupCommandHandler(IMapper mapper, IFinanceIncomeGroupWriteRepository financeIncomeGroupWriteRepository,
                                             FinanceIncomeGroupBusinessRules financeIncomeGroupBusinessRules, IFinanceIncomeGroupReadRepository financeIncomeGroupReadRepository, UpdateRowNoHelper<UpdateRowNoFinanceIncomeGroupResponse, X.FinanceIncomeGroup> updateRowNoHelper)
            {
                _mapper = mapper;
                _financeIncomeGroupWriteRepository = financeIncomeGroupWriteRepository;
                _financeIncomeGroupBusinessRules = financeIncomeGroupBusinessRules;
                _financeIncomeGroupReadRepository = financeIncomeGroupReadRepository;
                _updateRowNoHelper = updateRowNoHelper;
            }

            public async Task<UpdateRowNoFinanceIncomeGroupResponse> Handle(UpdateRowNoFinanceIncomeGroupCommand request, CancellationToken cancellationToken)
            {
                List<X.FinanceIncomeGroup> lst = _financeIncomeGroupReadRepository.GetAll().OrderBy(a => a.RowNo).ToList();

                X.FinanceIncomeGroup select = lst.Where(a => a.Gid == request.Gid).FirstOrDefault();

                UpdateRowNoFinanceIncomeGroupResponse response = await _updateRowNoHelper.UpdateRowNo(lst, select, request.IsUp);

                await _financeIncomeGroupWriteRepository.SaveAsync();

                return response;
            }
        }
    }
}
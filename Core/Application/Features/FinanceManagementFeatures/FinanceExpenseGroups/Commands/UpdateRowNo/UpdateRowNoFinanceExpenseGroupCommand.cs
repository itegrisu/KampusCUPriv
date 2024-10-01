using Application.Features.FinanceManagementFeatures.FinanceExpenseGroups.Commands.Create;
using Application.Features.FinanceManagementFeatures.FinanceExpenseGroups.Rules;
using Application.Helpers.UpdateRowNo;
using Application.Repositories.FinanceManagementRepos.FinanceExpenseGroupRepo;
using AutoMapper;
using X = Domain.Entities.FinanceManagements;
using MediatR;

namespace Application.Features.FinanceManagementFeatures.FinanceExpenseGroups.Commands.UpdateRowNo
{
    public class UpdateRowNoFinanceExpenseGroupCommand : IRequest<UpdateRowNoFinanceExpenseGroupResponse>
    {
        public Guid Gid { get; set; }
        public bool IsUp { get; set; }

        public class UpdateRowNoFinanceExpenseGroupCommandHandler : IRequestHandler<UpdateRowNoFinanceExpenseGroupCommand, UpdateRowNoFinanceExpenseGroupResponse>
        {
            private readonly IMapper _mapper;
            private readonly IFinanceExpenseGroupWriteRepository _financeExpenseGroupWriteRepository;
            private readonly IFinanceExpenseGroupReadRepository _financeExpenseGroupReadRepository;
            private readonly FinanceExpenseGroupBusinessRules _financeExpenseGroupBusinessRules;
            private readonly UpdateRowNoHelper<UpdateRowNoFinanceExpenseGroupResponse, X.FinanceExpenseGroup> _updateRowNoHelper;

            public UpdateRowNoFinanceExpenseGroupCommandHandler(IMapper mapper, IFinanceExpenseGroupWriteRepository financeExpenseGroupWriteRepository,
                                             FinanceExpenseGroupBusinessRules financeExpenseGroupBusinessRules, IFinanceExpenseGroupReadRepository financeExpenseGroupReadRepository, UpdateRowNoHelper<UpdateRowNoFinanceExpenseGroupResponse, X.FinanceExpenseGroup> updateRowNoHelper)
            {
                _mapper = mapper;
                _financeExpenseGroupWriteRepository = financeExpenseGroupWriteRepository;
                _financeExpenseGroupBusinessRules = financeExpenseGroupBusinessRules;
                _financeExpenseGroupReadRepository = financeExpenseGroupReadRepository;
                _updateRowNoHelper = updateRowNoHelper;
            }

            public async Task<UpdateRowNoFinanceExpenseGroupResponse> Handle(UpdateRowNoFinanceExpenseGroupCommand request, CancellationToken cancellationToken)
            {
                List<X.FinanceExpenseGroup> lst = _financeExpenseGroupReadRepository.GetAll().OrderBy(a => a.RowNo).ToList();

                X.FinanceExpenseGroup select = lst.Where(a => a.Gid == request.Gid).FirstOrDefault();

                UpdateRowNoFinanceExpenseGroupResponse response = await _updateRowNoHelper.UpdateRowNo(lst, select, request.IsUp);

                await _financeExpenseGroupWriteRepository.SaveAsync();

                return response;
            }
        }
    }
}
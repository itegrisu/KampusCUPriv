using Application.Features.SupplierCustomerManagementFeatures.SCWorkHistories.Constants;
using Application.Features.SupplierCustomerManagementFeatures.SCWorkHistories.Rules;
using Application.Repositories.SupplierManagementRepos.SCWorkHistoryRepo;
using AutoMapper;
using MediatR;
using X = Domain.Entities.SupplierCustomerManagements;

namespace Application.Features.SupplierCustomerManagementFeatures.SCWorkHistories.Commands.Delete;

public class DeleteSCWorkHistoryCommand : IRequest<DeletedSCWorkHistoryResponse>
{
    public Guid Gid { get; set; }

    public class DeleteSCWorkHistoryCommandHandler : IRequestHandler<DeleteSCWorkHistoryCommand, DeletedSCWorkHistoryResponse>
    {
        private readonly IMapper _mapper;
        private readonly ISCWorkHistoryReadRepository _sCWorkHistoryReadRepository;
        private readonly ISCWorkHistoryWriteRepository _sCWorkHistoryWriteRepository;
        private readonly SCWorkHistoryBusinessRules _sCWorkHistoryBusinessRules;

        public DeleteSCWorkHistoryCommandHandler(IMapper mapper, ISCWorkHistoryReadRepository sCWorkHistoryReadRepository,
                                         SCWorkHistoryBusinessRules sCWorkHistoryBusinessRules, ISCWorkHistoryWriteRepository sCWorkHistoryWriteRepository)
        {
            _mapper = mapper;
            _sCWorkHistoryReadRepository = sCWorkHistoryReadRepository;
            _sCWorkHistoryBusinessRules = sCWorkHistoryBusinessRules;
            _sCWorkHistoryWriteRepository = sCWorkHistoryWriteRepository;
        }

        public async Task<DeletedSCWorkHistoryResponse> Handle(DeleteSCWorkHistoryCommand request, CancellationToken cancellationToken)
        {
            X.SCWorkHistory? sCWorkHistory = await _sCWorkHistoryReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken);
            await _sCWorkHistoryBusinessRules.SCWorkHistoryShouldExistWhenSelected(sCWorkHistory);
            sCWorkHistory.DataState = Core.Enum.DataState.Deleted;

            _sCWorkHistoryWriteRepository.Update(sCWorkHistory);
            await _sCWorkHistoryWriteRepository.SaveAsync();

            return new()
            {
                Title = SCWorkHistoriesBusinessMessages.ProcessCompleted,
                Message = SCWorkHistoriesBusinessMessages.SuccessDeletedSCWorkHistoryMessage,
                IsValid = true
            };
        }
    }
}
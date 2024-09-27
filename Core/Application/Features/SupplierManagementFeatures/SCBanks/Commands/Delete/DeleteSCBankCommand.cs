using Application.Features.SupplierCustomerManagementFeatures.SCBanks.Constants;
using Application.Features.SupplierCustomerManagementFeatures.SCBanks.Rules;
using Application.Repositories.SupplierManagementRepos.SCBankRepo;
using AutoMapper;
using MediatR;
using X = Domain.Entities.SupplierCustomerManagements;

namespace Application.Features.SupplierCustomerManagementFeatures.SCBanks.Commands.Delete;

public class DeleteSCBankCommand : IRequest<DeletedSCBankResponse>
{
    public Guid Gid { get; set; }

    public class DeleteSCBankCommandHandler : IRequestHandler<DeleteSCBankCommand, DeletedSCBankResponse>
    {
        private readonly IMapper _mapper;
        private readonly ISCBankReadRepository _sCBankReadRepository;
        private readonly ISCBankWriteRepository _sCBankWriteRepository;
        private readonly SCBankBusinessRules _sCBankBusinessRules;

        public DeleteSCBankCommandHandler(IMapper mapper, ISCBankReadRepository sCBankReadRepository,
                                         SCBankBusinessRules sCBankBusinessRules, ISCBankWriteRepository sCBankWriteRepository)
        {
            _mapper = mapper;
            _sCBankReadRepository = sCBankReadRepository;
            _sCBankBusinessRules = sCBankBusinessRules;
            _sCBankWriteRepository = sCBankWriteRepository;
        }

        public async Task<DeletedSCBankResponse> Handle(DeleteSCBankCommand request, CancellationToken cancellationToken)
        {
            X.SCBank? sCBank = await _sCBankReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken);
            await _sCBankBusinessRules.SCBankShouldExistWhenSelected(sCBank);
            sCBank.DataState = Core.Enum.DataState.Deleted;

            _sCBankWriteRepository.Update(sCBank);
            await _sCBankWriteRepository.SaveAsync();

            return new()
            {
                Title = SCBanksBusinessMessages.ProcessCompleted,
                Message = SCBanksBusinessMessages.SuccessDeletedSCBankMessage,
                IsValid = true
            };
        }
    }
}
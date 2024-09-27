using Application.Features.SupplierCustomerManagementFeatures.SCAddresses.Constants;
using Application.Features.SupplierCustomerManagementFeatures.SCAddresses.Rules;
using Application.Repositories.SupplierManagementRepos.SCAddressRepo;
using AutoMapper;
using MediatR;
using X = Domain.Entities.SupplierCustomerManagements;

namespace Application.Features.SupplierCustomerManagementFeatures.SCAddresses.Commands.Delete;

public class DeleteSCAddressCommand : IRequest<DeletedSCAddressResponse>
{
    public Guid Gid { get; set; }

    public class DeleteSCAddressCommandHandler : IRequestHandler<DeleteSCAddressCommand, DeletedSCAddressResponse>
    {
        private readonly IMapper _mapper;
        private readonly ISCAddressReadRepository _sCAddressReadRepository;
        private readonly ISCAddressWriteRepository _sCAddressWriteRepository;
        private readonly SCAddressBusinessRules _sCAddressBusinessRules;

        public DeleteSCAddressCommandHandler(IMapper mapper, ISCAddressReadRepository sCAddressReadRepository,
                                         SCAddressBusinessRules sCAddressBusinessRules, ISCAddressWriteRepository sCAddressWriteRepository)
        {
            _mapper = mapper;
            _sCAddressReadRepository = sCAddressReadRepository;
            _sCAddressBusinessRules = sCAddressBusinessRules;
            _sCAddressWriteRepository = sCAddressWriteRepository;
        }

        public async Task<DeletedSCAddressResponse> Handle(DeleteSCAddressCommand request, CancellationToken cancellationToken)
        {
            X.SCAddress? sCAddress = await _sCAddressReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken);
            await _sCAddressBusinessRules.SCAddressShouldExistWhenSelected(sCAddress);
            sCAddress.DataState = Core.Enum.DataState.Deleted;

            _sCAddressWriteRepository.Update(sCAddress);
            await _sCAddressWriteRepository.SaveAsync();

            return new()
            {
                Title = SCAddressesBusinessMessages.ProcessCompleted,
                Message = SCAddressesBusinessMessages.SuccessDeletedSCAddressMessage,
                IsValid = true
            };
        }
    }
}
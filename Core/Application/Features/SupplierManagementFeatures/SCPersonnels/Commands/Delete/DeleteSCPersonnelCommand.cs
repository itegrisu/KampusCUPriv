using Application.Features.SupplierCustomerManagementFeatures.SCPersonnels.Constants;
using Application.Features.SupplierCustomerManagementFeatures.SCPersonnels.Rules;
using Application.Repositories.SupplierManagementRepos.SCPersonnelRepo;
using AutoMapper;
using MediatR;
using X = Domain.Entities.SupplierCustomerManagements;

namespace Application.Features.SupplierCustomerManagementFeatures.SCPersonnels.Commands.Delete;

public class DeleteSCPersonnelCommand : IRequest<DeletedSCPersonnelResponse>
{
	public Guid Gid { get; set; }

    public class DeleteSCPersonnelCommandHandler : IRequestHandler<DeleteSCPersonnelCommand, DeletedSCPersonnelResponse>
    {
        private readonly IMapper _mapper;
        private readonly ISCPersonnelReadRepository _sCPersonnelReadRepository;
        private readonly ISCPersonnelWriteRepository _sCPersonnelWriteRepository;
        private readonly SCPersonnelBusinessRules _sCPersonnelBusinessRules;

        public DeleteSCPersonnelCommandHandler(IMapper mapper, ISCPersonnelReadRepository sCPersonnelReadRepository,
                                         SCPersonnelBusinessRules sCPersonnelBusinessRules, ISCPersonnelWriteRepository sCPersonnelWriteRepository)
        {
            _mapper = mapper;
            _sCPersonnelReadRepository = sCPersonnelReadRepository;
            _sCPersonnelBusinessRules = sCPersonnelBusinessRules;
            _sCPersonnelWriteRepository = sCPersonnelWriteRepository;
        }

        public async Task<DeletedSCPersonnelResponse> Handle(DeleteSCPersonnelCommand request, CancellationToken cancellationToken)
        {
            X.SCPersonnel? sCPersonnel = await _sCPersonnelReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken);
            await _sCPersonnelBusinessRules.SCPersonnelShouldExistWhenSelected(sCPersonnel);
            sCPersonnel.DataState = Core.Enum.DataState.Deleted;

            _sCPersonnelWriteRepository.Update(sCPersonnel);
            await _sCPersonnelWriteRepository.SaveAsync();

            return new()
            {
                Title = SCPersonnelsBusinessMessages.ProcessCompleted,
                Message = SCPersonnelsBusinessMessages.SuccessDeletedSCPersonnelMessage,
                IsValid = true
            };
        }
    }
}
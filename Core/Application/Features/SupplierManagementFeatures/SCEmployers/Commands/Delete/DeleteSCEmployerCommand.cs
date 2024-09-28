using Application.Features.SupplierCustomerManagementFeatures.SCEmployers.Constants;
using Application.Features.SupplierCustomerManagementFeatures.SCEmployers.Rules;
using Application.Repositories.SupplierManagementRepos.SCEmployerRepo;
using AutoMapper;
using MediatR;
using X = Domain.Entities.SupplierCustomerManagements;

namespace Application.Features.SupplierCustomerManagementFeatures.SCEmployers.Commands.Delete;

public class DeleteSCEmployerCommand : IRequest<DeletedSCEmployerResponse>
{
    public Guid Gid { get; set; }

    public class DeleteSCEmployerCommandHandler : IRequestHandler<DeleteSCEmployerCommand, DeletedSCEmployerResponse>
    {
        private readonly IMapper _mapper;
        private readonly ISCEmployerReadRepository _sCEmployerReadRepository;
        private readonly ISCEmployerWriteRepository _sCEmployerWriteRepository;
        private readonly SCEmployerBusinessRules _sCEmployerBusinessRules;

        public DeleteSCEmployerCommandHandler(IMapper mapper, ISCEmployerReadRepository sCEmployerReadRepository,
                                         SCEmployerBusinessRules sCEmployerBusinessRules, ISCEmployerWriteRepository sCEmployerWriteRepository)
        {
            _mapper = mapper;
            _sCEmployerReadRepository = sCEmployerReadRepository;
            _sCEmployerBusinessRules = sCEmployerBusinessRules;
            _sCEmployerWriteRepository = sCEmployerWriteRepository;
        }

        public async Task<DeletedSCEmployerResponse> Handle(DeleteSCEmployerCommand request, CancellationToken cancellationToken)
        {
            X.SCEmployer? sCEmployer = await _sCEmployerReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken);
            await _sCEmployerBusinessRules.SCEmployerShouldExistWhenSelected(sCEmployer);
            sCEmployer.DataState = Core.Enum.DataState.Deleted;

            _sCEmployerWriteRepository.Update(sCEmployer);
            await _sCEmployerWriteRepository.SaveAsync();

            return new()
            {
                Title = SCEmployersBusinessMessages.ProcessCompleted,
                Message = SCEmployersBusinessMessages.SuccessDeletedSCEmployerMessage,
                IsValid = true
            };
        }
    }
}
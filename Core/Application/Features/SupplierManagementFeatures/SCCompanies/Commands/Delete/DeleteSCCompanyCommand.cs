using Application.Features.SupplierCustomerManagementFeatures.SCCompanies.Constants;
using Application.Features.SupplierCustomerManagementFeatures.SCCompanies.Rules;
using Application.Repositories.SupplierManagementRepos.SCCompanyRepo;
using AutoMapper;
using MediatR;
using X = Domain.Entities.SupplierCustomerManagements;

namespace Application.Features.SupplierCustomerManagementFeatures.SCCompanies.Commands.Delete;

public class DeleteSCCompanyCommand : IRequest<DeletedSCCompanyResponse>
{
    public Guid Gid { get; set; }

    public class DeleteSCCompanyCommandHandler : IRequestHandler<DeleteSCCompanyCommand, DeletedSCCompanyResponse>
    {
        private readonly IMapper _mapper;
        private readonly ISCCompanyReadRepository _sCCompanyReadRepository;
        private readonly ISCCompanyWriteRepository _sCCompanyWriteRepository;
        private readonly SCCompanyBusinessRules _sCCompanyBusinessRules;

        public DeleteSCCompanyCommandHandler(IMapper mapper, ISCCompanyReadRepository sCCompanyReadRepository,
                                         SCCompanyBusinessRules sCCompanyBusinessRules, ISCCompanyWriteRepository sCCompanyWriteRepository)
        {
            _mapper = mapper;
            _sCCompanyReadRepository = sCCompanyReadRepository;
            _sCCompanyBusinessRules = sCCompanyBusinessRules;
            _sCCompanyWriteRepository = sCCompanyWriteRepository;
        }

        public async Task<DeletedSCCompanyResponse> Handle(DeleteSCCompanyCommand request, CancellationToken cancellationToken)
        {
            X.SCCompany? sCCompany = await _sCCompanyReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken);
            await _sCCompanyBusinessRules.SCCompanyShouldExistWhenSelected(sCCompany);
            sCCompany.DataState = Core.Enum.DataState.Deleted;

            _sCCompanyWriteRepository.Update(sCCompany);
            await _sCCompanyWriteRepository.SaveAsync();

            return new()
            {
                Title = SCCompaniesBusinessMessages.ProcessCompleted,
                Message = SCCompaniesBusinessMessages.SuccessDeletedSCCompanyMessage,
                IsValid = true
            };
        }
    }
}
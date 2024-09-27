using Application.Features.SupplierCustomerManagementFeatures.SCBanks.Constants;
using Application.Features.SupplierCustomerManagementFeatures.SCBanks.Queries.GetByGid;
using Application.Features.SupplierCustomerManagementFeatures.SCBanks.Rules;
using Application.Repositories.SupplierManagementRepos.SCBankRepo;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using X = Domain.Entities.SupplierCustomerManagements;

namespace Application.Features.SupplierCustomerManagementFeatures.SCBanks.Commands.Create;

public class CreateSCBankCommand : IRequest<CreatedSCBankResponse>
{
    public Guid GidSCCompanyFK { get; set; }
    public Guid GidCurrencyFK { get; set; }

    public string Bank { get; set; }
    public string BranchName { get; set; }
    public string? BranchCode { get; set; }
    public string AccountNumber { get; set; }
    public string? IbanNo { get; set; }
    public string? SwiftNo { get; set; }



    public class CreateSCBankCommandHandler : IRequestHandler<CreateSCBankCommand, CreatedSCBankResponse>
    {
        private readonly IMapper _mapper;
        private readonly ISCBankWriteRepository _sCBankWriteRepository;
        private readonly ISCBankReadRepository _sCBankReadRepository;
        private readonly SCBankBusinessRules _sCBankBusinessRules;

        public CreateSCBankCommandHandler(IMapper mapper, ISCBankWriteRepository sCBankWriteRepository,
                                         SCBankBusinessRules sCBankBusinessRules, ISCBankReadRepository sCBankReadRepository)
        {
            _mapper = mapper;
            _sCBankWriteRepository = sCBankWriteRepository;
            _sCBankBusinessRules = sCBankBusinessRules;
            _sCBankReadRepository = sCBankReadRepository;
        }

        public async Task<CreatedSCBankResponse> Handle(CreateSCBankCommand request, CancellationToken cancellationToken)
        {
            await _sCBankBusinessRules.SCCompanyShouldExistWhenSelected(request.GidSCCompanyFK);
            await _sCBankBusinessRules.CurrencyShouldExistWhenSelected(request.GidCurrencyFK);

            X.SCBank sCBank = _mapper.Map<X.SCBank>(request);

            await _sCBankWriteRepository.AddAsync(sCBank);
            await _sCBankWriteRepository.SaveAsync();

            X.SCBank savedSCBank = await _sCBankReadRepository.GetAsync(predicate: x => x.Gid == sCBank.Gid, include: x => x.Include(x => x.SCCompanyFK).Include(x => x.CurrencyFK));
            //INCLUDES Buraya Gelecek include varsa eklenecek
            //include: x => x.Include(x => x.UserFK));

            GetByGidSCBankResponse obj = _mapper.Map<GetByGidSCBankResponse>(savedSCBank);
            return new()
            {
                Title = SCBanksBusinessMessages.ProcessCompleted,
                Message = SCBanksBusinessMessages.SuccessCreatedSCBankMessage,
                IsValid = true,
                Obj = obj
            };
        }
    }
}
using Application.Features.SupplierCustomerManagementFeatures.SCBanks.Constants;
using Application.Features.SupplierCustomerManagementFeatures.SCBanks.Queries.GetByGid;
using Application.Features.SupplierCustomerManagementFeatures.SCBanks.Rules;
using Application.Repositories.SupplierManagementRepos.SCBankRepo;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using X = Domain.Entities.SupplierCustomerManagements;

namespace Application.Features.SupplierCustomerManagementFeatures.SCBanks.Commands.Update;

public class UpdateSCBankCommand : IRequest<UpdatedSCBankResponse>
{
    public Guid Gid { get; set; }
    public Guid GidSCCompanyFK { get; set; }
    public Guid GidCurrencyFK { get; set; }
    public string Bank { get; set; }
    public string BranchName { get; set; }
    public string? BranchCode { get; set; }
    public string AccountNumber { get; set; }
    public string? IbanNo { get; set; }
    public string? SwiftNo { get; set; }



    public class UpdateSCBankCommandHandler : IRequestHandler<UpdateSCBankCommand, UpdatedSCBankResponse>
    {
        private readonly IMapper _mapper;
        private readonly ISCBankWriteRepository _sCBankWriteRepository;
        private readonly ISCBankReadRepository _sCBankReadRepository;
        private readonly SCBankBusinessRules _sCBankBusinessRules;

        public UpdateSCBankCommandHandler(IMapper mapper, ISCBankWriteRepository sCBankWriteRepository,
                                         SCBankBusinessRules sCBankBusinessRules, ISCBankReadRepository sCBankReadRepository)
        {
            _mapper = mapper;
            _sCBankWriteRepository = sCBankWriteRepository;
            _sCBankBusinessRules = sCBankBusinessRules;
            _sCBankReadRepository = sCBankReadRepository;
        }

        public async Task<UpdatedSCBankResponse> Handle(UpdateSCBankCommand request, CancellationToken cancellationToken)
        {
            X.SCBank? sCBank = await _sCBankReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken);
            //INCLUDES Buraya Gelecek include varsa eklenecek
            await _sCBankBusinessRules.SCBankShouldExistWhenSelected(sCBank);
            await _sCBankBusinessRules.SCCompanyShouldExistWhenSelected(request.GidSCCompanyFK);
            await _sCBankBusinessRules.CurrencyShouldExistWhenSelected(request.GidCurrencyFK);
            sCBank = _mapper.Map(request, sCBank);

            _sCBankWriteRepository.Update(sCBank!);
            await _sCBankWriteRepository.SaveAsync();

            X.SCBank savedSCBank = await _sCBankReadRepository.GetAsync(predicate: x => x.Gid == sCBank.Gid, include: x => x.Include(x => x.SCCompanyFK).Include(x => x.CurrencyFK));

            GetByGidSCBankResponse obj = _mapper.Map<GetByGidSCBankResponse>(sCBank);

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
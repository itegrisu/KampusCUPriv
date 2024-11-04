using Application.Features.SupplierCustomerManagementFeatures.SCCompanies.Constants;
using Application.Features.SupplierCustomerManagementFeatures.SCCompanies.Queries.GetByGid;
using Application.Features.SupplierCustomerManagementFeatures.SCCompanies.Rules;
using Application.Repositories.SupplierManagementRepos.SCCompanyRepo;
using AutoMapper;
using Domain.Enums;
using MediatR;
using X = Domain.Entities.SupplierCustomerManagements;

namespace Application.Features.SupplierCustomerManagementFeatures.SCCompanies.Commands.Update;

public class UpdateSCCompanyCommand : IRequest<UpdatedSCCompanyResponse>
{
    public Guid Gid { get; set; }


    public string CompanyName { get; set; }
    public string? Phone { get; set; }
    public string? WebSite { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
    public bool WebLoginStatus { get; set; }
    public string? Description { get; set; }
    public string? SpecialNote { get; set; }
    public string? TaxOffice { get; set; }
    public string? TaxNumber { get; set; }
    public string? Keywords { get; set; }
    public EnumPartnerType PartnerType { get; set; }
    public int? SupplierRank { get; set; }
    public int? CustomerRank { get; set; }
    public bool IsHotel { get; set; }
    public EnumType Type { get; set; }
    public EnumStatus Status { get; set; }



    public class UpdateSCCompanyCommandHandler : IRequestHandler<UpdateSCCompanyCommand, UpdatedSCCompanyResponse>
    {
        private readonly IMapper _mapper;
        private readonly ISCCompanyWriteRepository _sCCompanyWriteRepository;
        private readonly ISCCompanyReadRepository _sCCompanyReadRepository;
        private readonly SCCompanyBusinessRules _sCCompanyBusinessRules;

        public UpdateSCCompanyCommandHandler(IMapper mapper, ISCCompanyWriteRepository sCCompanyWriteRepository,
                                         SCCompanyBusinessRules sCCompanyBusinessRules, ISCCompanyReadRepository sCCompanyReadRepository)
        {
            _mapper = mapper;
            _sCCompanyWriteRepository = sCCompanyWriteRepository;
            _sCCompanyBusinessRules = sCCompanyBusinessRules;
            _sCCompanyReadRepository = sCCompanyReadRepository;
        }

        public async Task<UpdatedSCCompanyResponse> Handle(UpdateSCCompanyCommand request, CancellationToken cancellationToken)
        {
            X.SCCompany? sCCompany = await _sCCompanyReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken);
            //INCLUDES Buraya Gelecek include varsa eklenecek
            await _sCCompanyBusinessRules.SCCompanyShouldExistWhenSelected(sCCompany);
            await _sCCompanyBusinessRules.SCCompanyShouldUnique(request.CompanyName, request.Gid);
            sCCompany = _mapper.Map(request, sCCompany);

            _sCCompanyWriteRepository.Update(sCCompany!);
            await _sCCompanyWriteRepository.SaveAsync();
            GetByGidSCCompanyResponse obj = _mapper.Map<GetByGidSCCompanyResponse>(sCCompany);

            return new()
            {
                Title = SCCompaniesBusinessMessages.ProcessCompleted,
                Message = SCCompaniesBusinessMessages.SuccessCreatedSCCompanyMessage,
                IsValid = true,
                Obj = obj
            };
        }
    }
}
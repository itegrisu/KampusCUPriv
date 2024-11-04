using Application.Features.SupplierCustomerManagementFeatures.SCCompanies.Constants;
using Application.Features.SupplierCustomerManagementFeatures.SCCompanies.Queries.GetByGid;
using Application.Features.SupplierCustomerManagementFeatures.SCCompanies.Rules;
using Application.Repositories.SupplierManagementRepos.SCCompanyRepo;
using AutoMapper;
using Domain.Enums;
using MediatR;
using X = Domain.Entities.SupplierCustomerManagements;

namespace Application.Features.SupplierCustomerManagementFeatures.SCCompanies.Commands.Create;

public class CreateSCCompanyCommand : IRequest<CreatedSCCompanyResponse>
{

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



    public class CreateSCCompanyCommandHandler : IRequestHandler<CreateSCCompanyCommand, CreatedSCCompanyResponse>
    {
        private readonly IMapper _mapper;
        private readonly ISCCompanyWriteRepository _sCCompanyWriteRepository;
        private readonly ISCCompanyReadRepository _sCCompanyReadRepository;
        private readonly SCCompanyBusinessRules _sCCompanyBusinessRules;

        public CreateSCCompanyCommandHandler(IMapper mapper, ISCCompanyWriteRepository sCCompanyWriteRepository,
                                         SCCompanyBusinessRules sCCompanyBusinessRules, ISCCompanyReadRepository sCCompanyReadRepository)
        {
            _mapper = mapper;
            _sCCompanyWriteRepository = sCCompanyWriteRepository;
            _sCCompanyBusinessRules = sCCompanyBusinessRules;
            _sCCompanyReadRepository = sCCompanyReadRepository;
        }

        public async Task<CreatedSCCompanyResponse> Handle(CreateSCCompanyCommand request, CancellationToken cancellationToken)
        {
            await _sCCompanyBusinessRules.SCCompanyShouldUnique(request.CompanyName);
            X.SCCompany sCCompany = _mapper.Map<X.SCCompany>(request);
            //sCCompany.RowNo = maxRowNo + 1;

            await _sCCompanyWriteRepository.AddAsync(sCCompany);
            await _sCCompanyWriteRepository.SaveAsync();

            X.SCCompany savedSCCompany = await _sCCompanyReadRepository.GetAsync(predicate: x => x.Gid == sCCompany.Gid);
            //INCLUDES Buraya Gelecek include varsa eklenecek
            //include: x => x.Include(x => x.UserFK));

            GetByGidSCCompanyResponse obj = _mapper.Map<GetByGidSCCompanyResponse>(savedSCCompany);
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
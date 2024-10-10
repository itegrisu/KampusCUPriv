using Application.Features.MarketingManagementFeatures.MarketingCustomers.Constants;
using Application.Features.MarketingManagementFeatures.MarketingCustomers.Queries.GetByGid;
using Application.Features.MarketingManagementFeatures.MarketingCustomers.Rules;
using Application.Repositories.MarketingManagementsRepos.MarketingCustomerRepo;
using AutoMapper;
using MediatR;
using X = Domain.Entities.MarketingManagements;

namespace Application.Features.MarketingManagementFeatures.MarketingCustomers.Commands.Create;

public class CreateMarketingCustomerCommand : IRequest<CreatedMarketingCustomerResponse>
{

    public string FullName { get; set; }
    public string? Company { get; set; }
    public string Duty { get; set; }
    public string? PreviousDuty { get; set; }
    public string? Gsm { get; set; }
    public string? Email { get; set; }
    public string? Description { get; set; }



    public class CreateMarketingCustomerCommandHandler : IRequestHandler<CreateMarketingCustomerCommand, CreatedMarketingCustomerResponse>
    {
        private readonly IMapper _mapper;
        private readonly IMarketingCustomerWriteRepository _marketingCustomerWriteRepository;
        private readonly IMarketingCustomerReadRepository _marketingCustomerReadRepository;
        private readonly MarketingCustomerBusinessRules _marketingCustomerBusinessRules;

        public CreateMarketingCustomerCommandHandler(IMapper mapper, IMarketingCustomerWriteRepository marketingCustomerWriteRepository,
                                         MarketingCustomerBusinessRules marketingCustomerBusinessRules, IMarketingCustomerReadRepository marketingCustomerReadRepository)
        {
            _mapper = mapper;
            _marketingCustomerWriteRepository = marketingCustomerWriteRepository;
            _marketingCustomerBusinessRules = marketingCustomerBusinessRules;
            _marketingCustomerReadRepository = marketingCustomerReadRepository;
        }

        public async Task<CreatedMarketingCustomerResponse> Handle(CreateMarketingCustomerCommand request, CancellationToken cancellationToken)
        {
            //int maxRowNo = await _marketingCustomerReadRepository.GetAll().MaxAsync(r => r.RowNo);
            X.MarketingCustomer marketingCustomer = _mapper.Map<X.MarketingCustomer>(request);
            //marketingCustomer.RowNo = maxRowNo + 1;

            await _marketingCustomerWriteRepository.AddAsync(marketingCustomer);
            await _marketingCustomerWriteRepository.SaveAsync();

            X.MarketingCustomer savedMarketingCustomer = await _marketingCustomerReadRepository.GetAsync(predicate: x => x.Gid == marketingCustomer.Gid);
            //INCLUDES Buraya Gelecek include varsa eklenecek
            //include: x => x.Include(x => x.UserFK));

            GetByGidMarketingCustomerResponse obj = _mapper.Map<GetByGidMarketingCustomerResponse>(savedMarketingCustomer);
            return new()
            {
                Title = MarketingCustomersBusinessMessages.ProcessCompleted,
                Message = MarketingCustomersBusinessMessages.SuccessCreatedMarketingCustomerMessage,
                IsValid = true,
                Obj = obj
            };
        }
    }
}
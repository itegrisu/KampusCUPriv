using Application.Features.MarketingManagementFeatures.MarketingCustomers.Constants;
using Application.Features.MarketingManagementFeatures.MarketingCustomers.Queries.GetByGid;
using Application.Features.MarketingManagementFeatures.MarketingCustomers.Rules;
using Application.Repositories.MarketingManagementsRepos.MarketingCustomerRepo;
using AutoMapper;
using MediatR;
using X = Domain.Entities.MarketingManagements;

namespace Application.Features.MarketingManagementFeatures.MarketingCustomers.Commands.Update;

public class UpdateMarketingCustomerCommand : IRequest<UpdatedMarketingCustomerResponse>
{
    public Guid Gid { get; set; }


    public string FullName { get; set; }
    public string? Company { get; set; }
    public string Duty { get; set; }
    public string? PreviousDuty { get; set; }
    public string? Gsm { get; set; }
    public string? Email { get; set; }
    public string? Description { get; set; }



    public class UpdateMarketingCustomerCommandHandler : IRequestHandler<UpdateMarketingCustomerCommand, UpdatedMarketingCustomerResponse>
    {
        private readonly IMapper _mapper;
        private readonly IMarketingCustomerWriteRepository _marketingCustomerWriteRepository;
        private readonly IMarketingCustomerReadRepository _marketingCustomerReadRepository;
        private readonly MarketingCustomerBusinessRules _marketingCustomerBusinessRules;

        public UpdateMarketingCustomerCommandHandler(IMapper mapper, IMarketingCustomerWriteRepository marketingCustomerWriteRepository,
                                         MarketingCustomerBusinessRules marketingCustomerBusinessRules, IMarketingCustomerReadRepository marketingCustomerReadRepository)
        {
            _mapper = mapper;
            _marketingCustomerWriteRepository = marketingCustomerWriteRepository;
            _marketingCustomerBusinessRules = marketingCustomerBusinessRules;
            _marketingCustomerReadRepository = marketingCustomerReadRepository;
        }

        public async Task<UpdatedMarketingCustomerResponse> Handle(UpdateMarketingCustomerCommand request, CancellationToken cancellationToken)
        {
            X.MarketingCustomer? marketingCustomer = await _marketingCustomerReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken);
            //INCLUDES Buraya Gelecek include varsa eklenecek
            await _marketingCustomerBusinessRules.MarketingCustomerShouldExistWhenSelected(marketingCustomer);
            marketingCustomer = _mapper.Map(request, marketingCustomer);

            _marketingCustomerWriteRepository.Update(marketingCustomer!);
            await _marketingCustomerWriteRepository.SaveAsync();
            GetByGidMarketingCustomerResponse obj = _mapper.Map<GetByGidMarketingCustomerResponse>(marketingCustomer);

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
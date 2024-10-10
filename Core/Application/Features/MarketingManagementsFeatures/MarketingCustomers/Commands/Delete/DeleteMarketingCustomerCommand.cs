using Application.Features.MarketingManagementFeatures.MarketingCustomers.Constants;
using Application.Features.MarketingManagementFeatures.MarketingCustomers.Rules;
using Application.Repositories.MarketingManagementsRepos.MarketingCustomerRepo;
using AutoMapper;
using MediatR;
using X = Domain.Entities.MarketingManagements;

namespace Application.Features.MarketingManagementFeatures.MarketingCustomers.Commands.Delete;

public class DeleteMarketingCustomerCommand : IRequest<DeletedMarketingCustomerResponse>
{
    public Guid Gid { get; set; }

    public class DeleteMarketingCustomerCommandHandler : IRequestHandler<DeleteMarketingCustomerCommand, DeletedMarketingCustomerResponse>
    {
        private readonly IMapper _mapper;
        private readonly IMarketingCustomerReadRepository _marketingCustomerReadRepository;
        private readonly IMarketingCustomerWriteRepository _marketingCustomerWriteRepository;
        private readonly MarketingCustomerBusinessRules _marketingCustomerBusinessRules;

        public DeleteMarketingCustomerCommandHandler(IMapper mapper, IMarketingCustomerReadRepository marketingCustomerReadRepository,
                                         MarketingCustomerBusinessRules marketingCustomerBusinessRules, IMarketingCustomerWriteRepository marketingCustomerWriteRepository)
        {
            _mapper = mapper;
            _marketingCustomerReadRepository = marketingCustomerReadRepository;
            _marketingCustomerBusinessRules = marketingCustomerBusinessRules;
            _marketingCustomerWriteRepository = marketingCustomerWriteRepository;
        }

        public async Task<DeletedMarketingCustomerResponse> Handle(DeleteMarketingCustomerCommand request, CancellationToken cancellationToken)
        {
            X.MarketingCustomer? marketingCustomer = await _marketingCustomerReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken);
            await _marketingCustomerBusinessRules.MarketingCustomerShouldExistWhenSelected(marketingCustomer);
            marketingCustomer.DataState = Core.Enum.DataState.Deleted;

            _marketingCustomerWriteRepository.Update(marketingCustomer);
            await _marketingCustomerWriteRepository.SaveAsync();

            return new()
            {
                Title = MarketingCustomersBusinessMessages.ProcessCompleted,
                Message = MarketingCustomersBusinessMessages.SuccessDeletedMarketingCustomerMessage,
                IsValid = true
            };
        }
    }
}
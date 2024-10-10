using Application.Features.MarketingManagementFeatures.MarketingVisitPlans.Constants;
using Application.Features.MarketingManagementFeatures.MarketingVisitPlans.Rules;
using Application.Repositories.MarketingManagementsRepos.MerketingVisitPlanRepo;
using AutoMapper;
using MediatR;
using X = Domain.Entities.MarketingManagements;

namespace Application.Features.MarketingManagementFeatures.MarketingVisitPlans.Commands.Delete;

public class DeleteMarketingVisitPlanCommand : IRequest<DeletedMarketingVisitPlanResponse>
{
    public Guid Gid { get; set; }

    public class DeleteMarketingVisitPlanCommandHandler : IRequestHandler<DeleteMarketingVisitPlanCommand, DeletedMarketingVisitPlanResponse>
    {
        private readonly IMapper _mapper;
        private readonly IMarketingVisitPlanReadRepository _marketingVisitPlanReadRepository;
        private readonly IMarketingVisitPlanWriteRepository _marketingVisitPlanWriteRepository;
        private readonly MarketingVisitPlanBusinessRules _marketingVisitPlanBusinessRules;

        public DeleteMarketingVisitPlanCommandHandler(IMapper mapper, IMarketingVisitPlanReadRepository marketingVisitPlanReadRepository,
                                         MarketingVisitPlanBusinessRules marketingVisitPlanBusinessRules, IMarketingVisitPlanWriteRepository marketingVisitPlanWriteRepository)
        {
            _mapper = mapper;
            _marketingVisitPlanReadRepository = marketingVisitPlanReadRepository;
            _marketingVisitPlanBusinessRules = marketingVisitPlanBusinessRules;
            _marketingVisitPlanWriteRepository = marketingVisitPlanWriteRepository;
        }

        public async Task<DeletedMarketingVisitPlanResponse> Handle(DeleteMarketingVisitPlanCommand request, CancellationToken cancellationToken)
        {
            X.MarketingVisitPlan? marketingVisitPlan = await _marketingVisitPlanReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken);
            await _marketingVisitPlanBusinessRules.MarketingVisitPlanShouldExistWhenSelected(marketingVisitPlan);
            marketingVisitPlan.DataState = Core.Enum.DataState.Deleted;

            _marketingVisitPlanWriteRepository.Update(marketingVisitPlan);
            await _marketingVisitPlanWriteRepository.SaveAsync();

            return new()
            {
                Title = MarketingVisitPlansBusinessMessages.ProcessCompleted,
                Message = MarketingVisitPlansBusinessMessages.SuccessDeletedMarketingVisitPlanMessage,
                IsValid = true
            };
        }
    }
}
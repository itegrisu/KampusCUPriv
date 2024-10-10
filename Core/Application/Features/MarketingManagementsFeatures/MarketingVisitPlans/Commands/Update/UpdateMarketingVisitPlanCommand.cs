using Application.Features.MarketingManagementFeatures.MarketingVisitPlans.Constants;
using Application.Features.MarketingManagementFeatures.MarketingVisitPlans.Queries.GetByGid;
using Application.Features.MarketingManagementFeatures.MarketingVisitPlans.Rules;
using Application.Repositories.MarketingManagementsRepos.MerketingVisitPlanRepo;
using AutoMapper;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using X = Domain.Entities.MarketingManagements;

namespace Application.Features.MarketingManagementFeatures.MarketingVisitPlans.Commands.Update;

public class UpdateMarketingVisitPlanCommand : IRequest<UpdatedMarketingVisitPlanResponse>
{
    public Guid Gid { get; set; }

    public Guid GidPersonnelFK { get; set; }
    public Guid GidVisitCustomerFK { get; set; }

    public string Title { get; set; }
    public DateTime PlanningVisitDate { get; set; }
    public string? Description { get; set; }
    public EnumVisitStatus VisitStatus { get; set; }
    public DateTime? VisitDate { get; set; }
    public int? VisitRank { get; set; }
    public string? VisitNote { get; set; }



    public class UpdateMarketingVisitPlanCommandHandler : IRequestHandler<UpdateMarketingVisitPlanCommand, UpdatedMarketingVisitPlanResponse>
    {
        private readonly IMapper _mapper;
        private readonly IMarketingVisitPlanWriteRepository _marketingVisitPlanWriteRepository;
        private readonly IMarketingVisitPlanReadRepository _marketingVisitPlanReadRepository;
        private readonly MarketingVisitPlanBusinessRules _marketingVisitPlanBusinessRules;

        public UpdateMarketingVisitPlanCommandHandler(IMapper mapper, IMarketingVisitPlanWriteRepository marketingVisitPlanWriteRepository,
                                         MarketingVisitPlanBusinessRules marketingVisitPlanBusinessRules, IMarketingVisitPlanReadRepository marketingVisitPlanReadRepository)
        {
            _mapper = mapper;
            _marketingVisitPlanWriteRepository = marketingVisitPlanWriteRepository;
            _marketingVisitPlanBusinessRules = marketingVisitPlanBusinessRules;
            _marketingVisitPlanReadRepository = marketingVisitPlanReadRepository;
        }

        public async Task<UpdatedMarketingVisitPlanResponse> Handle(UpdateMarketingVisitPlanCommand request, CancellationToken cancellationToken)
        {
            X.MarketingVisitPlan? marketingVisitPlan = await _marketingVisitPlanReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, include: x => x.Include(x => x.MarketingCustomerFK).Include(x => x.UserFK), cancellationToken: cancellationToken);
            //INCLUDES Buraya Gelecek include varsa eklenecek
            await _marketingVisitPlanBusinessRules.MarketingVisitPlanShouldExistWhenSelected(marketingVisitPlan);
            marketingVisitPlan = _mapper.Map(request, marketingVisitPlan);

            _marketingVisitPlanWriteRepository.Update(marketingVisitPlan!);
            await _marketingVisitPlanWriteRepository.SaveAsync();
            GetByGidMarketingVisitPlanResponse obj = _mapper.Map<GetByGidMarketingVisitPlanResponse>(marketingVisitPlan);

            return new()
            {
                Title = MarketingVisitPlansBusinessMessages.ProcessCompleted,
                Message = MarketingVisitPlansBusinessMessages.SuccessCreatedMarketingVisitPlanMessage,
                IsValid = true,
                Obj = obj
            };
        }
    }
}
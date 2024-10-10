using Application.Features.MarketingManagementFeatures.MarketingVisitPlans.Constants;
using Application.Features.MarketingManagementFeatures.MarketingVisitPlans.Queries.GetByGid;
using Application.Features.MarketingManagementFeatures.MarketingVisitPlans.Rules;
using Application.Repositories.MarketingManagementsRepos.MerketingVisitPlanRepo;
using AutoMapper;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using X = Domain.Entities.MarketingManagements;

namespace Application.Features.MarketingManagementFeatures.MarketingVisitPlans.Commands.Create;

public class CreateMarketingVisitPlanCommand : IRequest<CreatedMarketingVisitPlanResponse>
{
    public Guid GidPersonnelFK { get; set; }
    public Guid GidVisitCustomerFK { get; set; }

    public string Title { get; set; }
    public DateTime PlanningVisitDate { get; set; }
    public string? Description { get; set; }
    public EnumVisitStatus VisitStatus { get; set; }
    public DateTime? VisitDate { get; set; }
    public int? VisitRank { get; set; }
    public string? VisitNote { get; set; }



    public class CreateMarketingVisitPlanCommandHandler : IRequestHandler<CreateMarketingVisitPlanCommand, CreatedMarketingVisitPlanResponse>
    {
        private readonly IMapper _mapper;
        private readonly IMarketingVisitPlanWriteRepository _marketingVisitPlanWriteRepository;
        private readonly IMarketingVisitPlanReadRepository _marketingVisitPlanReadRepository;
        private readonly MarketingVisitPlanBusinessRules _marketingVisitPlanBusinessRules;

        public CreateMarketingVisitPlanCommandHandler(IMapper mapper, IMarketingVisitPlanWriteRepository marketingVisitPlanWriteRepository,
                                         MarketingVisitPlanBusinessRules marketingVisitPlanBusinessRules, IMarketingVisitPlanReadRepository marketingVisitPlanReadRepository)
        {
            _mapper = mapper;
            _marketingVisitPlanWriteRepository = marketingVisitPlanWriteRepository;
            _marketingVisitPlanBusinessRules = marketingVisitPlanBusinessRules;
            _marketingVisitPlanReadRepository = marketingVisitPlanReadRepository;
        }

        public async Task<CreatedMarketingVisitPlanResponse> Handle(CreateMarketingVisitPlanCommand request, CancellationToken cancellationToken)
        {
            //int maxRowNo = await _marketingVisitPlanReadRepository.GetAll().MaxAsync(r => r.RowNo);
            X.MarketingVisitPlan marketingVisitPlan = _mapper.Map<X.MarketingVisitPlan>(request);
            //marketingVisitPlan.RowNo = maxRowNo + 1;

            await _marketingVisitPlanWriteRepository.AddAsync(marketingVisitPlan);
            await _marketingVisitPlanWriteRepository.SaveAsync();

            X.MarketingVisitPlan savedMarketingVisitPlan = await _marketingVisitPlanReadRepository.GetAsync(predicate: x => x.Gid == marketingVisitPlan.Gid, include: x => x.Include(x => x.MarketingCustomerFK).Include(x => x.UserFK));
            //INCLUDES Buraya Gelecek include varsa eklenecek
            //include: x => x.Include(x => x.UserFK));

            GetByGidMarketingVisitPlanResponse obj = _mapper.Map<GetByGidMarketingVisitPlanResponse>(savedMarketingVisitPlan);
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
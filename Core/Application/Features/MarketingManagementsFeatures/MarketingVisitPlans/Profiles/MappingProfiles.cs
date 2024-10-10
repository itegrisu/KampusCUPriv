using Application.Features.MarketingManagementFeatures.MarketingVisitPlans.Commands.Create;
using Application.Features.MarketingManagementFeatures.MarketingVisitPlans.Commands.Delete;
using Application.Features.MarketingManagementFeatures.MarketingVisitPlans.Commands.Update;
using Application.Features.MarketingManagementFeatures.MarketingVisitPlans.Queries.GetByGid;
using Application.Features.MarketingManagementFeatures.MarketingVisitPlans.Queries.GetList;
using Application.Features.MarketingManagementsFeatures.MarketingVisitPlans.Queries.GetByUserGid;
using Application.Features.MarketingManagementsFeatures.MarketingVisitPlans.Queries.GetByYearList;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using X = Domain.Entities.MarketingManagements;

namespace Application.Features.MarketingManagementFeatures.MarketingVisitPlans.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<X.MarketingVisitPlan, CreateMarketingVisitPlanCommand>().ReverseMap();
        CreateMap<X.MarketingVisitPlan, CreatedMarketingVisitPlanResponse>().ReverseMap();
        CreateMap<X.MarketingVisitPlan, UpdateMarketingVisitPlanCommand>().ReverseMap();
        CreateMap<X.MarketingVisitPlan, UpdatedMarketingVisitPlanResponse>().ReverseMap();
        CreateMap<X.MarketingVisitPlan, DeleteMarketingVisitPlanCommand>().ReverseMap();
        CreateMap<X.MarketingVisitPlan, DeletedMarketingVisitPlanResponse>().ReverseMap();

        CreateMap<X.MarketingVisitPlan, GetByGidMarketingVisitPlanResponse>().ReverseMap();

        CreateMap<X.MarketingVisitPlan, GetListMarketingVisitPlanListItemDto>().ReverseMap();
        CreateMap<IPaginate<X.MarketingVisitPlan>, GetListResponse<GetListMarketingVisitPlanListItemDto>>().ReverseMap();

        CreateMap<X.MarketingVisitPlan, GetByYearListMarketingVisitPlanListItemDto>().ReverseMap();
        CreateMap<IPaginate<X.MarketingVisitPlan>, GetListResponse<GetByYearListMarketingVisitPlanListItemDto>>().ReverseMap();

        CreateMap<X.MarketingVisitPlan, GetByUserGidListMarketingVisitPlanListItemDto>().ReverseMap();
        CreateMap<IPaginate<X.MarketingVisitPlan>, GetListResponse<GetByUserGidListMarketingVisitPlanListItemDto>>().ReverseMap();


    }
}
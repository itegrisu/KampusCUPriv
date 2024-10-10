using Application.Features.MarketingManagementFeatures.MarketingCustomers.Commands.Create;
using Application.Features.MarketingManagementFeatures.MarketingCustomers.Commands.Delete;
using Application.Features.MarketingManagementFeatures.MarketingCustomers.Commands.Update;
using Application.Features.MarketingManagementFeatures.MarketingCustomers.Queries.GetByGid;
using Application.Features.MarketingManagementFeatures.MarketingCustomers.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using X = Domain.Entities.MarketingManagements;

namespace Application.Features.MarketingManagementFeatures.MarketingCustomers.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<X.MarketingCustomer, CreateMarketingCustomerCommand>().ReverseMap();
        CreateMap<X.MarketingCustomer, CreatedMarketingCustomerResponse>().ReverseMap();
        CreateMap<X.MarketingCustomer, UpdateMarketingCustomerCommand>().ReverseMap();
        CreateMap<X.MarketingCustomer, UpdatedMarketingCustomerResponse>().ReverseMap();
        CreateMap<X.MarketingCustomer, DeleteMarketingCustomerCommand>().ReverseMap();
        CreateMap<X.MarketingCustomer, DeletedMarketingCustomerResponse>().ReverseMap();

		CreateMap<X.MarketingCustomer, GetByGidMarketingCustomerResponse>().ReverseMap();

        CreateMap<X.MarketingCustomer, GetListMarketingCustomerListItemDto>().ReverseMap();
        CreateMap<IPaginate<X.MarketingCustomer>, GetListResponse<GetListMarketingCustomerListItemDto>>().ReverseMap();
    }
}
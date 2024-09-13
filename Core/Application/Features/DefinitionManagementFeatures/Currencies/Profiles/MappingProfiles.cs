using Application.Features.DefinitionManagementFeatures.Currencies.Commands.Create;
using Application.Features.DefinitionManagementFeatures.Currencies.Commands.Delete;
using Application.Features.DefinitionManagementFeatures.Currencies.Commands.Update;
using Application.Features.DefinitionManagementFeatures.Currencies.Queries.GetByGid;
using Application.Features.DefinitionManagementFeatures.Currencies.Queries.GetById;
using Application.Features.DefinitionManagementFeatures.Currencies.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using X = Domain.Entities.DefinitionManagements;

namespace Application.Features.DefinitionManagementFeatures.Currencies.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<X.Currency, CreateCurrencyCommand>().ReverseMap();
        CreateMap<X.Currency, CreatedCurrencyResponse>().ReverseMap();
        CreateMap<X.Currency, UpdateCurrencyCommand>().ReverseMap();
        CreateMap<X.Currency, UpdatedCurrencyResponse>().ReverseMap();
        CreateMap<X.Currency, DeleteCurrencyCommand>().ReverseMap();
        CreateMap<X.Currency, DeletedCurrencyResponse>().ReverseMap();

		CreateMap<X.Currency, GetByGidCurrencyResponse>().ReverseMap();

        CreateMap<X.Currency, GetListCurrencyListItemDto>().ReverseMap();
        CreateMap<IPaginate<X.Currency>, GetListResponse<GetListCurrencyListItemDto>>().ReverseMap();
    }
}
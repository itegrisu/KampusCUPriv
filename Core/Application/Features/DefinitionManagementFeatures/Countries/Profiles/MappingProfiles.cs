using Application.Features.DefinitionManagementFeatures.Countries.Commands.Create;
using Application.Features.DefinitionManagementFeatures.Countries.Commands.Delete;
using Application.Features.DefinitionManagementFeatures.Countries.Commands.Update;
using Application.Features.DefinitionManagementFeatures.Countries.Queries.GetByGid;
using Application.Features.DefinitionManagementFeatures.Countries.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using X = Domain.Entities.DefinitionManagements;

namespace Application.Features.DefinitionManagementFeatures.Countries.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<X.Country, CreateCountryCommand>().ReverseMap();
        CreateMap<X.Country, CreatedCountryResponse>().ReverseMap();
        CreateMap<X.Country, UpdateCountryCommand>().ReverseMap();
        CreateMap<X.Country, UpdatedCountryResponse>().ReverseMap();
        CreateMap<X.Country, DeleteCountryCommand>().ReverseMap();
        CreateMap<X.Country, DeletedCountryResponse>().ReverseMap();

        CreateMap<X.Country, GetByGidCountryResponse>().ReverseMap();

        CreateMap<X.Country, GetListCountryListItemDto>().ReverseMap();
        CreateMap<IPaginate<X.Country>, GetListResponse<GetListCountryListItemDto>>().ReverseMap();
    }
}
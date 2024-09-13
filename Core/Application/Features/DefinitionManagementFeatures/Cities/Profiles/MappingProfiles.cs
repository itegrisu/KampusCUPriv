using Application.Features.DefinitionManagementFeatures.Cities.Commands.Create;
using Application.Features.DefinitionManagementFeatures.Cities.Commands.Delete;
using Application.Features.DefinitionManagementFeatures.Cities.Commands.Update;
using Application.Features.DefinitionManagementFeatures.Cities.Queries.GetByGid;
using Application.Features.DefinitionManagementFeatures.Cities.Queries.GetById;
using Application.Features.DefinitionManagementFeatures.Cities.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using X = Domain.Entities.DefinitionManagements;

namespace Application.Features.DefinitionManagementFeatures.Cities.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<X.City, CreateCityCommand>().ReverseMap();
        CreateMap<X.City, CreatedCityResponse>().ReverseMap();
        CreateMap<X.City, UpdateCityCommand>().ReverseMap();
        CreateMap<X.City, UpdatedCityResponse>().ReverseMap();
        CreateMap<X.City, DeleteCityCommand>().ReverseMap();
        CreateMap<X.City, DeletedCityResponse>().ReverseMap();

		CreateMap<X.City, GetByGidCityResponse>().ReverseMap();

        CreateMap<X.City, GetListCityListItemDto>().ReverseMap();
        CreateMap<IPaginate<X.City>, GetListResponse<GetListCityListItemDto>>().ReverseMap();
    }
}
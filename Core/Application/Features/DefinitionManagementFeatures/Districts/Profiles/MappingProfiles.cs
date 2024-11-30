using Application.Features.DefinitionManagementFeatures.Districts.Commands.Create;
using Application.Features.DefinitionManagementFeatures.Districts.Commands.Delete;
using Application.Features.DefinitionManagementFeatures.Districts.Commands.Update;
using Application.Features.DefinitionManagementFeatures.Districts.Queries.GetByCityGid;
using Application.Features.DefinitionManagementFeatures.Districts.Queries.GetByGid;
using Application.Features.DefinitionManagementFeatures.Districts.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using X = Domain.Entities.DefinitionManagements;

namespace Application.Features.DefinitionManagementFeatures.Districts.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<X.District, CreateDistrictCommand>().ReverseMap();
        CreateMap<X.District, CreatedDistrictResponse>().ReverseMap();
        CreateMap<X.District, UpdateDistrictCommand>().ReverseMap();
        CreateMap<X.District, UpdatedDistrictResponse>().ReverseMap();
        CreateMap<X.District, DeleteDistrictCommand>().ReverseMap();
        CreateMap<X.District, DeletedDistrictResponse>().ReverseMap();

		CreateMap<X.District, GetByGidDistrictResponse>().ReverseMap();

        CreateMap<X.District, GetListDistrictListItemDto>().ReverseMap();
        CreateMap<IPaginate<X.District>, GetListResponse<GetListDistrictListItemDto>>().ReverseMap();

        CreateMap<X.District, GetByCityGidListDistrictListItemDto>().ReverseMap();
        CreateMap<IPaginate<X.District>, GetListResponse<GetByCityGidListDistrictListItemDto>>().ReverseMap();
    }
}
using Application.Features.DefinitionManagementFeatures.TyreTypes.Commands.Create;
using Application.Features.DefinitionManagementFeatures.TyreTypes.Commands.Delete;
using Application.Features.DefinitionManagementFeatures.TyreTypes.Commands.Update;
using Application.Features.DefinitionManagementFeatures.TyreTypes.Queries.GetByGid;
using Application.Features.DefinitionManagementFeatures.TyreTypes.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using X = Domain.Entities.DefinitionManagements;

namespace Application.Features.DefinitionManagementFeatures.TyreTypes.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<X.TyreType, CreateTyreTypeCommand>().ReverseMap();
        CreateMap<X.TyreType, CreatedTyreTypeResponse>().ReverseMap();
        CreateMap<X.TyreType, UpdateTyreTypeCommand>().ReverseMap();
        CreateMap<X.TyreType, UpdatedTyreTypeResponse>().ReverseMap();
        CreateMap<X.TyreType, DeleteTyreTypeCommand>().ReverseMap();
        CreateMap<X.TyreType, DeletedTyreTypeResponse>().ReverseMap();

		CreateMap<X.TyreType, GetByGidTyreTypeResponse>().ReverseMap();

        CreateMap<X.TyreType, GetListTyreTypeListItemDto>().ReverseMap();
        CreateMap<IPaginate<X.TyreType>, GetListResponse<GetListTyreTypeListItemDto>>().ReverseMap();
    }
}
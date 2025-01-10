using Application.Features.DefinitionFeatures.Classes.Commands.Create;
using Application.Features.DefinitionFeatures.Classes.Commands.Delete;
using Application.Features.DefinitionFeatures.Classes.Commands.Update;
using Application.Features.DefinitionFeatures.Classes.Queries.GetByGid;
using Application.Features.DefinitionFeatures.Classes.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using X = Domain.Entities.DefinitionManagements;

namespace Application.Features.DefinitionFeatures.Classes.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<X.Class, CreateClassCommand>().ReverseMap();
        CreateMap<X.Class, CreatedClassResponse>().ReverseMap();
        CreateMap<X.Class, UpdateClassCommand>().ReverseMap();
        CreateMap<X.Class, UpdatedClassResponse>().ReverseMap();
        CreateMap<X.Class, DeleteClassCommand>().ReverseMap();
        CreateMap<X.Class, DeletedClassResponse>().ReverseMap();

		CreateMap<X.Class, GetByGidClassResponse>().ReverseMap();

        CreateMap<X.Class, GetListClassListItemDto>().ReverseMap();
        CreateMap<IPaginate<X.Class>, GetListResponse<GetListClassListItemDto>>().ReverseMap();
    }
}
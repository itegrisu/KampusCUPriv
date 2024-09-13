using Application.Features.DefinitionManagementFeatures.RoomTypes.Commands.Create;
using Application.Features.DefinitionManagementFeatures.RoomTypes.Commands.Delete;
using Application.Features.DefinitionManagementFeatures.RoomTypes.Commands.Update;
using Application.Features.DefinitionManagementFeatures.RoomTypes.Queries.GetByGid;
using Application.Features.DefinitionManagementFeatures.RoomTypes.Queries.GetById;
using Application.Features.DefinitionManagementFeatures.RoomTypes.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using X = Domain.Entities.DefinitionManagements;

namespace Application.Features.DefinitionManagementFeatures.RoomTypes.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<X.RoomType, CreateRoomTypeCommand>().ReverseMap();
        CreateMap<X.RoomType, CreatedRoomTypeResponse>().ReverseMap();
        CreateMap<X.RoomType, UpdateRoomTypeCommand>().ReverseMap();
        CreateMap<X.RoomType, UpdatedRoomTypeResponse>().ReverseMap();
        CreateMap<X.RoomType, DeleteRoomTypeCommand>().ReverseMap();
        CreateMap<X.RoomType, DeletedRoomTypeResponse>().ReverseMap();

		CreateMap<X.RoomType, GetByGidRoomTypeResponse>().ReverseMap();

        CreateMap<X.RoomType, GetListRoomTypeListItemDto>().ReverseMap();
        CreateMap<IPaginate<X.RoomType>, GetListResponse<GetListRoomTypeListItemDto>>().ReverseMap();
    }
}
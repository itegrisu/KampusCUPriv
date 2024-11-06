using Application.Features.VehicleManagementFeatures.VehicleAlls.Commands.Create;
using Application.Features.VehicleManagementFeatures.VehicleAlls.Commands.Delete;
using Application.Features.VehicleManagementFeatures.VehicleAlls.Commands.Update;
using Application.Features.VehicleManagementFeatures.VehicleAlls.Queries.GetByGid;
using Application.Features.VehicleManagementFeatures.VehicleAlls.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using X = Domain.Entities.VehicleManagements;

namespace Application.Features.VehicleManagementFeatures.VehicleAlls.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<X.VehicleAll, CreateVehicleAllCommand>().ReverseMap();
        CreateMap<X.VehicleAll, CreatedVehicleAllResponse>().ReverseMap();
        CreateMap<X.VehicleAll, UpdateVehicleAllCommand>().ReverseMap();
        CreateMap<X.VehicleAll, UpdatedVehicleAllResponse>().ReverseMap();
        CreateMap<X.VehicleAll, DeleteVehicleAllCommand>().ReverseMap();
        CreateMap<X.VehicleAll, DeletedVehicleAllResponse>().ReverseMap();

		CreateMap<X.VehicleAll, GetByGidVehicleAllResponse>().ReverseMap();

        CreateMap<X.VehicleAll, GetListVehicleAllListItemDto>().ReverseMap();
        CreateMap<IPaginate<X.VehicleAll>, GetListResponse<GetListVehicleAllListItemDto>>().ReverseMap();
    }
}
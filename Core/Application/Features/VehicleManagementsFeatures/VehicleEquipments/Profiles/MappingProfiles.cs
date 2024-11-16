using Application.Features.VehicleManagementFeatures.VehicleEquipments.Commands.Create;
using Application.Features.VehicleManagementFeatures.VehicleEquipments.Commands.Delete;
using Application.Features.VehicleManagementFeatures.VehicleEquipments.Commands.Update;
using Application.Features.VehicleManagementFeatures.VehicleEquipments.Queries.GetByGid;
using Application.Features.VehicleManagementFeatures.VehicleEquipments.Queries.GetList;
using Application.Features.VehicleManagementsFeatures.VehicleEquipments.Queries.GetByVehicleGid;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using X = Domain.Entities.VehicleManagements;

namespace Application.Features.VehicleManagementFeatures.VehicleEquipments.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<X.VehicleEquipment, CreateVehicleEquipmentCommand>().ReverseMap();
        CreateMap<X.VehicleEquipment, CreatedVehicleEquipmentResponse>().ReverseMap();
        CreateMap<X.VehicleEquipment, UpdateVehicleEquipmentCommand>().ReverseMap();
        CreateMap<X.VehicleEquipment, UpdatedVehicleEquipmentResponse>().ReverseMap();
        CreateMap<X.VehicleEquipment, DeleteVehicleEquipmentCommand>().ReverseMap();
        CreateMap<X.VehicleEquipment, DeletedVehicleEquipmentResponse>().ReverseMap();

		CreateMap<X.VehicleEquipment, GetByGidVehicleEquipmentResponse>().ReverseMap();

        CreateMap<X.VehicleEquipment, GetListVehicleEquipmentListItemDto>().ReverseMap();
        CreateMap<IPaginate<X.VehicleEquipment>, GetListResponse<GetListVehicleEquipmentListItemDto>>().ReverseMap();

        CreateMap<X.VehicleEquipment, GetByVehicleGidListVehicleEquipmentListItemDto>().ReverseMap();
        CreateMap<IPaginate<X.VehicleEquipment>, GetListResponse<GetByVehicleGidListVehicleEquipmentListItemDto>>().ReverseMap();
    }
}
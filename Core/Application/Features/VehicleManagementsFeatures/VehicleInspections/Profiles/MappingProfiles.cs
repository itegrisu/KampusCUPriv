using Application.Features.VehicleManagementFeatures.VehicleInspections.Commands.Create;
using Application.Features.VehicleManagementFeatures.VehicleInspections.Commands.Delete;
using Application.Features.VehicleManagementFeatures.VehicleInspections.Commands.Update;
using Application.Features.VehicleManagementFeatures.VehicleInspections.Queries.GetByGid;
using Application.Features.VehicleManagementFeatures.VehicleInspections.Queries.GetList;
using Application.Features.VehicleManagementsFeatures.VehicleInspections.Queries.GetByVehicleGid;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using X = Domain.Entities.VehicleManagements;

namespace Application.Features.VehicleManagementFeatures.VehicleInspections.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<X.VehicleInspection, CreateVehicleInspectionCommand>().ReverseMap();
        CreateMap<X.VehicleInspection, CreatedVehicleInspectionResponse>().ReverseMap();
        CreateMap<X.VehicleInspection, UpdateVehicleInspectionCommand>().ReverseMap();
        CreateMap<X.VehicleInspection, UpdatedVehicleInspectionResponse>().ReverseMap();
        CreateMap<X.VehicleInspection, DeleteVehicleInspectionCommand>().ReverseMap();
        CreateMap<X.VehicleInspection, DeletedVehicleInspectionResponse>().ReverseMap();

		CreateMap<X.VehicleInspection, GetByGidVehicleInspectionResponse>().ReverseMap();

        CreateMap<X.VehicleInspection, GetListVehicleInspectionListItemDto>().ReverseMap();
        CreateMap<IPaginate<X.VehicleInspection>, GetListResponse<GetListVehicleInspectionListItemDto>>().ReverseMap();

        CreateMap<X.VehicleInspection, GetByVehicleGidListVehicleInspectionListItemDto>().ReverseMap();
        CreateMap<IPaginate<X.VehicleInspection>, GetListResponse<GetByVehicleGidListVehicleInspectionListItemDto>>().ReverseMap();
    }
}
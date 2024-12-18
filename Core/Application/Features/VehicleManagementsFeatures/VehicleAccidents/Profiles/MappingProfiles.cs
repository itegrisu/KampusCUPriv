using Application.Features.VehicleManagementFeatures.VehicleAccidents.Commands.Create;
using Application.Features.VehicleManagementFeatures.VehicleAccidents.Commands.Delete;
using Application.Features.VehicleManagementFeatures.VehicleAccidents.Commands.Update;
using Application.Features.VehicleManagementFeatures.VehicleAccidents.Queries.GetByGid;
using Application.Features.VehicleManagementFeatures.VehicleAccidents.Queries.GetList;
using Application.Features.VehicleManagementsFeatures.VehicleAccidents.Queries.GetByUserGid;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using X = Domain.Entities.VehicleManagements;

namespace Application.Features.VehicleManagementFeatures.VehicleAccidents.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<X.VehicleAccident, CreateVehicleAccidentCommand>().ReverseMap();
        CreateMap<X.VehicleAccident, CreatedVehicleAccidentResponse>().ReverseMap();
        CreateMap<X.VehicleAccident, UpdateVehicleAccidentCommand>().ReverseMap();
        CreateMap<X.VehicleAccident, UpdatedVehicleAccidentResponse>().ReverseMap();
        CreateMap<X.VehicleAccident, DeleteVehicleAccidentCommand>().ReverseMap();
        CreateMap<X.VehicleAccident, DeletedVehicleAccidentResponse>().ReverseMap();

		CreateMap<X.VehicleAccident, GetByGidVehicleAccidentResponse>().ReverseMap();

        CreateMap<X.VehicleAccident, GetListVehicleAccidentListItemDto>().ReverseMap();
        CreateMap<IPaginate<X.VehicleAccident>, GetListResponse<GetListVehicleAccidentListItemDto>>().ReverseMap();

        CreateMap<X.VehicleAccident, GetByVehicleGidListVehicleAccidentListItemDto>().ReverseMap();
        CreateMap<IPaginate<X.VehicleAccident>, GetListResponse<GetByVehicleGidListVehicleAccidentListItemDto>>().ReverseMap();
    }
}
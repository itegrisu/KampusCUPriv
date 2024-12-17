using Application.Features.VehicleManagementFeatures.VehicleRequests.Commands.Create;
using Application.Features.VehicleManagementFeatures.VehicleRequests.Commands.Delete;
using Application.Features.VehicleManagementFeatures.VehicleRequests.Commands.Update;
using Application.Features.VehicleManagementFeatures.VehicleRequests.Queries.GetByGid;
using Application.Features.VehicleManagementFeatures.VehicleRequests.Queries.GetList;
using Application.Features.VehicleManagementsFeatures.VehicleRequests.Queries.GetByUserGid;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using X = Domain.Entities.VehicleManagements;

namespace Application.Features.VehicleManagementFeatures.VehicleRequests.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<X.VehicleRequest, CreateVehicleRequestCommand>().ReverseMap();
        CreateMap<X.VehicleRequest, CreatedVehicleRequestResponse>().ReverseMap();
        CreateMap<X.VehicleRequest, UpdateVehicleRequestCommand>().ReverseMap();
        CreateMap<X.VehicleRequest, UpdatedVehicleRequestResponse>().ReverseMap();
        CreateMap<X.VehicleRequest, DeleteVehicleRequestCommand>().ReverseMap();
        CreateMap<X.VehicleRequest, DeletedVehicleRequestResponse>().ReverseMap();

		CreateMap<X.VehicleRequest, GetByGidVehicleRequestResponse>().ReverseMap();

        CreateMap<X.VehicleRequest, GetListVehicleRequestListItemDto>().ReverseMap();
        CreateMap<IPaginate<X.VehicleRequest>, GetListResponse<GetListVehicleRequestListItemDto>>().ReverseMap();

        CreateMap<X.VehicleRequest, GetByUserGidListVehicleRequestListItemDto>().ReverseMap();
        CreateMap<IPaginate<X.VehicleRequest>, GetListResponse<GetByUserGidListVehicleRequestListItemDto>>().ReverseMap();
    }
}
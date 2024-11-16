using Application.Features.VehicleManagementFeatures.VehicleInsurances.Commands.Create;
using Application.Features.VehicleManagementFeatures.VehicleInsurances.Commands.Delete;
using Application.Features.VehicleManagementFeatures.VehicleInsurances.Commands.Update;
using Application.Features.VehicleManagementFeatures.VehicleInsurances.Queries.GetByGid;
using Application.Features.VehicleManagementFeatures.VehicleInsurances.Queries.GetList;
using Application.Features.VehicleManagementsFeatures.VehicleInsurances.Queries.GetByVehicleGid;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using X = Domain.Entities.VehicleManagements;

namespace Application.Features.VehicleManagementFeatures.VehicleInsurances.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<X.VehicleInsurance, CreateVehicleInsuranceCommand>().ReverseMap();
        CreateMap<X.VehicleInsurance, CreatedVehicleInsuranceResponse>().ReverseMap();
        CreateMap<X.VehicleInsurance, UpdateVehicleInsuranceCommand>().ReverseMap();
        CreateMap<X.VehicleInsurance, UpdatedVehicleInsuranceResponse>().ReverseMap();
        CreateMap<X.VehicleInsurance, DeleteVehicleInsuranceCommand>().ReverseMap();
        CreateMap<X.VehicleInsurance, DeletedVehicleInsuranceResponse>().ReverseMap();

		CreateMap<X.VehicleInsurance, GetByGidVehicleInsuranceResponse>().ReverseMap();

        CreateMap<X.VehicleInsurance, GetListVehicleInsuranceListItemDto>().ReverseMap();
        CreateMap<IPaginate<X.VehicleInsurance>, GetListResponse<GetListVehicleInsuranceListItemDto>>().ReverseMap();

        CreateMap<X.VehicleInsurance, GetByVehicleGidListVehicleInsuranceListItemDto>().ReverseMap();
        CreateMap<IPaginate<X.VehicleInsurance>, GetListResponse<GetByVehicleGidListVehicleInsuranceListItemDto>>().ReverseMap();
    }
}
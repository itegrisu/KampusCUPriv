using Application.Features.VehicleManagementFeatures.VehicleTransactions.Commands.Create;
using Application.Features.VehicleManagementFeatures.VehicleTransactions.Commands.Delete;
using Application.Features.VehicleManagementFeatures.VehicleTransactions.Commands.Update;
using Application.Features.VehicleManagementFeatures.VehicleTransactions.Queries.GetByGid;
using Application.Features.VehicleManagementFeatures.VehicleTransactions.Queries.GetList;
using Application.Features.VehicleManagementsFeatures.VehicleTransactions.Queries.GetByVehicleTransactionGid;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using X = Domain.Entities.VehicleManagements;

namespace Application.Features.VehicleManagementFeatures.VehicleTransactions.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<X.VehicleTransaction, CreateVehicleTransactionCommand>().ReverseMap();
        CreateMap<X.VehicleTransaction, CreatedVehicleTransactionResponse>().ReverseMap();
        CreateMap<X.VehicleTransaction, UpdateVehicleTransactionCommand>().ReverseMap();
        CreateMap<X.VehicleTransaction, UpdatedVehicleTransactionResponse>().ReverseMap();
        CreateMap<X.VehicleTransaction, DeleteVehicleTransactionCommand>().ReverseMap();
        CreateMap<X.VehicleTransaction, DeletedVehicleTransactionResponse>().ReverseMap();

		CreateMap<X.VehicleTransaction, GetByGidVehicleTransactionResponse>().ReverseMap();

        CreateMap<X.VehicleTransaction, GetListVehicleTransactionListItemDto>().ReverseMap();
        CreateMap<IPaginate<X.VehicleTransaction>, GetListResponse<GetListVehicleTransactionListItemDto>>().ReverseMap();

        CreateMap<X.VehicleTransaction, GetByVehicleTransactionGidListVehicleTransactionListItemDto>().ReverseMap();
        CreateMap<IPaginate<X.VehicleTransaction>, GetListResponse<GetByVehicleTransactionGidListVehicleTransactionListItemDto>>().ReverseMap();
    }
}
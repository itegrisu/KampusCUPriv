using Application.Features.VehicleManagementFeatures.VehicleDocuments.Commands.Create;
using Application.Features.VehicleManagementFeatures.VehicleDocuments.Commands.Delete;
using Application.Features.VehicleManagementFeatures.VehicleDocuments.Commands.Update;
using Application.Features.VehicleManagementFeatures.VehicleDocuments.Queries.GetByGid;
using Application.Features.VehicleManagementFeatures.VehicleDocuments.Queries.GetList;
using Application.Features.VehicleManagementsFeatures.VehicleDocuments.Queries.GetByVehicleGid;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using X = Domain.Entities.VehicleManagements;

namespace Application.Features.VehicleManagementFeatures.VehicleDocuments.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<X.VehicleDocument, CreateVehicleDocumentCommand>().ReverseMap();
        CreateMap<X.VehicleDocument, CreatedVehicleDocumentResponse>().ReverseMap();
        CreateMap<X.VehicleDocument, UpdateVehicleDocumentCommand>().ReverseMap();
        CreateMap<X.VehicleDocument, UpdatedVehicleDocumentResponse>().ReverseMap();
        CreateMap<X.VehicleDocument, DeleteVehicleDocumentCommand>().ReverseMap();
        CreateMap<X.VehicleDocument, DeletedVehicleDocumentResponse>().ReverseMap();

		CreateMap<X.VehicleDocument, GetByGidVehicleDocumentResponse>().ReverseMap();

        CreateMap<X.VehicleDocument, GetListVehicleDocumentListItemDto>().ReverseMap();
        CreateMap<IPaginate<X.VehicleDocument>, GetListResponse<GetListVehicleDocumentListItemDto>>().ReverseMap();

        CreateMap<X.VehicleDocument, GetByVehicleGidListVehicleDocumentListItemDto>().ReverseMap();
        CreateMap<IPaginate<X.VehicleDocument>, GetListResponse<GetByVehicleGidListVehicleDocumentListItemDto>>().ReverseMap();
    }
}
using Application.Features.TransportationManagementFeatures.TransportationServices.Commands.Create;
using Application.Features.TransportationManagementFeatures.TransportationServices.Commands.Delete;
using Application.Features.TransportationManagementFeatures.TransportationServices.Commands.Update;
using Application.Features.TransportationManagementFeatures.TransportationServices.Queries.GetByGid;
using Application.Features.TransportationManagementFeatures.TransportationServices.Queries.GetList;
using Application.Features.TransportationManagementsFeatures.TransportationServices.Commands.CancelReport;
using Application.Features.TransportationManagementsFeatures.TransportationServices.Commands.CreateServiceWithGroup;
using Application.Features.TransportationManagementsFeatures.TransportationServices.Commands.Print;
using Application.Features.TransportationManagementsFeatures.TransportationServices.Commands.ReportTransportationService;
using Application.Features.TransportationManagementsFeatures.TransportationServices.Commands.UpdateServiceWithGroup;
using Application.Features.TransportationManagementsFeatures.TransportationServices.Queries.GetByTransportationGid;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using X = Domain.Entities.TransportationManagements;

namespace Application.Features.TransportationManagementFeatures.TransportationServices.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<X.TransportationService, CreateTransportationServiceCommand>().ReverseMap();
        CreateMap<X.TransportationService, CreatedTransportationServiceResponse>().ReverseMap();
        CreateMap<X.TransportationService, UpdateTransportationServiceCommand>().ReverseMap();
        CreateMap<X.TransportationService, UpdatedTransportationServiceResponse>().ReverseMap();
        CreateMap<X.TransportationService, DeleteTransportationServiceCommand>().ReverseMap();
        CreateMap<X.TransportationService, DeletedTransportationServiceResponse>().ReverseMap();

		CreateMap<X.TransportationService, GetByGidTransportationServiceResponse>().ReverseMap();

        CreateMap<X.TransportationService, GetListTransportationServiceListItemDto>().ReverseMap();
        CreateMap<IPaginate<X.TransportationService>, GetListResponse<GetListTransportationServiceListItemDto>>().ReverseMap();

        CreateMap<X.TransportationService, GetByTransportationGidListTransportationServiceListItemDto>().ReverseMap();
        CreateMap<IPaginate<X.TransportationService>, GetListResponse<GetByTransportationGidListTransportationServiceListItemDto>>().ReverseMap();

        CreateMap<X.TransportationService, CreateTransportationServiceWithGroupCommand>().ReverseMap();
        CreateMap<X.TransportationService, CreatedTransportationServiceWithGroupResponse>().ReverseMap();

        CreateMap<X.TransportationService, UpdateServiceWithGroupTransportationServiceCommand>().ReverseMap();
        CreateMap<X.TransportationService, UpdatedTransportationServiceResponse>().ReverseMap();

        CreateMap<X.TransportationService, ReportTransportationServiceCommand>().ReverseMap();
        CreateMap<X.TransportationService, ReportedTransportationServiceResponse>().ReverseMap();

        CreateMap<X.TransportationService, CancelTransportationServiceCommand>().ReverseMap();
        CreateMap<X.TransportationService, CancaledTransportationServiceResponse>().ReverseMap();

        CreateMap<X.TransportationService, PrintTransportationServiceCommand>().ReverseMap();
        CreateMap<X.TransportationService, PrintedTransportationServiceResponse>().ReverseMap();
    }
}
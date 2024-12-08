using Application.Features.TransportationManagementFeatures.TransportationPassengers.Commands.Create;
using Application.Features.TransportationManagementFeatures.TransportationPassengers.Commands.Delete;
using Application.Features.TransportationManagementFeatures.TransportationPassengers.Commands.Update;
using Application.Features.TransportationManagementFeatures.TransportationPassengers.Queries.GetByGid;
using Application.Features.TransportationManagementFeatures.TransportationPassengers.Queries.GetList;
using Application.Features.TransportationManagementsFeatures.TransportationPassengers.Commands.CancelReport;
using Application.Features.TransportationManagementsFeatures.TransportationPassengers.Commands.ReportMultiPersonnel;
using Application.Features.TransportationManagementsFeatures.TransportationPassengers.Commands.ReportPersonnel;
using Application.Features.TransportationManagementsFeatures.TransportationPassengers.Queries.GetByGroupGid;
using Application.Features.TransportationManagementsFeatures.TransportationPassengers.Queries.GetByServiceGid;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using X = Domain.Entities.TransportationManagements;

namespace Application.Features.TransportationManagementFeatures.TransportationPassengers.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<X.TransportationPassenger, CreateTransportationPassengerCommand>().ReverseMap();
        CreateMap<X.TransportationPassenger, CreatedTransportationPassengerResponse>().ReverseMap();
        CreateMap<X.TransportationPassenger, UpdateTransportationPassengerCommand>().ReverseMap();
        CreateMap<X.TransportationPassenger, UpdatedTransportationPassengerResponse>().ReverseMap();
        CreateMap<X.TransportationPassenger, DeleteTransportationPassengerCommand>().ReverseMap();
        CreateMap<X.TransportationPassenger, DeletedTransportationPassengerResponse>().ReverseMap();

		CreateMap<X.TransportationPassenger, GetByGidTransportationPassengerResponse>().ReverseMap();

        CreateMap<X.TransportationPassenger, GetListTransportationPassengerListItemDto>().ReverseMap();
        CreateMap<IPaginate<X.TransportationPassenger>, GetListResponse<GetListTransportationPassengerListItemDto>>().ReverseMap();

        CreateMap<X.TransportationPassenger, GetByGroupGidListTransportationPassengerListItemDto>().ReverseMap();
        CreateMap<IPaginate<X.TransportationPassenger>, GetListResponse<GetByGroupGidListTransportationPassengerListItemDto>>().ReverseMap();

        CreateMap<X.TransportationPassenger, GetByServiceGidListTransportationPassengerListItemDto>().ReverseMap();
        CreateMap<IPaginate<X.TransportationPassenger>, GetListResponse<GetByServiceGidListTransportationPassengerListItemDto>>().ReverseMap();

        CreateMap<X.TransportationPassenger, ReportTransportationPassengerMultiCommand>().ReverseMap();
        CreateMap<X.TransportationPassenger, ReportedTransportationPassengerMultiResponse>().ReverseMap();

        CreateMap<X.TransportationPassenger, ReportTransportationPassengerCommand>().ReverseMap();
        CreateMap<X.TransportationPassenger, ReportedTransportationPassengerResponse>().ReverseMap();

        CreateMap<X.TransportationPassenger, CancelTransportationPassengerCommand>().ReverseMap();
        CreateMap<X.TransportationPassenger, CanceledTransportationPassengerResponse>().ReverseMap();
    }
}
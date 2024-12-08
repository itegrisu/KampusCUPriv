using Application.Features.GeneralManagementFeatures.Departments.Queries.GetListWithUser;
using Application.Features.TransportationManagementFeatures.TransportationGroups.Commands.Create;
using Application.Features.TransportationManagementFeatures.TransportationGroups.Commands.Delete;
using Application.Features.TransportationManagementFeatures.TransportationGroups.Commands.Update;
using Application.Features.TransportationManagementFeatures.TransportationGroups.Queries.GetByGid;
using Application.Features.TransportationManagementFeatures.TransportationGroups.Queries.GetList;
using Application.Features.TransportationManagementsFeatures.TransportationGroups.Commands.CancelReport;
using Application.Features.TransportationManagementsFeatures.TransportationGroups.Commands.Report;
using Application.Features.TransportationManagementsFeatures.TransportationGroups.Queries.GetByServiceGid;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using X = Domain.Entities.TransportationManagements;

namespace Application.Features.TransportationManagementFeatures.TransportationGroups.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<X.TransportationGroup, CreateTransportationGroupCommand>().ReverseMap();
        CreateMap<X.TransportationGroup, CreatedTransportationGroupResponse>().ReverseMap();
        CreateMap<X.TransportationGroup, UpdateTransportationGroupCommand>().ReverseMap();
        CreateMap<X.TransportationGroup, UpdatedTransportationGroupResponse>().ReverseMap();
        CreateMap<X.TransportationGroup, DeleteTransportationGroupCommand>().ReverseMap();
        CreateMap<X.TransportationGroup, DeletedTransportationGroupResponse>().ReverseMap();

		CreateMap<X.TransportationGroup, GetByGidTransportationGroupResponse>().ReverseMap();

        CreateMap<X.TransportationGroup, GetListTransportationGroupListItemDto>().ReverseMap();
        CreateMap<IPaginate<X.TransportationGroup>, GetListResponse<GetListTransportationGroupListItemDto>>().ReverseMap();

        CreateMap<X.TransportationGroup, GetByServiceGidListTransportationGroupListItemDto>().ForMember(dest => dest.PassengerCount, opt => opt.MapFrom(src => src.TransportationPassengers.Where(t => t.DataState == Core.Enum.DataState.Active).Count())).ReverseMap();
        CreateMap<IPaginate<X.TransportationGroup>, GetListResponse<GetByServiceGidListTransportationGroupListItemDto>>().ReverseMap();

        CreateMap<X.TransportationGroup, ReportTransportationGroupCommand>().ReverseMap();
        CreateMap<X.TransportationGroup, ReportedTransportationGroupResponse>().ReverseMap();

        CreateMap<X.TransportationGroup, CancelTransportationGroupCommand>().ReverseMap();
        CreateMap<X.TransportationGroup, CanceledTransportationGroupResponse>().ReverseMap();
    }
}
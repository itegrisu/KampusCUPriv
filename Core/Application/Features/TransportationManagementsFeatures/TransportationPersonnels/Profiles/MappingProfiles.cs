using Application.Features.TransportationManagementFeatures.TransportationPersonnels.Commands.Create;
using Application.Features.TransportationManagementFeatures.TransportationPersonnels.Commands.Delete;
using Application.Features.TransportationManagementFeatures.TransportationPersonnels.Commands.Update;
using Application.Features.TransportationManagementFeatures.TransportationPersonnels.Queries.GetByGid;
using Application.Features.TransportationManagementFeatures.TransportationPersonnels.Queries.GetList;
using Application.Features.TransportationManagementsFeatures.TransportationPersonnels.Commands.CancelReport;
using Application.Features.TransportationManagementsFeatures.TransportationPersonnels.Commands.Report;
using Application.Features.TransportationManagementsFeatures.TransportationPersonnels.Queries.GetByServiceGid;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using X = Domain.Entities.TransportationManagements;

namespace Application.Features.TransportationManagementFeatures.TransportationPersonnels.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<X.TransportationPersonnel, CreateTransportationPersonnelCommand>().ReverseMap();
        CreateMap<X.TransportationPersonnel, CreatedTransportationPersonnelResponse>().ReverseMap();
        CreateMap<X.TransportationPersonnel, UpdateTransportationPersonnelCommand>().ReverseMap();
        CreateMap<X.TransportationPersonnel, UpdatedTransportationPersonnelResponse>().ReverseMap();
        CreateMap<X.TransportationPersonnel, DeleteTransportationPersonnelCommand>().ReverseMap();
        CreateMap<X.TransportationPersonnel, DeletedTransportationPersonnelResponse>().ReverseMap();

		CreateMap<X.TransportationPersonnel, GetByGidTransportationPersonnelResponse>().ReverseMap();

        CreateMap<X.TransportationPersonnel, GetListTransportationPersonnelListItemDto>().ReverseMap();
        CreateMap<IPaginate<X.TransportationPersonnel>, GetListResponse<GetListTransportationPersonnelListItemDto>>().ReverseMap();

        CreateMap<X.TransportationPersonnel, GetByServiceGidListTransportationPersonnelListItemDto>().ReverseMap();
        CreateMap<IPaginate<X.TransportationPersonnel>, GetListResponse<GetByServiceGidListTransportationPersonnelListItemDto>>().ReverseMap();

        CreateMap<X.TransportationPersonnel, ReportTransportationPersonnelCommand>().ReverseMap();
        CreateMap<X.TransportationPersonnel, ReportedTransportationPersonnelResponse>().ReverseMap();

        CreateMap<X.TransportationPersonnel, CancelTransportationPersonnelCommand>().ReverseMap();
        CreateMap<X.TransportationPersonnel, CanceledTransportationPersonnelResponse>().ReverseMap();
    }
}
using Application.Features.TransportationManagementFeatures.TransportationPersonnels.Commands.Create;
using Application.Features.TransportationManagementFeatures.TransportationPersonnels.Commands.Delete;
using Application.Features.TransportationManagementFeatures.TransportationPersonnels.Commands.Update;
using Application.Features.TransportationManagementFeatures.TransportationPersonnels.Queries.GetByGid;
using Application.Features.TransportationManagementFeatures.TransportationPersonnels.Queries.GetList;
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
    }
}
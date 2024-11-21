using Application.Features.TransportationManagementFeatures.TransportationServices.Commands.Create;
using Application.Features.TransportationManagementFeatures.TransportationServices.Commands.Delete;
using Application.Features.TransportationManagementFeatures.TransportationServices.Commands.Update;
using Application.Features.TransportationManagementFeatures.TransportationServices.Queries.GetByGid;
using Application.Features.TransportationManagementFeatures.TransportationServices.Queries.GetList;
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
    }
}
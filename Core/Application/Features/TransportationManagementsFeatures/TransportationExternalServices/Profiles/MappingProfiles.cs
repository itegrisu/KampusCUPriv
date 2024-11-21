using Application.Features.TransportationManagementFeatures.TransportationExternalServices.Commands.Create;
using Application.Features.TransportationManagementFeatures.TransportationExternalServices.Commands.Delete;
using Application.Features.TransportationManagementFeatures.TransportationExternalServices.Commands.Update;
using Application.Features.TransportationManagementFeatures.TransportationExternalServices.Queries.GetByGid;
using Application.Features.TransportationManagementFeatures.TransportationExternalServices.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using X = Domain.Entities.TransportationManagements;

namespace Application.Features.TransportationManagementFeatures.TransportationExternalServices.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<X.TransportationExternalService, CreateTransportationExternalServiceCommand>().ReverseMap();
        CreateMap<X.TransportationExternalService, CreatedTransportationExternalServiceResponse>().ReverseMap();
        CreateMap<X.TransportationExternalService, UpdateTransportationExternalServiceCommand>().ReverseMap();
        CreateMap<X.TransportationExternalService, UpdatedTransportationExternalServiceResponse>().ReverseMap();
        CreateMap<X.TransportationExternalService, DeleteTransportationExternalServiceCommand>().ReverseMap();
        CreateMap<X.TransportationExternalService, DeletedTransportationExternalServiceResponse>().ReverseMap();

		CreateMap<X.TransportationExternalService, GetByGidTransportationExternalServiceResponse>().ReverseMap();

        CreateMap<X.TransportationExternalService, GetListTransportationExternalServiceListItemDto>().ReverseMap();
        CreateMap<IPaginate<X.TransportationExternalService>, GetListResponse<GetListTransportationExternalServiceListItemDto>>().ReverseMap();
    }
}
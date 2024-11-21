using Application.Features.TransportationManagementFeatures.Transportations.Commands.Create;
using Application.Features.TransportationManagementFeatures.Transportations.Commands.Delete;
using Application.Features.TransportationManagementFeatures.Transportations.Commands.Update;
using Application.Features.TransportationManagementFeatures.Transportations.Queries.GetByGid;
using Application.Features.TransportationManagementFeatures.Transportations.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using X = Domain.Entities.TransportationManagements;

namespace Application.Features.TransportationManagementFeatures.Transportations.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<X.Transportation, CreateTransportationCommand>().ReverseMap();
        CreateMap<X.Transportation, CreatedTransportationResponse>().ReverseMap();
        CreateMap<X.Transportation, UpdateTransportationCommand>().ReverseMap();
        CreateMap<X.Transportation, UpdatedTransportationResponse>().ReverseMap();
        CreateMap<X.Transportation, DeleteTransportationCommand>().ReverseMap();
        CreateMap<X.Transportation, DeletedTransportationResponse>().ReverseMap();

		CreateMap<X.Transportation, GetByGidTransportationResponse>().ReverseMap();

        CreateMap<X.Transportation, GetListTransportationListItemDto>().ReverseMap();
        CreateMap<IPaginate<X.Transportation>, GetListResponse<GetListTransportationListItemDto>>().ReverseMap();
    }
}
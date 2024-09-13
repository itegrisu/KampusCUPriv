using Application.Features.PortalManagementFeatures.PortalParameters.Commands.Create;
using Application.Features.PortalManagementFeatures.PortalParameters.Commands.Delete;
using Application.Features.PortalManagementFeatures.PortalParameters.Commands.Update;
using Application.Features.PortalManagementFeatures.PortalParameters.Queries.GetByGid;
using Application.Features.PortalManagementFeatures.PortalParameters.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using X = Domain.Entities.PortalManagements;

namespace Application.Features.PortalManagementFeatures.PortalParameters.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<X.PortalParameter, CreatePortalParameterCommand>().ReverseMap();
        CreateMap<X.PortalParameter, CreatedPortalParameterResponse>().ReverseMap();
        CreateMap<X.PortalParameter, UpdatePortalParameterCommand>().ReverseMap();
        CreateMap<X.PortalParameter, UpdatedPortalParameterResponse>().ReverseMap();
        CreateMap<X.PortalParameter, DeletePortalParameterCommand>().ReverseMap();
        CreateMap<X.PortalParameter, DeletedPortalParameterResponse>().ReverseMap();

		CreateMap<X.PortalParameter, GetByGidPortalParameterResponse>().ReverseMap();

        CreateMap<X.PortalParameter, GetListPortalParameterListItemDto>().ReverseMap();
        CreateMap<IPaginate<X.PortalParameter>, GetListResponse<GetListPortalParameterListItemDto>>().ReverseMap();
    }
}
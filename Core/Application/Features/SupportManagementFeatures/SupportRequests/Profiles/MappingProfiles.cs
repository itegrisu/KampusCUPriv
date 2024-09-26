using Application.Features.SupportManagementFeatures.SupportRequests.Commands.Create;
using Application.Features.SupportManagementFeatures.SupportRequests.Commands.Delete;
using Application.Features.SupportManagementFeatures.SupportRequests.Commands.Update;
using Application.Features.SupportManagementFeatures.SupportRequests.Queries.GetByGid;
using Application.Features.SupportManagementFeatures.SupportRequests.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using X = Domain.Entities.SupportManagements;

namespace Application.Features.SupportManagementFeatures.SupportRequests.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<X.SupportRequest, CreateSupportRequestCommand>().ReverseMap();
        CreateMap<X.SupportRequest, CreatedSupportRequestResponse>().ReverseMap();
        CreateMap<X.SupportRequest, UpdateSupportRequestCommand>().ReverseMap();
        CreateMap<X.SupportRequest, UpdatedSupportRequestResponse>().ReverseMap();
        CreateMap<X.SupportRequest, DeleteSupportRequestCommand>().ReverseMap();
        CreateMap<X.SupportRequest, DeletedSupportRequestResponse>().ReverseMap();

		CreateMap<X.SupportRequest, GetByGidSupportRequestResponse>().ReverseMap();

        CreateMap<X.SupportRequest, GetListSupportRequestListItemDto>().ReverseMap();
        CreateMap<IPaginate<X.SupportRequest>, GetListResponse<GetListSupportRequestListItemDto>>().ReverseMap();
    }
}
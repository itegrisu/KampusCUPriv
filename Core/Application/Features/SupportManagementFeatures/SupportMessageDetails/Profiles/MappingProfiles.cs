using Application.Features.SupportManagementFeatures.SupportMessageDetails.Commands.Create;
using Application.Features.SupportManagementFeatures.SupportMessageDetails.Commands.Delete;
using Application.Features.SupportManagementFeatures.SupportMessageDetails.Commands.Update;
using Application.Features.SupportManagementFeatures.SupportMessageDetails.Queries.GetByGid;
using Application.Features.SupportManagementFeatures.SupportMessageDetails.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities.SupportManagements;
using X = Domain.Entities.SupportManagements;

namespace Application.Features.SupportManagementFeatures.SupportMessageDetails.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<X.SupportMessageDetail, CreateSupportMessageDetailCommand>().ReverseMap();
        CreateMap<X.SupportMessageDetail, CreatedSupportMessageDetailResponse>().ReverseMap();
        CreateMap<X.SupportMessageDetail, UpdateSupportMessageDetailCommand>().ReverseMap();
        CreateMap<X.SupportMessageDetail, UpdatedSupportMessageDetailResponse>().ReverseMap();
        CreateMap<X.SupportMessageDetail, DeleteSupportMessageDetailCommand>().ReverseMap();
        CreateMap<X.SupportMessageDetail, DeletedSupportMessageDetailResponse>().ReverseMap();

		CreateMap<X.SupportMessageDetail, GetByGidSupportMessageDetailResponse>().ReverseMap();

        CreateMap<X.SupportMessageDetail, GetListSupportMessageDetailListItemDto>().ReverseMap();
        CreateMap<IPaginate<X.SupportMessageDetail>, GetListResponse<GetListSupportMessageDetailListItemDto>>().ReverseMap();

        
    }
}
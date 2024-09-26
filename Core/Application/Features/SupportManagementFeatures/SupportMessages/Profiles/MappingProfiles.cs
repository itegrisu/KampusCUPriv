using Application.Features.SupportManagementFeatures.SupportMessages.Commands.Create;
using Application.Features.SupportManagementFeatures.SupportMessages.Commands.Delete;
using Application.Features.SupportManagementFeatures.SupportMessages.Commands.Update;
using Application.Features.SupportManagementFeatures.SupportMessages.Queries.GetByGid;
using Application.Features.SupportManagementFeatures.SupportMessages.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities.SupportManagements;
using X = Domain.Entities.SupportManagements;

namespace Application.Features.SupportManagementFeatures.SupportMessages.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<X.SupportMessage, CreateSupportMessageCommand>().ReverseMap();
        CreateMap<X.SupportMessage, CreatedSupportMessageResponse>().ReverseMap();
        CreateMap<X.SupportMessage, UpdateSupportMessageCommand>().ReverseMap();
        CreateMap<X.SupportMessage, UpdatedSupportMessageResponse>().ReverseMap();
        CreateMap<X.SupportMessage, DeleteSupportMessageCommand>().ReverseMap();
        CreateMap<X.SupportMessage, DeletedSupportMessageResponse>().ReverseMap();

		CreateMap<X.SupportMessage, GetByGidSupportMessageResponse>().ReverseMap();

        CreateMap<X.SupportMessage, GetListSupportMessageListItemDto>().ReverseMap();

        CreateMap<SupportMessage, GetListSupportMessageListItemDto>()
           .ForMember(dest => dest.GetByGidSupportMessageDetailResponse, opt => opt.MapFrom(src => src.SupportMessageDetails))
           .ReverseMap();


        CreateMap<IPaginate<X.SupportMessage>, GetListResponse<GetListSupportMessageListItemDto>>().ReverseMap();
    }
}
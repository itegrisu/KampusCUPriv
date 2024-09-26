using Application.Features.GeneralManagementFeatures.UserShortCuts.Commands.Create;
using Application.Features.GeneralManagementFeatures.UserShortCuts.Commands.Delete;
using Application.Features.GeneralManagementFeatures.UserShortCuts.Commands.Update;
using Application.Features.GeneralManagementFeatures.UserShortCuts.Queries.GetByGid;
using Application.Features.GeneralManagementFeatures.UserShortCuts.Queries.GetByUserGid;
using Application.Features.GeneralManagementFeatures.UserShortCuts.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities.GeneralManagements;

namespace Application.Features.GeneralManagementFeatures.UserShortCuts.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<UserShortCut, CreateUserShortCutCommand>().ReverseMap();
        CreateMap<UserShortCut, CreatedUserShortCutResponse>().ReverseMap();
        CreateMap<UserShortCut, UpdateUserShortCutCommand>().ReverseMap();
        CreateMap<UserShortCut, UpdatedUserShortCutResponse>().ReverseMap();
        CreateMap<UserShortCut, DeleteUserShortCutCommand>().ReverseMap();
        CreateMap<UserShortCut, DeletedUserShortCutResponse>().ReverseMap();

        CreateMap<UserShortCut, GetByGidUserShortCutResponse>().ReverseMap();

        CreateMap<UserShortCut, GetListUserShortCutListItemDto>().ReverseMap();
        CreateMap<IPaginate<UserShortCut>, GetListResponse<GetListUserShortCutListItemDto>>().ReverseMap();

        CreateMap<UserShortCut, GetByUserGidShortCutListItemDto>().ReverseMap();
        CreateMap<IPaginate<UserShortCut>, GetListResponse<GetByUserGidShortCutListItemDto>>().ReverseMap();

    }
}
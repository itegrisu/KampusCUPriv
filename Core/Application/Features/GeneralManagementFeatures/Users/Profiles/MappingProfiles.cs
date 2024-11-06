using Application.Features.GeneralManagementFeatures.Users.Commands.Create;
using Application.Features.GeneralManagementFeatures.Users.Commands.Delete;
using Application.Features.GeneralManagementFeatures.Users.Commands.Update;
using Application.Features.GeneralManagementFeatures.Users.Commands.UpdateForAdmin;
using Application.Features.GeneralManagementFeatures.Users.Queries.GetActiveUser;
using Application.Features.GeneralManagementFeatures.Users.Queries.GetByEnum;
using Application.Features.GeneralManagementFeatures.Users.Queries.GetByGid;
using Application.Features.GeneralManagementFeatures.Users.Queries.GetList;
using Application.Features.GeneralManagementFeatures.Users.Queries.GetListDeleted;
using Application.Features.GeneralManagementFeatures.Users.Queries.GetSystemAdmin;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities.GeneralManagements;

namespace Application.Features.GeneralManagementFeatures.Users.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<User, CreateUserCommand>().ReverseMap();
        CreateMap<User, CreatedUserResponse>().ReverseMap();
        CreateMap<User, UpdateUserCommand>().ReverseMap();
        CreateMap<User, UpdatedUserResponse>().ReverseMap();
        CreateMap<User, UpdateForAdminUserResponse>().ReverseMap();
        CreateMap<User, UpdateForAdminUserCommand>().ReverseMap();
        CreateMap<User, DeleteUserCommand>().ReverseMap();
        CreateMap<User, DeletedUserResponse>().ReverseMap();
        CreateMap<User, GetListUserListItemDto>().ReverseMap();
        CreateMap<User, GetSystemAdminUserListItemDto>().ReverseMap();
        CreateMap<User, GetListDeletedUserListItemDto>().ReverseMap();
        CreateMap<User, GetByGidUserResponse>().ReverseMap();
        CreateMap<IPaginate<User>, GetListResponse<GetListUserListItemDto>>().ReverseMap();
        CreateMap<IPaginate<User>, GetListResponse<GetSystemAdminUserListItemDto>>().ReverseMap();
        CreateMap<IPaginate<User>, GetListResponse<GetListDeletedUserListItemDto>>().ReverseMap();

        CreateMap<User, GetByEnumUserListItemDto>().ReverseMap();
        CreateMap<IPaginate<User>, GetListResponse<GetByEnumUserListItemDto>>().ReverseMap();

        CreateMap<User, GetActiveUserListItemDto>().ReverseMap();
        CreateMap<IPaginate<User>, GetListResponse<GetActiveUserListItemDto>>().ReverseMap();

    }
}
using Application.Features.GeneralFeatures.Users.Commands.Create;
using Application.Features.GeneralFeatures.Users.Commands.Delete;
using Application.Features.GeneralFeatures.Users.Commands.Update;
using Application.Features.GeneralFeatures.Users.Queries.GetByGid;
using Application.Features.GeneralFeatures.Users.Queries.GetList;
using Application.Features.GeneralManagementFeatures.Users.Commands.Login;
using Application.Features.GeneralManagementFeatures.Users.Commands.RefreshToken;
using Application.Features.GeneralManagementFeatures.Users.Commands.RevokeRefreshToken;
using Application.Features.GeneralManagementFeatures.Users.Commands.VerifyEmail;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using X = Domain.Entities.GeneralManagements;

namespace Application.Features.GeneralFeatures.Users.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<X.User, CreateUserCommand>().ReverseMap();
        CreateMap<X.User, CreatedUserResponse>().ReverseMap();
        CreateMap<X.User, UpdateUserCommand>().ReverseMap();
        CreateMap<X.User, UpdatedUserResponse>().ReverseMap();
        CreateMap<X.User, DeleteUserCommand>().ReverseMap();
        CreateMap<X.User, DeletedUserResponse>().ReverseMap();

		CreateMap<X.User, GetByGidUserResponse>().ReverseMap();

        CreateMap<X.User, LoginUserCommand>().ReverseMap();
        CreateMap<X.User, LoginUserResponse>().ReverseMap();

        CreateMap<X.User, VerifyEmailUserCommand>().ReverseMap();
        CreateMap<X.User, VerifyEmailUserResponse>().ReverseMap();

        CreateMap<X.User, GetListUserListItemDto>().ReverseMap();
        CreateMap<IPaginate<X.User>, GetListResponse<GetListUserListItemDto>>().ReverseMap();

        CreateMap<X.User, RefreshTokenCommand>().ReverseMap();
        CreateMap<X.User, RefreshTokenResponse>().ReverseMap();

        CreateMap<X.User, RevokeRefreshTokenCommand>().ReverseMap();
        CreateMap<X.User, RevokeRefreshTokenResponse>().ReverseMap();
    }
}
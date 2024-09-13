using Application.Features.LogManagementFeatures.LogSuccessedLogins.Commands.Create;
using Application.Features.LogManagementFeatures.LogSuccessedLogins.Commands.Delete;
using Application.Features.LogManagementFeatures.LogSuccessedLogins.Commands.LogOutLog;
using Application.Features.LogManagementFeatures.LogSuccessedLogins.Commands.Update;
using Application.Features.LogManagementFeatures.LogSuccessedLogins.Queries.GetActiveLoginUser;
using Application.Features.LogManagementFeatures.LogSuccessedLogins.Queries.GetByGid;
using Application.Features.LogManagementFeatures.LogSuccessedLogins.Queries.GetLastTenLogin;
using Application.Features.LogManagementFeatures.LogSuccessedLogins.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities.LogManagements;
using X = Domain.Entities.LogManagements;

namespace Application.Features.LogManagementFeatures.LogSuccessedLogins.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<X.LogSuccessedLogin, CreateLogSuccessedLoginCommand>().ReverseMap();
        CreateMap<X.LogSuccessedLogin, CreatedLogSuccessedLoginResponse>().ReverseMap();
        CreateMap<X.LogSuccessedLogin, UpdateLogSuccessedLoginCommand>().ReverseMap();
        CreateMap<X.LogSuccessedLogin, UpdatedLogSuccessedLoginResponse>().ReverseMap();
        CreateMap<X.LogSuccessedLogin, DeleteLogSuccessedLoginCommand>().ReverseMap();
        CreateMap<X.LogSuccessedLogin, DeletedLogSuccessedLoginResponse>().ReverseMap();

        CreateMap<X.LogSuccessedLogin, LogOutLogSuccessedLoginCommand>().ReverseMap();
        CreateMap<X.LogSuccessedLogin, LogOutLogSuccessedLoginResponse>().ReverseMap();
        
        CreateMap<X.LogSuccessedLogin, GetByGidLogSuccessedLoginResponse>().ReverseMap();

        CreateMap<X.LogSuccessedLogin, GetListLogSuccessedLoginListItemDto>().ReverseMap();
        CreateMap<IPaginate<X.LogSuccessedLogin>, GetListResponse<GetListLogSuccessedLoginListItemDto>>().ReverseMap();

        CreateMap<LogSuccessedLogin, GetActiveLoginUserLogSuccessedLoginListItemDto>().ReverseMap();
        CreateMap<IPaginate<LogSuccessedLogin>, GetListResponse<GetActiveLoginUserLogSuccessedLoginListItemDto>>().ReverseMap();

        CreateMap<X.LogSuccessedLogin, GetLastTenLogSuccessedLoginListItemDto>().ReverseMap();
        CreateMap<IPaginate<X.LogSuccessedLogin>, GetListResponse<GetLastTenLogSuccessedLoginListItemDto>>().ReverseMap();




    }
}
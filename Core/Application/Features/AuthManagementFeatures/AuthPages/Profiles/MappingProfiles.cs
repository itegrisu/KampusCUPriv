using Application.Features.AuthManagementFeatures.AuthPages.Commands.Create;
using Application.Features.AuthManagementFeatures.AuthPages.Commands.Delete;
using Application.Features.AuthManagementFeatures.AuthPages.Commands.Update;
using Application.Features.AuthManagementFeatures.AuthPages.Queries.GetByGid;
using Application.Features.AuthManagementFeatures.AuthPages.Queries.GetByUserGid;
using Application.Features.AuthManagementFeatures.AuthPages.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities.AuthManagements;

namespace Application.Features.AuthManagementFeatures.AuthPages.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<AuthPage, CreateAuthPageCommand>().ReverseMap();
        CreateMap<AuthPage, CreatedAuthPageResponse>().ReverseMap();
        CreateMap<AuthPage, UpdateAuthPageCommand>().ReverseMap();
        CreateMap<AuthPage, UpdatedAuthPageResponse>().ReverseMap();
        CreateMap<AuthPage, DeleteAuthPageCommand>().ReverseMap();
        CreateMap<AuthPage, DeletedAuthPageResponse>().ReverseMap();
        CreateMap<AuthPage, GetByGidAuthPageResponse>().ReverseMap();
        CreateMap<AuthPage, GetListAuthPageListItemDto>().ReverseMap();
        CreateMap<IPaginate<AuthPage>, GetListResponse<GetListAuthPageListItemDto>>().ReverseMap();
        CreateMap<AuthPage, GetByUserGidAuthPageListItemDto>().ReverseMap();

    }
}
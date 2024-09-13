using Application.Features.LogManagementFeatures.LogFailedLogins.Commands.Create;
using Application.Features.LogManagementFeatures.LogFailedLogins.Commands.Delete;
using Application.Features.LogManagementFeatures.LogFailedLogins.Commands.Update;
using Application.Features.LogManagementFeatures.LogFailedLogins.Queries.GetByGid;
using Application.Features.LogManagementFeatures.LogFailedLogins.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using X = Domain.Entities.LogManagements;

namespace Application.Features.LogManagementFeatures.LogFailedLogins.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<X.LogFailedLogin, CreateLogFailedLoginCommand>().ReverseMap();
        CreateMap<X.LogFailedLogin, CreatedLogFailedLoginResponse>().ReverseMap();
        CreateMap<X.LogFailedLogin, UpdateLogFailedLoginCommand>().ReverseMap();
        CreateMap<X.LogFailedLogin, UpdatedLogFailedLoginResponse>().ReverseMap();
        CreateMap<X.LogFailedLogin, DeleteLogFailedLoginCommand>().ReverseMap();
        CreateMap<X.LogFailedLogin, DeletedLogFailedLoginResponse>().ReverseMap();

		CreateMap<X.LogFailedLogin, GetByGidLogFailedLoginResponse>().ReverseMap();

        CreateMap<X.LogFailedLogin, GetListLogFailedLoginListItemDto>().ReverseMap();
        CreateMap<IPaginate<X.LogFailedLogin>, GetListResponse<GetListLogFailedLoginListItemDto>>().ReverseMap();
    }
}
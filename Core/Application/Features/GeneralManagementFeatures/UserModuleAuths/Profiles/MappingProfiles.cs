using Application.Features.GeneralManagementFeatures.UserModuleAuths.Commands.Create;
using Application.Features.GeneralManagementFeatures.UserModuleAuths.Commands.Delete;
using Application.Features.GeneralManagementFeatures.UserModuleAuths.Commands.Update;
using Application.Features.GeneralManagementFeatures.UserModuleAuths.Queries.GetByGid;
using Application.Features.GeneralManagementFeatures.UserModuleAuths.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using X = Domain.Entities.GeneralManagements;

namespace Application.Features.GeneralManagementFeatures.UserModuleAuths.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<X.UserModuleAuth, CreateUserModuleAuthCommand>().ReverseMap();
        CreateMap<X.UserModuleAuth, CreatedUserModuleAuthResponse>().ReverseMap();
        CreateMap<X.UserModuleAuth, UpdateUserModuleAuthCommand>().ReverseMap();
        CreateMap<X.UserModuleAuth, UpdatedUserModuleAuthResponse>().ReverseMap();
        CreateMap<X.UserModuleAuth, DeleteUserModuleAuthCommand>().ReverseMap();
        CreateMap<X.UserModuleAuth, DeletedUserModuleAuthResponse>().ReverseMap();

		CreateMap<X.UserModuleAuth, GetByGidUserModuleAuthResponse>().ReverseMap();

        CreateMap<X.UserModuleAuth, GetListUserModuleAuthListItemDto>().ReverseMap();
        CreateMap<IPaginate<X.UserModuleAuth>, GetListResponse<GetListUserModuleAuthListItemDto>>().ReverseMap();
    }
}
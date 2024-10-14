using Application.Features.GeneralManagementFeatures.DepartmentUsers.Commands.Create;
using Application.Features.GeneralManagementFeatures.DepartmentUsers.Commands.Delete;
using Application.Features.GeneralManagementFeatures.DepartmentUsers.Commands.Update;
using Application.Features.GeneralManagementFeatures.DepartmentUsers.Queries.GetByGid;
using Application.Features.GeneralManagementFeatures.DepartmentUsers.Queries.GetList;
using Application.Features.GeneralManagementFeatures.DepartmentUsers.Queries.GetListByDepartmentGid;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using X = Domain.Entities.GeneralManagements;

namespace Application.Features.GeneralManagementFeatures.DepartmentUsers.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<X.DepartmentUser, CreateDepartmentUserCommand>().ReverseMap();
        CreateMap<X.DepartmentUser, CreatedDepartmentUserResponse>().ReverseMap();
        CreateMap<X.DepartmentUser, UpdateDepartmentUserCommand>().ReverseMap();
        CreateMap<X.DepartmentUser, UpdatedDepartmentUserResponse>().ReverseMap();
        CreateMap<X.DepartmentUser, DeleteDepartmentUserCommand>().ReverseMap();
        CreateMap<X.DepartmentUser, DeletedDepartmentUserResponse>().ReverseMap();

        CreateMap<X.DepartmentUser, GetByGidDepartmentUserResponse>().ReverseMap();

        CreateMap<X.DepartmentUser, GetListDepartmentUserListItemDto>().ReverseMap();
        CreateMap<IPaginate<X.DepartmentUser>, GetListResponse<GetListDepartmentUserListItemDto>>().ReverseMap();

        CreateMap<X.DepartmentUser, GetByDepartmentGidDepartmentUserListItemDto>().ReverseMap();
        CreateMap<IPaginate<X.DepartmentUser>, GetListResponse<GetByDepartmentGidDepartmentUserListItemDto>>().ReverseMap();


    }
}
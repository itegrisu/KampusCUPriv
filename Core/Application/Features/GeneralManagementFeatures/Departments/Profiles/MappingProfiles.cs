using Application.Features.GeneralManagementFeatures.Departments.Commands.Create;
using Application.Features.GeneralManagementFeatures.Departments.Commands.Delete;
using Application.Features.GeneralManagementFeatures.Departments.Commands.Update;
using Application.Features.GeneralManagementFeatures.Departments.Queries.GetByGid;
using Application.Features.GeneralManagementFeatures.Departments.Queries.GetList;
using Application.Features.GeneralManagementFeatures.Departments.Queries.GetListWithUser;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using X = Domain.Entities.GeneralManagements;

namespace Application.Features.GeneralManagementFeatures.Departments.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<X.Department, CreateDepartmentCommand>().ReverseMap();
        CreateMap<X.Department, CreatedDepartmentResponse>().ReverseMap();
        CreateMap<X.Department, UpdateDepartmentCommand>().ReverseMap();
        CreateMap<X.Department, UpdatedDepartmentResponse>().ReverseMap();
        CreateMap<X.Department, DeleteDepartmentCommand>().ReverseMap();
        CreateMap<X.Department, DeletedDepartmentResponse>().ReverseMap();

        CreateMap<X.Department, GetByGidDepartmentResponse>().ReverseMap();

        CreateMap<X.Department, GetListDepartmentListItemDto>().ReverseMap();
        CreateMap<IPaginate<X.Department>, GetListResponse<GetListDepartmentListItemDto>>().ReverseMap();

        CreateMap<X.Department, GetListWithUserDepartmentListItemDto>().ReverseMap();
        CreateMap<IPaginate<X.Department>, GetListResponse<GetListWithUserDepartmentListItemDto>>().ReverseMap();
    }
}
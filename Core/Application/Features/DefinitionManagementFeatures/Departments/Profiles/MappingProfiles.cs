using Application.Features.DefinitionFeatures.Departments.Commands.Create;
using Application.Features.DefinitionFeatures.Departments.Commands.Delete;
using Application.Features.DefinitionFeatures.Departments.Commands.Update;
using Application.Features.DefinitionFeatures.Departments.Queries.GetByGid;
using Application.Features.DefinitionFeatures.Departments.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using X = Domain.Entities.DefinitionManagements;

namespace Application.Features.DefinitionFeatures.Departments.Profiles;

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
    }
}
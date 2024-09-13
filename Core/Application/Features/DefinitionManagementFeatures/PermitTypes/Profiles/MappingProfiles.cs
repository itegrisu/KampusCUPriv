using Application.Features.DefinitionManagementFeatures.PermitTypes.Commands.Create;
using Application.Features.DefinitionManagementFeatures.PermitTypes.Commands.Delete;
using Application.Features.DefinitionManagementFeatures.PermitTypes.Commands.Update;
using Application.Features.DefinitionManagementFeatures.PermitTypes.Queries.GetByGid;
using Application.Features.DefinitionManagementFeatures.PermitTypes.Queries.GetById;
using Application.Features.DefinitionManagementFeatures.PermitTypes.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using X = Domain.Entities.DefinitionManagements;

namespace Application.Features.DefinitionManagementFeatures.PermitTypes.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<X.PermitType, CreatePermitTypeCommand>().ReverseMap();
        CreateMap<X.PermitType, CreatedPermitTypeResponse>().ReverseMap();
        CreateMap<X.PermitType, UpdatePermitTypeCommand>().ReverseMap();
        CreateMap<X.PermitType, UpdatedPermitTypeResponse>().ReverseMap();
        CreateMap<X.PermitType, DeletePermitTypeCommand>().ReverseMap();
        CreateMap<X.PermitType, DeletedPermitTypeResponse>().ReverseMap();

		CreateMap<X.PermitType, GetByGidPermitTypeResponse>().ReverseMap();

        CreateMap<X.PermitType, GetListPermitTypeListItemDto>().ReverseMap();
        CreateMap<IPaginate<X.PermitType>, GetListResponse<GetListPermitTypeListItemDto>>().ReverseMap();
    }
}
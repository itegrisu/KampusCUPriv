using Application.Features.DefinitionManagementFeatures.OtoBrands.Commands.Create;
using Application.Features.DefinitionManagementFeatures.OtoBrands.Commands.Delete;
using Application.Features.DefinitionManagementFeatures.OtoBrands.Commands.Update;
using Application.Features.DefinitionManagementFeatures.OtoBrands.Queries.GetByGid;
using Application.Features.DefinitionManagementFeatures.OtoBrands.Queries.GetById;
using Application.Features.DefinitionManagementFeatures.OtoBrands.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using X = Domain.Entities.DefinitionManagements;

namespace Application.Features.DefinitionManagementFeatures.OtoBrands.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<X.OtoBrand, CreateOtoBrandCommand>().ReverseMap();
        CreateMap<X.OtoBrand, CreatedOtoBrandResponse>().ReverseMap();
        CreateMap<X.OtoBrand, UpdateOtoBrandCommand>().ReverseMap();
        CreateMap<X.OtoBrand, UpdatedOtoBrandResponse>().ReverseMap();
        CreateMap<X.OtoBrand, DeleteOtoBrandCommand>().ReverseMap();
        CreateMap<X.OtoBrand, DeletedOtoBrandResponse>().ReverseMap();

		CreateMap<X.OtoBrand, GetByGidOtoBrandResponse>().ReverseMap();

        CreateMap<X.OtoBrand, GetListOtoBrandListItemDto>().ReverseMap();
        CreateMap<IPaginate<X.OtoBrand>, GetListResponse<GetListOtoBrandListItemDto>>().ReverseMap();
    }
}
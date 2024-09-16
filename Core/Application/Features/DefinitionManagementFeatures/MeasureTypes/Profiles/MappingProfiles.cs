using Application.Features.DefinitionManagementFeatures.MeasureTypes.Commands.Create;
using Application.Features.DefinitionManagementFeatures.MeasureTypes.Commands.Delete;
using Application.Features.DefinitionManagementFeatures.MeasureTypes.Commands.Update;
using Application.Features.DefinitionManagementFeatures.MeasureTypes.Queries.GetByGid;
using Application.Features.DefinitionManagementFeatures.MeasureTypes.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using X = Domain.Entities.DefinitionManagements;

namespace Application.Features.DefinitionManagementFeatures.MeasureTypes.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<X.MeasureType, CreateMeasureTypeCommand>().ReverseMap();
        CreateMap<X.MeasureType, CreatedMeasureTypeResponse>().ReverseMap();
        CreateMap<X.MeasureType, UpdateMeasureTypeCommand>().ReverseMap();
        CreateMap<X.MeasureType, UpdatedMeasureTypeResponse>().ReverseMap();
        CreateMap<X.MeasureType, DeleteMeasureTypeCommand>().ReverseMap();
        CreateMap<X.MeasureType, DeletedMeasureTypeResponse>().ReverseMap();

        CreateMap<X.MeasureType, GetByGidMeasureTypeResponse>().ReverseMap();

        CreateMap<X.MeasureType, GetListMeasureTypeListItemDto>().ReverseMap();
        CreateMap<IPaginate<X.MeasureType>, GetListResponse<GetListMeasureTypeListItemDto>>().ReverseMap();
    }
}
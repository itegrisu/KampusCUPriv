using Application.Features.DefinitionManagementFeatures.DocumentTypes.Commands.Create;
using Application.Features.DefinitionManagementFeatures.DocumentTypes.Commands.Delete;
using Application.Features.DefinitionManagementFeatures.DocumentTypes.Commands.Update;
using Application.Features.DefinitionManagementFeatures.DocumentTypes.Queries.GetByGid;
using Application.Features.DefinitionManagementFeatures.DocumentTypes.Queries.GetById;
using Application.Features.DefinitionManagementFeatures.DocumentTypes.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using X = Domain.Entities.DefinitionManagements;

namespace Application.Features.DefinitionManagementFeatures.DocumentTypes.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<X.DocumentType, CreateDocumentTypeCommand>().ReverseMap();
        CreateMap<X.DocumentType, CreatedDocumentTypeResponse>().ReverseMap();
        CreateMap<X.DocumentType, UpdateDocumentTypeCommand>().ReverseMap();
        CreateMap<X.DocumentType, UpdatedDocumentTypeResponse>().ReverseMap();
        CreateMap<X.DocumentType, DeleteDocumentTypeCommand>().ReverseMap();
        CreateMap<X.DocumentType, DeletedDocumentTypeResponse>().ReverseMap();

		CreateMap<X.DocumentType, GetByGidDocumentTypeResponse>().ReverseMap();

        CreateMap<X.DocumentType, GetListDocumentTypeListItemDto>().ReverseMap();
        CreateMap<IPaginate<X.DocumentType>, GetListResponse<GetListDocumentTypeListItemDto>>().ReverseMap();
    }
}
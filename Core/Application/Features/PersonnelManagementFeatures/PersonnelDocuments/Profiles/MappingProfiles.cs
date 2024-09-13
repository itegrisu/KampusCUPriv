using Application.Features.PersonnelManagementFeatures.PersonnelDocuments.Commands.Create;
using Application.Features.PersonnelManagementFeatures.PersonnelDocuments.Commands.Delete;
using Application.Features.PersonnelManagementFeatures.PersonnelDocuments.Commands.Update;
using Application.Features.PersonnelManagementFeatures.PersonnelDocuments.Queries.GetByGid;
using Application.Features.PersonnelManagementFeatures.PersonnelDocuments.Queries.GetById;
using Application.Features.PersonnelManagementFeatures.PersonnelDocuments.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using X = Domain.Entities.PersonnelManagements;

namespace Application.Features.PersonnelManagementFeatures.PersonnelDocuments.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<X.PersonnelDocument, CreatePersonnelDocumentCommand>().ReverseMap();
        CreateMap<X.PersonnelDocument, CreatedPersonnelDocumentResponse>().ReverseMap();
        CreateMap<X.PersonnelDocument, UpdatePersonnelDocumentCommand>().ReverseMap();
        CreateMap<X.PersonnelDocument, UpdatedPersonnelDocumentResponse>().ReverseMap();
        CreateMap<X.PersonnelDocument, DeletePersonnelDocumentCommand>().ReverseMap();
        CreateMap<X.PersonnelDocument, DeletedPersonnelDocumentResponse>().ReverseMap();

		CreateMap<X.PersonnelDocument, GetByGidPersonnelDocumentResponse>().ReverseMap();

        CreateMap<X.PersonnelDocument, GetListPersonnelDocumentListItemDto>().ReverseMap();
        CreateMap<IPaginate<X.PersonnelDocument>, GetListResponse<GetListPersonnelDocumentListItemDto>>().ReverseMap();
    }
}
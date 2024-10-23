using Application.Features.PersonnelManagementFeatures.PersonnelResidenceInfos.Commands.Create;
using Application.Features.PersonnelManagementFeatures.PersonnelResidenceInfos.Commands.Delete;
using Application.Features.PersonnelManagementFeatures.PersonnelResidenceInfos.Commands.Update;
using Application.Features.PersonnelManagementFeatures.PersonnelResidenceInfos.Queries.GetByGid;
using Application.Features.PersonnelManagementFeatures.PersonnelResidenceInfos.Queries.GetByUserGid;
using Application.Features.PersonnelManagementFeatures.PersonnelResidenceInfos.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using X = Domain.Entities.PersonnelManagements;

namespace Application.Features.PersonnelManagementFeatures.PersonnelResidenceInfos.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<X.PersonnelResidenceInfo, CreatePersonnelResidenceInfoCommand>().ReverseMap();
        CreateMap<X.PersonnelResidenceInfo, CreatedPersonnelResidenceInfoResponse>().ReverseMap();
        CreateMap<X.PersonnelResidenceInfo, UpdatePersonnelResidenceInfoCommand>().ReverseMap();
        CreateMap<X.PersonnelResidenceInfo, UpdatedPersonnelResidenceInfoResponse>().ReverseMap();
        CreateMap<X.PersonnelResidenceInfo, DeletePersonnelResidenceInfoCommand>().ReverseMap();
        CreateMap<X.PersonnelResidenceInfo, DeletedPersonnelResidenceInfoResponse>().ReverseMap();

        CreateMap<X.PersonnelResidenceInfo, GetByGidPersonnelResidenceInfoResponse>().ReverseMap();

        CreateMap<X.PersonnelResidenceInfo, GetListPersonnelResidenceInfoListItemDto>().ReverseMap();
        CreateMap<IPaginate<X.PersonnelResidenceInfo>, GetListResponse<GetListPersonnelResidenceInfoListItemDto>>().ReverseMap();

        CreateMap<X.PersonnelResidenceInfo, GetByUserGidListPersonnelResidenceInfoListItemDto>().ReverseMap();
        CreateMap<IPaginate<X.PersonnelResidenceInfo>, GetListResponse<GetByUserGidListPersonnelResidenceInfoListItemDto>>().ReverseMap();
    }
}
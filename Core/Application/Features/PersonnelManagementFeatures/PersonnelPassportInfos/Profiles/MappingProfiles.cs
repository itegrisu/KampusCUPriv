using Application.Features.PersonnelManagementFeatures.PersonnelPassportInfos.Commands.Create;
using Application.Features.PersonnelManagementFeatures.PersonnelPassportInfos.Commands.Delete;
using Application.Features.PersonnelManagementFeatures.PersonnelPassportInfos.Commands.Update;
using Application.Features.PersonnelManagementFeatures.PersonnelPassportInfos.Queries.GetByGid;
using Application.Features.PersonnelManagementFeatures.PersonnelPassportInfos.Queries.GetByUserGid;
using Application.Features.PersonnelManagementFeatures.PersonnelPassportInfos.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using X = Domain.Entities.PersonnelManagements;

namespace Application.Features.PersonnelManagementFeatures.PersonnelPassportInfos.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<X.PersonnelPassportInfo, CreatePersonnelPassportInfoCommand>().ReverseMap();
        CreateMap<X.PersonnelPassportInfo, CreatedPersonnelPassportInfoResponse>().ReverseMap();
        CreateMap<X.PersonnelPassportInfo, UpdatePersonnelPassportInfoCommand>().ReverseMap();
        CreateMap<X.PersonnelPassportInfo, UpdatedPersonnelPassportInfoResponse>().ReverseMap();
        CreateMap<X.PersonnelPassportInfo, DeletePersonnelPassportInfoCommand>().ReverseMap();
        CreateMap<X.PersonnelPassportInfo, DeletedPersonnelPassportInfoResponse>().ReverseMap();

        CreateMap<X.PersonnelPassportInfo, GetByGidPersonnelPassportInfoResponse>().ReverseMap();

        CreateMap<X.PersonnelPassportInfo, GetListPersonnelPassportInfoListItemDto>().ReverseMap();
        CreateMap<IPaginate<X.PersonnelPassportInfo>, GetListResponse<GetListPersonnelPassportInfoListItemDto>>().ReverseMap();

        CreateMap<X.PersonnelPassportInfo, GetByUserGidListPersonnelPassportInfoListItemDto>().ReverseMap();
        CreateMap<IPaginate<X.PersonnelPassportInfo>, GetListResponse<GetByUserGidListPersonnelPassportInfoListItemDto>>().ReverseMap();
    }
}
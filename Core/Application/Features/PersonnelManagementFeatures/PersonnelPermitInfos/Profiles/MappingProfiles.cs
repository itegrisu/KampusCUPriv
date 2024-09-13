using Application.Features.PersonnelManagementFeatures.PersonnelPermitInfos.Commands.Create;
using Application.Features.PersonnelManagementFeatures.PersonnelPermitInfos.Commands.Delete;
using Application.Features.PersonnelManagementFeatures.PersonnelPermitInfos.Commands.Update;
using Application.Features.PersonnelManagementFeatures.PersonnelPermitInfos.Queries.GetByGid;
using Application.Features.PersonnelManagementFeatures.PersonnelPermitInfos.Queries.GetById;
using Application.Features.PersonnelManagementFeatures.PersonnelPermitInfos.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using X = Domain.Entities.PersonnelManagements;

namespace Application.Features.PersonnelManagementFeatures.PersonnelPermitInfos.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<X.PersonnelPermitInfo, CreatePersonnelPermitInfoCommand>().ReverseMap();
        CreateMap<X.PersonnelPermitInfo, CreatedPersonnelPermitInfoResponse>().ReverseMap();
        CreateMap<X.PersonnelPermitInfo, UpdatePersonnelPermitInfoCommand>().ReverseMap();
        CreateMap<X.PersonnelPermitInfo, UpdatedPersonnelPermitInfoResponse>().ReverseMap();
        CreateMap<X.PersonnelPermitInfo, DeletePersonnelPermitInfoCommand>().ReverseMap();
        CreateMap<X.PersonnelPermitInfo, DeletedPersonnelPermitInfoResponse>().ReverseMap();

		CreateMap<X.PersonnelPermitInfo, GetByGidPersonnelPermitInfoResponse>().ReverseMap();

        CreateMap<X.PersonnelPermitInfo, GetListPersonnelPermitInfoListItemDto>().ReverseMap();
        CreateMap<IPaginate<X.PersonnelPermitInfo>, GetListResponse<GetListPersonnelPermitInfoListItemDto>>().ReverseMap();
    }
}
using Application.Features.PersonnelManagementFeatures.PersonnelGraduatedSchools.Commands.Create;
using Application.Features.PersonnelManagementFeatures.PersonnelGraduatedSchools.Commands.Delete;
using Application.Features.PersonnelManagementFeatures.PersonnelGraduatedSchools.Commands.Update;
using Application.Features.PersonnelManagementFeatures.PersonnelGraduatedSchools.Queries.GetByGid;
using Application.Features.PersonnelManagementFeatures.PersonnelGraduatedSchools.Queries.GetById;
using Application.Features.PersonnelManagementFeatures.PersonnelGraduatedSchools.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using X = Domain.Entities.PersonnelManagements;

namespace Application.Features.PersonnelManagementFeatures.PersonnelGraduatedSchools.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<X.PersonnelGraduatedSchool, CreatePersonnelGraduatedSchoolCommand>().ReverseMap();
        CreateMap<X.PersonnelGraduatedSchool, CreatedPersonnelGraduatedSchoolResponse>().ReverseMap();
        CreateMap<X.PersonnelGraduatedSchool, UpdatePersonnelGraduatedSchoolCommand>().ReverseMap();
        CreateMap<X.PersonnelGraduatedSchool, UpdatedPersonnelGraduatedSchoolResponse>().ReverseMap();
        CreateMap<X.PersonnelGraduatedSchool, DeletePersonnelGraduatedSchoolCommand>().ReverseMap();
        CreateMap<X.PersonnelGraduatedSchool, DeletedPersonnelGraduatedSchoolResponse>().ReverseMap();

		CreateMap<X.PersonnelGraduatedSchool, GetByGidPersonnelGraduatedSchoolResponse>().ReverseMap();

        CreateMap<X.PersonnelGraduatedSchool, GetListPersonnelGraduatedSchoolListItemDto>().ReverseMap();
        CreateMap<IPaginate<X.PersonnelGraduatedSchool>, GetListResponse<GetListPersonnelGraduatedSchoolListItemDto>>().ReverseMap();
    }
}
using Application.Features.ClubFeatures.StudentClubs.Commands.Create;
using Application.Features.ClubFeatures.StudentClubs.Commands.Delete;
using Application.Features.ClubFeatures.StudentClubs.Commands.Update;
using Application.Features.ClubFeatures.StudentClubs.Queries.GetByGid;
using Application.Features.ClubFeatures.StudentClubs.Queries.GetList;
using Application.Features.ClubManagementFeatures.StudentClubs.Queries.GetByClubGid;
using Application.Features.ClubManagementFeatures.StudentClubs.Queries.GetByUserGid;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using X = Domain.Entities.ClubManagements;

namespace Application.Features.ClubFeatures.StudentClubs.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<X.StudentClub, CreateStudentClubCommand>().ReverseMap();
        CreateMap<X.StudentClub, CreatedStudentClubResponse>().ReverseMap();
        CreateMap<X.StudentClub, UpdateStudentClubCommand>().ReverseMap();
        CreateMap<X.StudentClub, UpdatedStudentClubResponse>().ReverseMap();
        CreateMap<X.StudentClub, DeleteStudentClubCommand>().ReverseMap();
        CreateMap<X.StudentClub, DeletedStudentClubResponse>().ReverseMap();

		CreateMap<X.StudentClub, GetByGidStudentClubResponse>().ReverseMap();

        CreateMap<X.StudentClub, GetListStudentClubListItemDto>().ReverseMap();
        CreateMap<IPaginate<X.StudentClub>, GetListResponse<GetListStudentClubListItemDto>>().ReverseMap();

        CreateMap<X.StudentClub, GetByUserGidListStudentClubListItemDto>().ReverseMap();
        CreateMap<IPaginate<X.StudentClub>, GetListResponse<GetByUserGidListStudentClubListItemDto>>().ReverseMap();

        CreateMap<X.StudentClub, GetByClubGidListStudentClubListItemDto>().ReverseMap();
        CreateMap<IPaginate<X.StudentClub>, GetListResponse<GetByClubGidListStudentClubListItemDto>>().ReverseMap();
    }
}
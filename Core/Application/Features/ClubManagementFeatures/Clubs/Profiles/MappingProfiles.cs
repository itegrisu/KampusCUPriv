using Application.Features.ClubFeatures.Clubs.Commands.Create;
using Application.Features.ClubFeatures.Clubs.Commands.Delete;
using Application.Features.ClubFeatures.Clubs.Commands.Update;
using Application.Features.ClubFeatures.Clubs.Queries.GetByGid;
using Application.Features.ClubFeatures.Clubs.Queries.GetList;
using Application.Features.ClubManagementFeatures.Clubs.Queries.GetByCount;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using X = Domain.Entities.ClubManagements;

namespace Application.Features.ClubFeatures.Clubs.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<X.Club, CreateClubCommand>().ReverseMap();
        CreateMap<X.Club, CreatedClubResponse>().ReverseMap();
        CreateMap<X.Club, UpdateClubCommand>().ReverseMap();
        CreateMap<X.Club, UpdatedClubResponse>().ReverseMap();
        CreateMap<X.Club, DeleteClubCommand>().ReverseMap();
        CreateMap<X.Club, DeletedClubResponse>().ReverseMap();

		CreateMap<X.Club, GetByGidClubResponse>().ReverseMap();

        CreateMap<X.Club, GetListClubListItemDto>().ReverseMap();
        CreateMap<IPaginate<X.Club>, GetListResponse<GetListClubListItemDto>>().ReverseMap();

        CreateMap<X.Club, GetByCountListClubListItemDto>().ReverseMap();
        CreateMap<IPaginate<X.Club>, GetListResponse<GetByCountListClubListItemDto>>().ReverseMap();
    }
}
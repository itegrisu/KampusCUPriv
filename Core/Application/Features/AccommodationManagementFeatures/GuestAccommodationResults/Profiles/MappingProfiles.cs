using Application.Features.AccommodationManagementFeatures.GuestAccommodationResults.Commands.Create;
using Application.Features.AccommodationManagementFeatures.GuestAccommodationResults.Commands.Delete;
using Application.Features.AccommodationManagementFeatures.GuestAccommodationResults.Commands.Update;
using Application.Features.AccommodationManagementFeatures.GuestAccommodationResults.Queries.GetByGid;
using Application.Features.AccommodationManagementFeatures.GuestAccommodationResults.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using X = Domain.Entities.AccommodationManagements;

namespace Application.Features.AccommodationManagementFeatures.GuestAccommodationResults.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<X.GuestAccommodationResult, CreateGuestAccommodationResultCommand>().ReverseMap();
        CreateMap<X.GuestAccommodationResult, CreatedGuestAccommodationResultResponse>().ReverseMap();
        CreateMap<X.GuestAccommodationResult, UpdateGuestAccommodationResultCommand>().ReverseMap();
        CreateMap<X.GuestAccommodationResult, UpdatedGuestAccommodationResultResponse>().ReverseMap();
        CreateMap<X.GuestAccommodationResult, DeleteGuestAccommodationResultCommand>().ReverseMap();
        CreateMap<X.GuestAccommodationResult, DeletedGuestAccommodationResultResponse>().ReverseMap();

		CreateMap<X.GuestAccommodationResult, GetByGidGuestAccommodationResultResponse>().ReverseMap();

        CreateMap<X.GuestAccommodationResult, GetListGuestAccommodationResultListItemDto>().ReverseMap();
        CreateMap<IPaginate<X.GuestAccommodationResult>, GetListResponse<GetListGuestAccommodationResultListItemDto>>().ReverseMap();
    }
}
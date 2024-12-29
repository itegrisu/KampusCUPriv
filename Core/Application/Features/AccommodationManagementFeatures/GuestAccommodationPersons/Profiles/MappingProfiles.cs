using Application.Features.AccommodationManagementFeatures.GuestAccommodationPersons.Commands.Create;
using Application.Features.AccommodationManagementFeatures.GuestAccommodationPersons.Commands.Delete;
using Application.Features.AccommodationManagementFeatures.GuestAccommodationPersons.Commands.Update;
using Application.Features.AccommodationManagementFeatures.GuestAccommodationPersons.Queries.GetByGid;
using Application.Features.AccommodationManagementFeatures.GuestAccommodationPersons.Queries.GetByGuestGid;
using Application.Features.AccommodationManagementFeatures.GuestAccommodationPersons.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using X = Domain.Entities.AccommodationManagements;

namespace Application.Features.AccommodationManagementFeatures.GuestAccommodationPersons.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<X.GuestAccommodationPerson, CreateGuestAccommodationPersonCommand>().ReverseMap();
        CreateMap<X.GuestAccommodationPerson, CreatedGuestAccommodationPersonResponse>().ReverseMap();
        CreateMap<X.GuestAccommodationPerson, UpdateGuestAccommodationPersonCommand>().ReverseMap();
        CreateMap<X.GuestAccommodationPerson, UpdatedGuestAccommodationPersonResponse>().ReverseMap();
        CreateMap<X.GuestAccommodationPerson, DeleteGuestAccommodationPersonCommand>().ReverseMap();
        CreateMap<X.GuestAccommodationPerson, DeletedGuestAccommodationPersonResponse>().ReverseMap();

		CreateMap<X.GuestAccommodationPerson, GetByGidGuestAccommodationPersonResponse>().ReverseMap();

        CreateMap<X.GuestAccommodationPerson, GetListGuestAccommodationPersonListItemDto>().ReverseMap();
        CreateMap<IPaginate<X.GuestAccommodationPerson>, GetListResponse<GetListGuestAccommodationPersonListItemDto>>().ReverseMap();

        CreateMap<X.GuestAccommodationPerson, GetByGuestGidListGuestAccommodationPersonListItemDto>().ReverseMap();
        CreateMap<IPaginate<X.GuestAccommodationPerson>, GetListResponse<GetByGuestGidListGuestAccommodationPersonListItemDto>>().ReverseMap();
    }
}
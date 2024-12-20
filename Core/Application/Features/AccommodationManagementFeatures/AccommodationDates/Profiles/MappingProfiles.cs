using Application.Features.AccommodationManagementFeatures.AccommodationDates.Commands.Create;
using Application.Features.AccommodationManagementFeatures.AccommodationDates.Commands.Delete;
using Application.Features.AccommodationManagementFeatures.AccommodationDates.Commands.Update;
using Application.Features.AccommodationManagementFeatures.AccommodationDates.Queries.GetByGid;
using Application.Features.AccommodationManagementFeatures.AccommodationDates.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using X = Domain.Entities.AccommodationManagements;

namespace Application.Features.AccommodationManagementFeatures.AccommodationDates.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<X.AccommodationDate, CreateAccommodationDateCommand>().ReverseMap();
        CreateMap<X.AccommodationDate, CreatedAccommodationDateResponse>().ReverseMap();
        CreateMap<X.AccommodationDate, UpdateAccommodationDateCommand>().ReverseMap();
        CreateMap<X.AccommodationDate, UpdatedAccommodationDateResponse>().ReverseMap();
        CreateMap<X.AccommodationDate, DeleteAccommodationDateCommand>().ReverseMap();
        CreateMap<X.AccommodationDate, DeletedAccommodationDateResponse>().ReverseMap();

		CreateMap<X.AccommodationDate, GetByGidAccommodationDateResponse>().ReverseMap();

        CreateMap<X.AccommodationDate, GetListAccommodationDateListItemDto>().ReverseMap();
        CreateMap<IPaginate<X.AccommodationDate>, GetListResponse<GetListAccommodationDateListItemDto>>().ReverseMap();
    }
}
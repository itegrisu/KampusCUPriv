using Application.Features.OfferManagementFeatures.Offers.Commands.Create;
using Application.Features.OfferManagementFeatures.Offers.Commands.Delete;
using Application.Features.OfferManagementFeatures.Offers.Commands.Update;
using Application.Features.OfferManagementFeatures.Offers.Queries.GetByGid;
using Application.Features.OfferManagementFeatures.Offers.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using X = Domain.Entities.OfferManagements;

namespace Application.Features.OfferManagementFeatures.Offers.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<X.Offer, CreateOfferCommand>().ReverseMap();
        CreateMap<X.Offer, CreatedOfferResponse>().ReverseMap();
        CreateMap<X.Offer, UpdateOfferCommand>().ReverseMap();
        CreateMap<X.Offer, UpdatedOfferResponse>().ReverseMap();
        CreateMap<X.Offer, DeleteOfferCommand>().ReverseMap();
        CreateMap<X.Offer, DeletedOfferResponse>().ReverseMap();

		CreateMap<X.Offer, GetByGidOfferResponse>().ReverseMap();

        CreateMap<X.Offer, GetListOfferListItemDto>().ReverseMap();
        CreateMap<IPaginate<X.Offer>, GetListResponse<GetListOfferListItemDto>>().ReverseMap();
    }
}
using Application.Features.OfferManagementFeatures.OfferTransactions.Commands.Create;
using Application.Features.OfferManagementFeatures.OfferTransactions.Commands.Delete;
using Application.Features.OfferManagementFeatures.OfferTransactions.Commands.Update;
using Application.Features.OfferManagementFeatures.OfferTransactions.Queries.GetByGid;
using Application.Features.OfferManagementFeatures.OfferTransactions.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using X = Domain.Entities.OfferManagements;

namespace Application.Features.OfferManagementFeatures.OfferTransactions.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<X.OfferTransaction, CreateOfferTransactionCommand>().ReverseMap();
        CreateMap<X.OfferTransaction, CreatedOfferTransactionResponse>().ReverseMap();
        CreateMap<X.OfferTransaction, UpdateOfferTransactionCommand>().ReverseMap();
        CreateMap<X.OfferTransaction, UpdatedOfferTransactionResponse>().ReverseMap();
        CreateMap<X.OfferTransaction, DeleteOfferTransactionCommand>().ReverseMap();
        CreateMap<X.OfferTransaction, DeletedOfferTransactionResponse>().ReverseMap();

		CreateMap<X.OfferTransaction, GetByGidOfferTransactionResponse>().ReverseMap();

        CreateMap<X.OfferTransaction, GetListOfferTransactionListItemDto>().ReverseMap();
        CreateMap<IPaginate<X.OfferTransaction>, GetListResponse<GetListOfferTransactionListItemDto>>().ReverseMap();
    }
}
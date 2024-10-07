using Application.Features.OfferManagementFeatures.OfferFiles.Commands.Create;
using Application.Features.OfferManagementFeatures.OfferFiles.Commands.Delete;
using Application.Features.OfferManagementFeatures.OfferFiles.Commands.Update;
using Application.Features.OfferManagementFeatures.OfferFiles.Queries.GetByGid;
using Application.Features.OfferManagementFeatures.OfferFiles.Queries.GetByOfferGid;
using Application.Features.OfferManagementFeatures.OfferFiles.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using X = Domain.Entities.OfferManagements;

namespace Application.Features.OfferManagementFeatures.OfferFiles.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<X.OfferFile, CreateOfferFileCommand>().ReverseMap();
        CreateMap<X.OfferFile, CreatedOfferFileResponse>().ReverseMap();
        CreateMap<X.OfferFile, UpdateOfferFileCommand>().ReverseMap();
        CreateMap<X.OfferFile, UpdatedOfferFileResponse>().ReverseMap();
        CreateMap<X.OfferFile, DeleteOfferFileCommand>().ReverseMap();
        CreateMap<X.OfferFile, DeletedOfferFileResponse>().ReverseMap();

        CreateMap<X.OfferFile, GetByGidOfferFileResponse>().ReverseMap();

        CreateMap<X.OfferFile, GetListOfferFileListItemDto>().ReverseMap();
        CreateMap<IPaginate<X.OfferFile>, GetListResponse<GetListOfferFileListItemDto>>().ReverseMap();

        CreateMap<X.OfferFile, GetByOfferGidListOfferFileListItemDto>().ReverseMap();
        CreateMap<IPaginate<X.OfferFile>, GetListResponse<GetByOfferGidListOfferFileListItemDto>>().ReverseMap();
    }
}
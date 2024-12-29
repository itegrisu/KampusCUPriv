using Application.Features.AccommodationManagementFeatures.PartTimeWorkerFiles.Commands.Create;
using Application.Features.AccommodationManagementFeatures.PartTimeWorkerFiles.Commands.Delete;
using Application.Features.AccommodationManagementFeatures.PartTimeWorkerFiles.Commands.Update;
using Application.Features.AccommodationManagementFeatures.PartTimeWorkerFiles.Queries.GetByGid;
using Application.Features.AccommodationManagementFeatures.PartTimeWorkerFiles.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using X = Domain.Entities.AccommodationManagements;

namespace Application.Features.AccommodationManagementFeatures.PartTimeWorkerFiles.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<X.PartTimeWorkerFile, CreatePartTimeWorkerFileCommand>().ReverseMap();
        CreateMap<X.PartTimeWorkerFile, CreatedPartTimeWorkerFileResponse>().ReverseMap();
        CreateMap<X.PartTimeWorkerFile, UpdatePartTimeWorkerFileCommand>().ReverseMap();
        CreateMap<X.PartTimeWorkerFile, UpdatedPartTimeWorkerFileResponse>().ReverseMap();
        CreateMap<X.PartTimeWorkerFile, DeletePartTimeWorkerFileCommand>().ReverseMap();
        CreateMap<X.PartTimeWorkerFile, DeletedPartTimeWorkerFileResponse>().ReverseMap();

		CreateMap<X.PartTimeWorkerFile, GetByGidPartTimeWorkerFileResponse>().ReverseMap();

        CreateMap<X.PartTimeWorkerFile, GetListPartTimeWorkerFileListItemDto>().ReverseMap();
        CreateMap<IPaginate<X.PartTimeWorkerFile>, GetListResponse<GetListPartTimeWorkerFileListItemDto>>().ReverseMap();
    }
}
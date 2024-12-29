using Application.Features.AccommodationManagementFeatures.PartTimeWorkers.Commands.Create;
using Application.Features.AccommodationManagementFeatures.PartTimeWorkers.Commands.Delete;
using Application.Features.AccommodationManagementFeatures.PartTimeWorkers.Commands.Update;
using Application.Features.AccommodationManagementFeatures.PartTimeWorkers.Queries.GetByGid;
using Application.Features.AccommodationManagementFeatures.PartTimeWorkers.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using X = Domain.Entities.AccommodationManagements;

namespace Application.Features.AccommodationManagementFeatures.PartTimeWorkers.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<X.PartTimeWorker, CreatePartTimeWorkerCommand>().ReverseMap();
        CreateMap<X.PartTimeWorker, CreatedPartTimeWorkerResponse>().ReverseMap();
        CreateMap<X.PartTimeWorker, UpdatePartTimeWorkerCommand>().ReverseMap();
        CreateMap<X.PartTimeWorker, UpdatedPartTimeWorkerResponse>().ReverseMap();
        CreateMap<X.PartTimeWorker, DeletePartTimeWorkerCommand>().ReverseMap();
        CreateMap<X.PartTimeWorker, DeletedPartTimeWorkerResponse>().ReverseMap();

		CreateMap<X.PartTimeWorker, GetByGidPartTimeWorkerResponse>().ReverseMap();

        CreateMap<X.PartTimeWorker, GetListPartTimeWorkerListItemDto>().ReverseMap();
        CreateMap<IPaginate<X.PartTimeWorker>, GetListResponse<GetListPartTimeWorkerListItemDto>>().ReverseMap();
    }
}
using Application.Features.DefinitionManagementFeatures.JobTypes.Commands.Create;
using Application.Features.DefinitionManagementFeatures.JobTypes.Commands.Delete;
using Application.Features.DefinitionManagementFeatures.JobTypes.Commands.Update;
using Application.Features.DefinitionManagementFeatures.JobTypes.Queries.GetByGid;
using Application.Features.DefinitionManagementFeatures.JobTypes.Queries.GetById;
using Application.Features.DefinitionManagementFeatures.JobTypes.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using X = Domain.Entities.DefinitionManagements;

namespace Application.Features.DefinitionManagementFeatures.JobTypes.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<X.JobType, CreateJobTypeCommand>().ReverseMap();
        CreateMap<X.JobType, CreatedJobTypeResponse>().ReverseMap();
        CreateMap<X.JobType, UpdateJobTypeCommand>().ReverseMap();
        CreateMap<X.JobType, UpdatedJobTypeResponse>().ReverseMap();
        CreateMap<X.JobType, DeleteJobTypeCommand>().ReverseMap();
        CreateMap<X.JobType, DeletedJobTypeResponse>().ReverseMap();

		CreateMap<X.JobType, GetByGidJobTypeResponse>().ReverseMap();

        CreateMap<X.JobType, GetListJobTypeListItemDto>().ReverseMap();
        CreateMap<IPaginate<X.JobType>, GetListResponse<GetListJobTypeListItemDto>>().ReverseMap();
    }
}
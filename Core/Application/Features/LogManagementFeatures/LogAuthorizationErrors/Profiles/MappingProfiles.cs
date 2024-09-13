using Application.Features.LogManagementFeatures.LogAuthorizationErrors.Commands.Create;
using Application.Features.LogManagementFeatures.LogAuthorizationErrors.Commands.Delete;
using Application.Features.LogManagementFeatures.LogAuthorizationErrors.Commands.Update;
using Application.Features.LogManagementFeatures.LogAuthorizationErrors.Queries.GetByGid;
using Application.Features.LogManagementFeatures.LogAuthorizationErrors.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using X = Domain.Entities.LogManagements;

namespace Application.Features.LogManagementFeatures.LogAuthorizationErrors.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<X.LogAuthorizationError, CreateLogAuthorizationErrorCommand>().ReverseMap();
        CreateMap<X.LogAuthorizationError, CreatedLogAuthorizationErrorResponse>().ReverseMap();
        CreateMap<X.LogAuthorizationError, UpdateLogAuthorizationErrorCommand>().ReverseMap();
        CreateMap<X.LogAuthorizationError, UpdatedLogAuthorizationErrorResponse>().ReverseMap();
        CreateMap<X.LogAuthorizationError, DeleteLogAuthorizationErrorCommand>().ReverseMap();
        CreateMap<X.LogAuthorizationError, DeletedLogAuthorizationErrorResponse>().ReverseMap();

		CreateMap<X.LogAuthorizationError, GetByGidLogAuthorizationErrorResponse>().ReverseMap();

        CreateMap<X.LogAuthorizationError, GetListLogAuthorizationErrorListItemDto>().ReverseMap();
        CreateMap<IPaginate<X.LogAuthorizationError>, GetListResponse<GetListLogAuthorizationErrorListItemDto>>().ReverseMap();
    }
}
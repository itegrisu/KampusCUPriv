using Application.Features.LogManagementFeatures.LogUserPageVisits.Commands.Create;
using Application.Features.LogManagementFeatures.LogUserPageVisits.Commands.Delete;
using Application.Features.LogManagementFeatures.LogUserPageVisits.Commands.Update;
using Application.Features.LogManagementFeatures.LogUserPageVisits.Queries.GetByGid;
using Application.Features.LogManagementFeatures.LogUserPageVisits.Queries.GetBySessionId;
using Application.Features.LogManagementFeatures.LogUserPageVisits.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using X = Domain.Entities.LogManagements;

namespace Application.Features.LogManagementFeatures.LogUserPageVisits.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<X.LogUserPageVisit, CreateLogUserPageVisitCommand>().ReverseMap();
        CreateMap<X.LogUserPageVisit, CreatedLogUserPageVisitResponse>().ReverseMap();
        CreateMap<X.LogUserPageVisit, UpdateLogUserPageVisitCommand>().ReverseMap();
        CreateMap<X.LogUserPageVisit, UpdatedLogUserPageVisitResponse>().ReverseMap();
        CreateMap<X.LogUserPageVisit, DeleteLogUserPageVisitCommand>().ReverseMap();
        CreateMap<X.LogUserPageVisit, DeletedLogUserPageVisitResponse>().ReverseMap();

		CreateMap<X.LogUserPageVisit, GetByGidLogUserPageVisitResponse>().ReverseMap();

        CreateMap<X.LogUserPageVisit, GetListLogUserPageVisitListItemDto>().ReverseMap();
        CreateMap<IPaginate<X.LogUserPageVisit>, GetListResponse<GetListLogUserPageVisitListItemDto>>().ReverseMap();

        CreateMap<X.LogUserPageVisit, GetBySessionIdLogUserPageVisitListItemDto>().ReverseMap();
        CreateMap<IPaginate<X.LogUserPageVisit>,GetListResponse<GetBySessionIdLogUserPageVisitListItemDto>>().ReverseMap();

    }
}
using Application.Features.LogManagementFeatures.LogUserPageVisitActions.Commands.Create;
using Application.Features.LogManagementFeatures.LogUserPageVisitActions.Commands.Delete;
using Application.Features.LogManagementFeatures.LogUserPageVisitActions.Commands.Update;
using Application.Features.LogManagementFeatures.LogUserPageVisitActions.Queries.GetByGid;
using Application.Features.LogManagementFeatures.LogUserPageVisitActions.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using X = Domain.Entities.LogManagements;

namespace Application.Features.LogManagementFeatures.LogUserPageVisitActions.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<X.LogUserPageVisitAction, CreateLogUserPageVisitActionCommand>().ReverseMap();
        CreateMap<X.LogUserPageVisitAction, CreatedLogUserPageVisitActionResponse>().ReverseMap();
        CreateMap<X.LogUserPageVisitAction, UpdateLogUserPageVisitActionCommand>().ReverseMap();
        CreateMap<X.LogUserPageVisitAction, UpdatedLogUserPageVisitActionResponse>().ReverseMap();
        CreateMap<X.LogUserPageVisitAction, DeleteLogUserPageVisitActionCommand>().ReverseMap();
        CreateMap<X.LogUserPageVisitAction, DeletedLogUserPageVisitActionResponse>().ReverseMap();

        CreateMap<X.LogUserPageVisitAction, GetByGidLogUserPageVisitActionResponse>().ReverseMap();

        CreateMap<X.LogUserPageVisitAction, GetListLogUserPageVisitActionListItemDto>()
            .ForMember(dest => dest.OperationQuery, opt => opt.MapFrom(src => src.Operation)).ReverseMap();


        CreateMap<IPaginate<X.LogUserPageVisitAction>, GetListResponse<GetListLogUserPageVisitActionListItemDto>>().ReverseMap();


    }
}
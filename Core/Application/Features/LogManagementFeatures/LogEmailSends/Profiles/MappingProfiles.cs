using Application.Features.LogManagementFeatures.LogEmailSends.Commands.Create;
using Application.Features.LogManagementFeatures.LogEmailSends.Commands.Delete;
using Application.Features.LogManagementFeatures.LogEmailSends.Commands.Update;
using Application.Features.LogManagementFeatures.LogEmailSends.Queries.GetByGid;
using Application.Features.LogManagementFeatures.LogEmailSends.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using X = Domain.Entities.LogManagements;

namespace Application.Features.LogManagementFeatures.LogEmailSends.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<X.LogEmailSend, CreateLogEmailSendCommand>().ReverseMap();
        CreateMap<X.LogEmailSend, CreatedLogEmailSendResponse>().ReverseMap();
        CreateMap<X.LogEmailSend, UpdateLogEmailSendCommand>().ReverseMap();
        CreateMap<X.LogEmailSend, UpdatedLogEmailSendResponse>().ReverseMap();
        CreateMap<X.LogEmailSend, DeleteLogEmailSendCommand>().ReverseMap();
        CreateMap<X.LogEmailSend, DeletedLogEmailSendResponse>().ReverseMap();

		CreateMap<X.LogEmailSend, GetByGidLogEmailSendResponse>().ReverseMap();

        CreateMap<X.LogEmailSend, GetListLogEmailSendListItemDto>().ReverseMap();
        CreateMap<IPaginate<X.LogEmailSend>, GetListResponse<GetListLogEmailSendListItemDto>>().ReverseMap();
    }
}
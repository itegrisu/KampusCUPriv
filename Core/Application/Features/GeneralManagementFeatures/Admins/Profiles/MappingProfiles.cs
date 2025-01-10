using Application.Features.GeneralFeatures.Admins.Commands.Create;
using Application.Features.GeneralFeatures.Admins.Commands.Delete;
using Application.Features.GeneralFeatures.Admins.Commands.Update;
using Application.Features.GeneralFeatures.Admins.Queries.GetByGid;
using Application.Features.GeneralFeatures.Admins.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using X = Domain.Entities.GeneralManagements;

namespace Application.Features.GeneralFeatures.Admins.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<X.Admin, CreateAdminCommand>().ReverseMap();
        CreateMap<X.Admin, CreatedAdminResponse>().ReverseMap();
        CreateMap<X.Admin, UpdateAdminCommand>().ReverseMap();
        CreateMap<X.Admin, UpdatedAdminResponse>().ReverseMap();
        CreateMap<X.Admin, DeleteAdminCommand>().ReverseMap();
        CreateMap<X.Admin, DeletedAdminResponse>().ReverseMap();

		CreateMap<X.Admin, GetByGidAdminResponse>().ReverseMap();

        CreateMap<X.Admin, GetListAdminListItemDto>().ReverseMap();
        CreateMap<IPaginate<X.Admin>, GetListResponse<GetListAdminListItemDto>>().ReverseMap();
    }
}
using Application.Features.SupplierCustomerManagementFeatures.SCPersonnels.Commands.Create;
using Application.Features.SupplierCustomerManagementFeatures.SCPersonnels.Commands.Delete;
using Application.Features.SupplierCustomerManagementFeatures.SCPersonnels.Commands.Update;
using Application.Features.SupplierCustomerManagementFeatures.SCPersonnels.Queries.GetByGid;
using Application.Features.SupplierCustomerManagementFeatures.SCPersonnels.Queries.GetList;
using Application.Features.SupplierManagementFeatures.SCPersonnels.Queries.GetByCompanyGid;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using X = Domain.Entities.SupplierCustomerManagements;

namespace Application.Features.SupplierCustomerManagementFeatures.SCPersonnels.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<X.SCPersonnel, CreateSCPersonnelCommand>().ReverseMap();
        CreateMap<X.SCPersonnel, CreatedSCPersonnelResponse>().ReverseMap();
        CreateMap<X.SCPersonnel, UpdateSCPersonnelCommand>().ReverseMap();
        CreateMap<X.SCPersonnel, UpdatedSCPersonnelResponse>().ReverseMap();
        CreateMap<X.SCPersonnel, DeleteSCPersonnelCommand>().ReverseMap();
        CreateMap<X.SCPersonnel, DeletedSCPersonnelResponse>().ReverseMap();

		CreateMap<X.SCPersonnel, GetByGidSCPersonnelResponse>().ReverseMap();

        CreateMap<X.SCPersonnel, GetListSCPersonnelListItemDto>().ReverseMap();
        CreateMap<IPaginate<X.SCPersonnel>, GetListResponse<GetListSCPersonnelListItemDto>>().ReverseMap();

        CreateMap<X.SCPersonnel, GetByCompanyGidListSCPersonnelListItemDto>().ReverseMap();
        CreateMap<IPaginate<X.SCPersonnel>, GetListResponse<GetByCompanyGidListSCPersonnelListItemDto>>().ReverseMap();
    }
}
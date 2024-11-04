using Application.Features.SupplierCustomerManagementFeatures.SCAddresses.Commands.Create;
using Application.Features.SupplierCustomerManagementFeatures.SCAddresses.Commands.Delete;
using Application.Features.SupplierCustomerManagementFeatures.SCAddresses.Commands.Update;
using Application.Features.SupplierCustomerManagementFeatures.SCAddresses.Queries.GetByGid;
using Application.Features.SupplierCustomerManagementFeatures.SCAddresses.Queries.GetList;
using Application.Features.SupplierManagementFeatures.SCAddresses.Queries.GetByCompanyGid;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using X = Domain.Entities.SupplierCustomerManagements;

namespace Application.Features.SupplierCustomerManagementFeatures.SCAddresses.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<X.SCAddress, CreateSCAddressCommand>().ReverseMap();
        CreateMap<X.SCAddress, CreatedSCAddressResponse>().ReverseMap();
        CreateMap<X.SCAddress, UpdateSCAddressCommand>().ReverseMap();
        CreateMap<X.SCAddress, UpdatedSCAddressResponse>().ReverseMap();
        CreateMap<X.SCAddress, DeleteSCAddressCommand>().ReverseMap();
        CreateMap<X.SCAddress, DeletedSCAddressResponse>().ReverseMap();

		CreateMap<X.SCAddress, GetByGidSCAddressResponse>().ReverseMap();

        CreateMap<X.SCAddress, GetListSCAddressListItemDto>().ReverseMap();
        CreateMap<IPaginate<X.SCAddress>, GetListResponse<GetListSCAddressListItemDto>>().ReverseMap();

        CreateMap<X.SCAddress, GetByCompanyGidListSCAddressListItemDto>().ReverseMap();
        CreateMap<IPaginate<X.SCAddress>, GetListResponse<GetByCompanyGidListSCAddressListItemDto>>().ReverseMap();
    }
}
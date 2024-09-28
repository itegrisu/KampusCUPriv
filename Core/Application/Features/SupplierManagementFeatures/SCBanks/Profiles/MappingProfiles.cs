using Application.Features.SupplierCustomerManagementFeatures.SCBanks.Commands.Create;
using Application.Features.SupplierCustomerManagementFeatures.SCBanks.Commands.Delete;
using Application.Features.SupplierCustomerManagementFeatures.SCBanks.Commands.Update;
using Application.Features.SupplierCustomerManagementFeatures.SCBanks.Queries.GetByGid;
using Application.Features.SupplierCustomerManagementFeatures.SCBanks.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using X = Domain.Entities.SupplierCustomerManagements;

namespace Application.Features.SupplierCustomerManagementFeatures.SCBanks.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<X.SCBank, CreateSCBankCommand>().ReverseMap();
        CreateMap<X.SCBank, CreatedSCBankResponse>().ReverseMap();
        CreateMap<X.SCBank, UpdateSCBankCommand>().ReverseMap();
        CreateMap<X.SCBank, UpdatedSCBankResponse>().ReverseMap();
        CreateMap<X.SCBank, DeleteSCBankCommand>().ReverseMap();
        CreateMap<X.SCBank, DeletedSCBankResponse>().ReverseMap();

		CreateMap<X.SCBank, GetByGidSCBankResponse>().ReverseMap();

        CreateMap<X.SCBank, GetListSCBankListItemDto>().ReverseMap();
        CreateMap<IPaginate<X.SCBank>, GetListResponse<GetListSCBankListItemDto>>().ReverseMap();
    }
}
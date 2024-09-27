using Application.Features.SupplierCustomerManagementFeatures.SCCompanies.Commands.Create;
using Application.Features.SupplierCustomerManagementFeatures.SCCompanies.Commands.Delete;
using Application.Features.SupplierCustomerManagementFeatures.SCCompanies.Commands.Update;
using Application.Features.SupplierCustomerManagementFeatures.SCCompanies.Queries.GetByGid;
using Application.Features.SupplierCustomerManagementFeatures.SCCompanies.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using X = Domain.Entities.SupplierCustomerManagements;

namespace Application.Features.SupplierCustomerManagementFeatures.SCCompanies.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<X.SCCompany, CreateSCCompanyCommand>().ReverseMap();
        CreateMap<X.SCCompany, CreatedSCCompanyResponse>().ReverseMap();
        CreateMap<X.SCCompany, UpdateSCCompanyCommand>().ReverseMap();
        CreateMap<X.SCCompany, UpdatedSCCompanyResponse>().ReverseMap();
        CreateMap<X.SCCompany, DeleteSCCompanyCommand>().ReverseMap();
        CreateMap<X.SCCompany, DeletedSCCompanyResponse>().ReverseMap();

		CreateMap<X.SCCompany, GetByGidSCCompanyResponse>().ReverseMap();

        CreateMap<X.SCCompany, GetListSCCompanyListItemDto>().ReverseMap();
        CreateMap<IPaginate<X.SCCompany>, GetListResponse<GetListSCCompanyListItemDto>>().ReverseMap();
    }
}
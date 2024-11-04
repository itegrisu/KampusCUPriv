using Application.Features.SupplierCustomerManagementFeatures.SCEmployers.Commands.Create;
using Application.Features.SupplierCustomerManagementFeatures.SCEmployers.Commands.Delete;
using Application.Features.SupplierCustomerManagementFeatures.SCEmployers.Commands.Update;
using Application.Features.SupplierCustomerManagementFeatures.SCEmployers.Queries.GetByGid;
using Application.Features.SupplierCustomerManagementFeatures.SCEmployers.Queries.GetList;
using Application.Features.SupplierManagementFeatures.SCEmployers.Queries.GetByCompanyGid;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using X = Domain.Entities.SupplierCustomerManagements;

namespace Application.Features.SupplierCustomerManagementFeatures.SCEmployers.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<X.SCEmployer, CreateSCEmployerCommand>().ReverseMap();
        CreateMap<X.SCEmployer, CreatedSCEmployerResponse>().ReverseMap();
        CreateMap<X.SCEmployer, UpdateSCEmployerCommand>().ReverseMap();
        CreateMap<X.SCEmployer, UpdatedSCEmployerResponse>().ReverseMap();
        CreateMap<X.SCEmployer, DeleteSCEmployerCommand>().ReverseMap();
        CreateMap<X.SCEmployer, DeletedSCEmployerResponse>().ReverseMap();

		CreateMap<X.SCEmployer, GetByGidSCEmployerResponse>().ReverseMap();

        CreateMap<X.SCEmployer, GetListSCEmployerListItemDto>().ReverseMap();
        CreateMap<IPaginate<X.SCEmployer>, GetListResponse<GetListSCEmployerListItemDto>>().ReverseMap();

        CreateMap<X.SCEmployer, GetByCompanyGidListSCEmployerListItemDto>().ReverseMap();
        CreateMap<IPaginate<X.SCEmployer>, GetListResponse<GetByCompanyGidListSCEmployerListItemDto>>().ReverseMap();
    }
}
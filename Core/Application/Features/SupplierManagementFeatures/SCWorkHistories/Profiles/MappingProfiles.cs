using Application.Features.SupplierCustomerManagementFeatures.SCWorkHistories.Commands.Create;
using Application.Features.SupplierCustomerManagementFeatures.SCWorkHistories.Commands.Delete;
using Application.Features.SupplierCustomerManagementFeatures.SCWorkHistories.Commands.Update;
using Application.Features.SupplierCustomerManagementFeatures.SCWorkHistories.Queries.GetByGid;
using Application.Features.SupplierCustomerManagementFeatures.SCWorkHistories.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using X = Domain.Entities.SupplierCustomerManagements;

namespace Application.Features.SupplierCustomerManagementFeatures.SCWorkHistories.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<X.SCWorkHistory, CreateSCWorkHistoryCommand>().ReverseMap();
        CreateMap<X.SCWorkHistory, CreatedSCWorkHistoryResponse>().ReverseMap();
        CreateMap<X.SCWorkHistory, UpdateSCWorkHistoryCommand>().ReverseMap();
        CreateMap<X.SCWorkHistory, UpdatedSCWorkHistoryResponse>().ReverseMap();
        CreateMap<X.SCWorkHistory, DeleteSCWorkHistoryCommand>().ReverseMap();
        CreateMap<X.SCWorkHistory, DeletedSCWorkHistoryResponse>().ReverseMap();

		CreateMap<X.SCWorkHistory, GetByGidSCWorkHistoryResponse>().ReverseMap();

        CreateMap<X.SCWorkHistory, GetListSCWorkHistoryListItemDto>().ReverseMap();
        CreateMap<IPaginate<X.SCWorkHistory>, GetListResponse<GetListSCWorkHistoryListItemDto>>().ReverseMap();
    }
}
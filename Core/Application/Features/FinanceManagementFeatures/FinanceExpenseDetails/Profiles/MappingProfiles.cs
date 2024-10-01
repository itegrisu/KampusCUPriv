using Application.Features.FinanceManagementFeatures.FinanceExpenseDetails.Commands.Create;
using Application.Features.FinanceManagementFeatures.FinanceExpenseDetails.Commands.Delete;
using Application.Features.FinanceManagementFeatures.FinanceExpenseDetails.Commands.Update;
using Application.Features.FinanceManagementFeatures.FinanceExpenseDetails.Queries.GetByGid;
using Application.Features.FinanceManagementFeatures.FinanceExpenseDetails.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using X = Domain.Entities.FinanceManagements;

namespace Application.Features.FinanceManagementFeatures.FinanceExpenseDetails.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<X.FinanceExpenseDetail, CreateFinanceExpenseDetailCommand>().ReverseMap();
        CreateMap<X.FinanceExpenseDetail, CreatedFinanceExpenseDetailResponse>().ReverseMap();
        CreateMap<X.FinanceExpenseDetail, UpdateFinanceExpenseDetailCommand>().ReverseMap();
        CreateMap<X.FinanceExpenseDetail, UpdatedFinanceExpenseDetailResponse>().ReverseMap();
        CreateMap<X.FinanceExpenseDetail, DeleteFinanceExpenseDetailCommand>().ReverseMap();
        CreateMap<X.FinanceExpenseDetail, DeletedFinanceExpenseDetailResponse>().ReverseMap();

		CreateMap<X.FinanceExpenseDetail, GetByGidFinanceExpenseDetailResponse>().ReverseMap();

        CreateMap<X.FinanceExpenseDetail, GetListFinanceExpenseDetailListItemDto>().ReverseMap();
        CreateMap<IPaginate<X.FinanceExpenseDetail>, GetListResponse<GetListFinanceExpenseDetailListItemDto>>().ReverseMap();
    }
}
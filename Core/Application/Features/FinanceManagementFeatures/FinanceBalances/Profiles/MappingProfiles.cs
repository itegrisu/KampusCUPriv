using Application.Features.FinanceManagementFeatures.FinanceBalances.Commands.Create;
using Application.Features.FinanceManagementFeatures.FinanceBalances.Commands.Delete;
using Application.Features.FinanceManagementFeatures.FinanceBalances.Commands.Update;
using Application.Features.FinanceManagementFeatures.FinanceBalances.Queries.GetByGid;
using Application.Features.FinanceManagementFeatures.FinanceBalances.Queries.GetBySCGidWithDateRange;
using Application.Features.FinanceManagementFeatures.FinanceBalances.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using X = Domain.Entities.FinanceManagements;

namespace Application.Features.FinanceManagementFeatures.FinanceBalances.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<X.FinanceBalance, CreateFinanceBalanceCommand>().ReverseMap();
        CreateMap<X.FinanceBalance, CreatedFinanceBalanceResponse>().ReverseMap();
        CreateMap<X.FinanceBalance, UpdateFinanceBalanceCommand>().ReverseMap();
        CreateMap<X.FinanceBalance, UpdatedFinanceBalanceResponse>().ReverseMap();
        CreateMap<X.FinanceBalance, DeleteFinanceBalanceCommand>().ReverseMap();
        CreateMap<X.FinanceBalance, DeletedFinanceBalanceResponse>().ReverseMap();

		CreateMap<X.FinanceBalance, GetByGidFinanceBalanceResponse>().ReverseMap();

        CreateMap<X.FinanceBalance, GetListFinanceBalanceListItemDto>().ReverseMap();
        CreateMap<IPaginate<X.FinanceBalance>, GetListResponse<GetListFinanceBalanceListItemDto>>().ReverseMap();

        CreateMap<X.FinanceBalance, GetBySCGidWithDateRangeListFinanceBalanceListItemDto>().ReverseMap();
        CreateMap<IPaginate<X.FinanceBalance>, GetListResponse<GetBySCGidWithDateRangeListFinanceBalanceListItemDto>>().ReverseMap();
    }
}
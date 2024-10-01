using Application.Features.FinanceManagementFeatures.FinanceExpenses.Commands.Create;
using Application.Features.FinanceManagementFeatures.FinanceExpenses.Commands.Delete;
using Application.Features.FinanceManagementFeatures.FinanceExpenses.Commands.Update;
using Application.Features.FinanceManagementFeatures.FinanceExpenses.Queries.GetByGid;
using Application.Features.FinanceManagementFeatures.FinanceExpenses.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using X = Domain.Entities.FinanceManagements;

namespace Application.Features.FinanceManagementFeatures.FinanceExpenses.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<X.FinanceExpense, CreateFinanceExpenseCommand>().ReverseMap();
        CreateMap<X.FinanceExpense, CreatedFinanceExpenseResponse>().ReverseMap();
        CreateMap<X.FinanceExpense, UpdateFinanceExpenseCommand>().ReverseMap();
        CreateMap<X.FinanceExpense, UpdatedFinanceExpenseResponse>().ReverseMap();
        CreateMap<X.FinanceExpense, DeleteFinanceExpenseCommand>().ReverseMap();
        CreateMap<X.FinanceExpense, DeletedFinanceExpenseResponse>().ReverseMap();

		CreateMap<X.FinanceExpense, GetByGidFinanceExpenseResponse>().ReverseMap();

        CreateMap<X.FinanceExpense, GetListFinanceExpenseListItemDto>().ReverseMap();
        CreateMap<IPaginate<X.FinanceExpense>, GetListResponse<GetListFinanceExpenseListItemDto>>().ReverseMap();
    }
}
using Application.Features.FinanceManagementFeatures.FinanceIncomes.Commands.Create;
using Application.Features.FinanceManagementFeatures.FinanceIncomes.Commands.Delete;
using Application.Features.FinanceManagementFeatures.FinanceIncomes.Commands.Update;
using Application.Features.FinanceManagementFeatures.FinanceIncomes.Queries.GetByGid;
using Application.Features.FinanceManagementFeatures.FinanceIncomes.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using X = Domain.Entities.FinanceManagements;

namespace Application.Features.FinanceManagementFeatures.FinanceIncomes.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<X.FinanceIncome, CreateFinanceIncomeCommand>().ReverseMap();
        CreateMap<X.FinanceIncome, CreatedFinanceIncomeResponse>().ReverseMap();
        CreateMap<X.FinanceIncome, UpdateFinanceIncomeCommand>().ReverseMap();
        CreateMap<X.FinanceIncome, UpdatedFinanceIncomeResponse>().ReverseMap();
        CreateMap<X.FinanceIncome, DeleteFinanceIncomeCommand>().ReverseMap();
        CreateMap<X.FinanceIncome, DeletedFinanceIncomeResponse>().ReverseMap();

		CreateMap<X.FinanceIncome, GetByGidFinanceIncomeResponse>().ReverseMap();

        CreateMap<X.FinanceIncome, GetListFinanceIncomeListItemDto>().ReverseMap();
        CreateMap<IPaginate<X.FinanceIncome>, GetListResponse<GetListFinanceIncomeListItemDto>>().ReverseMap();
    }
}
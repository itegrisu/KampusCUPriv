using Application.Features.FinanceManagementFeatures.FinanceIncomeGroups.Commands.Create;
using Application.Features.FinanceManagementFeatures.FinanceIncomeGroups.Commands.Delete;
using Application.Features.FinanceManagementFeatures.FinanceIncomeGroups.Commands.Update;
using Application.Features.FinanceManagementFeatures.FinanceIncomeGroups.Queries.GetByGid;
using Application.Features.FinanceManagementFeatures.FinanceIncomeGroups.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using X = Domain.Entities.FinanceManagements;

namespace Application.Features.FinanceManagementFeatures.FinanceIncomeGroups.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<X.FinanceIncomeGroup, CreateFinanceIncomeGroupCommand>().ReverseMap();
        CreateMap<X.FinanceIncomeGroup, CreatedFinanceIncomeGroupResponse>().ReverseMap();
        CreateMap<X.FinanceIncomeGroup, UpdateFinanceIncomeGroupCommand>().ReverseMap();
        CreateMap<X.FinanceIncomeGroup, UpdatedFinanceIncomeGroupResponse>().ReverseMap();
        CreateMap<X.FinanceIncomeGroup, DeleteFinanceIncomeGroupCommand>().ReverseMap();
        CreateMap<X.FinanceIncomeGroup, DeletedFinanceIncomeGroupResponse>().ReverseMap();

		CreateMap<X.FinanceIncomeGroup, GetByGidFinanceIncomeGroupResponse>().ReverseMap();

        CreateMap<X.FinanceIncomeGroup, GetListFinanceIncomeGroupListItemDto>().ReverseMap();
        CreateMap<IPaginate<X.FinanceIncomeGroup>, GetListResponse<GetListFinanceIncomeGroupListItemDto>>().ReverseMap();
    }
}
using Application.Features.FinanceManagementFeatures.FinanceExpenseGroups.Commands.Create;
using Application.Features.FinanceManagementFeatures.FinanceExpenseGroups.Commands.Delete;
using Application.Features.FinanceManagementFeatures.FinanceExpenseGroups.Commands.Update;
using Application.Features.FinanceManagementFeatures.FinanceExpenseGroups.Queries.GetByGid;
using Application.Features.FinanceManagementFeatures.FinanceExpenseGroups.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using X = Domain.Entities.FinanceManagements;

namespace Application.Features.FinanceManagementFeatures.FinanceExpenseGroups.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<X.FinanceExpenseGroup, CreateFinanceExpenseGroupCommand>().ReverseMap();
        CreateMap<X.FinanceExpenseGroup, CreatedFinanceExpenseGroupResponse>().ReverseMap();
        CreateMap<X.FinanceExpenseGroup, UpdateFinanceExpenseGroupCommand>().ReverseMap();
        CreateMap<X.FinanceExpenseGroup, UpdatedFinanceExpenseGroupResponse>().ReverseMap();
        CreateMap<X.FinanceExpenseGroup, DeleteFinanceExpenseGroupCommand>().ReverseMap();
        CreateMap<X.FinanceExpenseGroup, DeletedFinanceExpenseGroupResponse>().ReverseMap();

		CreateMap<X.FinanceExpenseGroup, GetByGidFinanceExpenseGroupResponse>().ReverseMap();

        CreateMap<X.FinanceExpenseGroup, GetListFinanceExpenseGroupListItemDto>().ReverseMap();
        CreateMap<IPaginate<X.FinanceExpenseGroup>, GetListResponse<GetListFinanceExpenseGroupListItemDto>>().ReverseMap();
    }
}
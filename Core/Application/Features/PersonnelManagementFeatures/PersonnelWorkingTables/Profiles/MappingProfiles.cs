using Application.Features.PersonnelManagementFeatures.PersonnelWorkingTables.Commands.Create;
using Application.Features.PersonnelManagementFeatures.PersonnelWorkingTables.Commands.Delete;
using Application.Features.PersonnelManagementFeatures.PersonnelWorkingTables.Commands.Update;
using Application.Features.PersonnelManagementFeatures.PersonnelWorkingTables.Queries.GetByGid;
using Application.Features.PersonnelManagementFeatures.PersonnelWorkingTables.Queries.GetById;
using Application.Features.PersonnelManagementFeatures.PersonnelWorkingTables.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using X = Domain.Entities.PersonnelManagements;

namespace Application.Features.PersonnelManagementFeatures.PersonnelWorkingTables.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<X.PersonnelWorkingTable, CreatePersonnelWorkingTableCommand>().ReverseMap();
        CreateMap<X.PersonnelWorkingTable, CreatedPersonnelWorkingTableResponse>().ReverseMap();
        CreateMap<X.PersonnelWorkingTable, UpdatePersonnelWorkingTableCommand>().ReverseMap();
        CreateMap<X.PersonnelWorkingTable, UpdatedPersonnelWorkingTableResponse>().ReverseMap();
        CreateMap<X.PersonnelWorkingTable, DeletePersonnelWorkingTableCommand>().ReverseMap();
        CreateMap<X.PersonnelWorkingTable, DeletedPersonnelWorkingTableResponse>().ReverseMap();

		CreateMap<X.PersonnelWorkingTable, GetByGidPersonnelWorkingTableResponse>().ReverseMap();

        CreateMap<X.PersonnelWorkingTable, GetListPersonnelWorkingTableListItemDto>().ReverseMap();
        CreateMap<IPaginate<X.PersonnelWorkingTable>, GetListResponse<GetListPersonnelWorkingTableListItemDto>>().ReverseMap();
    }
}
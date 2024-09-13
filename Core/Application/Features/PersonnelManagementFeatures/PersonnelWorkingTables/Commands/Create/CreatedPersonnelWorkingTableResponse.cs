using Application.Features.Base;
using Application.Features.PersonnelManagementFeatures.PersonnelWorkingTables.Queries.GetByGid;
using Core.Application.Responses;
using System.Configuration;

namespace Application.Features.PersonnelManagementFeatures.PersonnelWorkingTables.Commands.Create;

public class CreatedPersonnelWorkingTableResponse : BaseResponse, IResponse
{
    public GetByGidPersonnelWorkingTableResponse Obj { get; set; }
}
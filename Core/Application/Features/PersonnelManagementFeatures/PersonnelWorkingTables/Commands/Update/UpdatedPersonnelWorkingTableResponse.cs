using Application.Features.Base;
using Application.Features.PersonnelManagementFeatures.PersonnelWorkingTables.Queries.GetByGid;
using Core.Application.Responses;

namespace Application.Features.PersonnelManagementFeatures.PersonnelWorkingTables.Commands.Update;

public class UpdatedPersonnelWorkingTableResponse : BaseResponse, IResponse
{
    public GetByGidPersonnelWorkingTableResponse Obj { get; set; }
}
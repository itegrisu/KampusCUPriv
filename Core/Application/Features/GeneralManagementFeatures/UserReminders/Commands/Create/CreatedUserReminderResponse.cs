using Application.Features.Base;
using Application.Features.GeneralManagementFeatures.UserReminders.Queries.GetByGid;
using Core.Application.Responses;
using System.Configuration;

namespace Application.Features.GeneralManagementFeatures.UserReminders.Commands.Create;

public class CreatedUserReminderResponse : BaseResponse, IResponse
{
    public GetByGidUserReminderResponse Obj { get; set; }
}
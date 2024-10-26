using Application.Features.Base;
using Application.Features.GeneralManagementFeatures.UserReminders.Queries.GetByGid;
using Core.Application.Responses;

namespace Application.Features.GeneralManagementFeatures.UserReminders.Commands.Update;

public class UpdatedUserReminderResponse : BaseResponse, IResponse
{
    public GetByGidUserReminderResponse Obj { get; set; }
}
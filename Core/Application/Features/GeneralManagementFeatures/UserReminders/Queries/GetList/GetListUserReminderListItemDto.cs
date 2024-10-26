using Core.Application.Dtos;
using Core.Enum;
using Domain.Enums;

namespace Application.Features.GeneralManagementFeatures.UserReminders.Queries.GetList;

public class GetListUserReminderListItemDto : IDto
{
    public Guid Gid { get; set; }
    public Guid GidUserFK { get; set; }
    public string UserFKFullName { get; set; }
    public DateTime Date { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }
    public string? Document { get; set; }
    public EnumReminderType ReminderType { get; set; }


}
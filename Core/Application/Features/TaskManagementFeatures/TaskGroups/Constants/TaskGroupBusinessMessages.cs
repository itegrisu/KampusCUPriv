namespace Application.Features.TaskManagementFeatures.TaskGroups.Constants;

public static class TaskGroupsBusinessMessages
{
    public const string SectionName = "TaskGroup";

    public const string TaskGroupNotExists = "Task Group Not Exists";
    public const string ProcessCompleted = "Process Completed";

    public const string SuccessCreatedTaskGroupMessage = "Task Group Successfully Created";
    public const string SuccessDeletedTaskGroupMessage = "Task Group Successfully Deleted";
    public const string SuccessUpdatedTaskGroupMessage = "Task Group Successfully Updated";
    
	//public const string SuccessMovedRecord = "Selected Record Successfuly Moved";
	//public const string Ranked1stError = "The record you want to move up is ranked 1st";
	//public const string RankedLastError = "The record you want to move down is in the last row";

    public const string TechnicalError = "Technical Error";
	public const string NotFoundRecord = "No Record Found!";
	public const string IncorrectOperation = "Incorrect Operation";
    //public const string IdNumberAlreadyExists = "This Gid Number is Already Registered in the System";

    public const string TaskGroupHasTaskGroupUser = "Task Group Has User";
    public const string UserNotExists = "User Not Exists";
}
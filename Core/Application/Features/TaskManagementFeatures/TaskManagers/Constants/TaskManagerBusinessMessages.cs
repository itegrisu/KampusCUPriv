namespace Application.Features.TaskManagementFeatures.TaskManagers.Constants;

public static class TaskManagersBusinessMessages
{
    public const string SectionName = "TaskManager";

    public const string TaskManagerNotExists = "TaskManager Not Exists";
    public const string ProcessCompleted = "Process Completed";

    public const string SuccessCreatedTaskManagerMessage = "TaskManager Successfully Created";
    public const string SuccessDeletedTaskManagerMessage = "TaskManager Successfully Deleted";
    public const string SuccessUpdatedTaskManagerMessage = "TaskManager Successfully Updated";

    //public const string SuccessMovedRecord = "Selected Record Successfuly Moved";
    //public const string Ranked1stError = "The record you want to move up is ranked 1st";
    //public const string RankedLastError = "The record you want to move down is in the last row";

    public const string TechnicalError = "Technical Error";
    public const string NotFoundRecord = "No Record Found!";
    public const string IncorrectOperation = "Incorrect Operation";
    //public const string IdNumberAlreadyExists = "This Gid Number is Already Registered in the System";

    public const string UserNotExists = "User Not Exists";
    public const string TaskManagerAlreadyExist = "Task Manager Already Exist";
    public const string TaskGroupNotExists = "Task Group Not Exists";
    public const string UserIsNotTaskManager = "User Is Not Task Manager";
}
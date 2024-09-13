namespace Application.Features.LogManagementFeatures.LogAuthorizationErrors.Constants;

public static class LogAuthorizationErrorsBusinessMessages
{
    public const string SectionName = "LogAuthorizationError";

    public const string LogAuthorizationErrorNotExists = "LogAuthorizationError Not Exists";
    public const string ProcessCompleted = "Process Completed";

    public const string SuccessCreatedLogAuthorizationErrorMessage = "LogAuthorizationError Successfully Created";
    public const string SuccessDeletedLogAuthorizationErrorMessage = "LogAuthorizationError Successfully Deleted";
    public const string SuccessUpdatedLogAuthorizationErrorMessage = "LogAuthorizationError Successfully Updated";
    
	//public const string SuccessMovedRecord = "Selected Record Successfuly Moved";
	//public const string Ranked1stError = "The record you want to move up is ranked 1st";
	//public const string RankedLastError = "The record you want to move down is in the last row";

    public const string TechnicalError = "Technical Error";
	public const string NotFoundRecord = "No Record Found!";
	public const string IncorrectOperation = "Incorrect Operation";
	//public const string IdNumberAlreadyExists = "This Id Number is Already Registered in the System";
}
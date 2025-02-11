namespace Application.Features.GeneralFeatures.Users.Constants;

public static class UsersBusinessMessages
{
    public const string SectionName = "User";

    public const string UserNotExists = "User Not Exists";
    public const string ProcessCompleted = "Process Completed";

    public const string SuccessCreatedUserMessage = "User Successfully Created";
    public const string SuccessDeletedUserMessage = "User Successfully Deleted";
    public const string SuccessUpdatedUserMessage = "User Successfully Updated";
    
	//public const string SuccessMovedRecord = "Selected Record Successfuly Moved";
	//public const string Ranked1stError = "The record you want to move up is ranked 1st";
	//public const string RankedLastError = "The record you want to move down is in the last row";

    public const string TechnicalError = "Technical Error";
	public const string NotFoundRecord = "No Record Found!";
	public const string IncorrectOperation = "Incorrect Operation";
    //public const string IdNumberAlreadyExists = "This Id Number is Already Registered in the System";
    public const string IncorrectAvatarImageMessage = "Avatar Image Type Is Not In The Allowed File Type";
    public const string FileReadyForUpload = "The File Is Ready For To Be Uploaded To The System";
    //InvalidCode
    public const string InvalidCode = "Invalid Code";
    //UserAlreadyExists
    public const string UserAlreadyExists = "User Already Exists";
}
namespace Application.Features.ClubFeatures.StudentClubs.Constants;

public static class StudentClubsBusinessMessages
{
    public const string SectionName = "StudentClub";

    public const string StudentClubNotExists = "StudentClub Not Exists";
    public const string ProcessCompleted = "Process Completed";

    public const string SuccessCreatedStudentClubMessage = "StudentClub Successfully Created";
    public const string SuccessDeletedStudentClubMessage = "StudentClub Successfully Deleted";
    public const string SuccessUpdatedStudentClubMessage = "StudentClub Successfully Updated";
    
	//public const string SuccessMovedRecord = "Selected Record Successfuly Moved";
	//public const string Ranked1stError = "The record you want to move up is ranked 1st";
	//public const string RankedLastError = "The record you want to move down is in the last row";

    public const string TechnicalError = "Technical Error";
	public const string NotFoundRecord = "No Record Found!";
	public const string IncorrectOperation = "Incorrect Operation";
    //public const string IdNumberAlreadyExists = "This Id Number is Already Registered in the System";
    //UserAlreadyInClub
    public const string UserAlreadyInClub = "User Already In Club";
}
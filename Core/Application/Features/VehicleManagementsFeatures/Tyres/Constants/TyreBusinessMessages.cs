namespace Application.Features.VehicleManagementFeatures.Tyres.Constants;

public static class TyresBusinessMessages
{
    public const string SectionName = "Tyre";

    public const string TyreNotExists = "Tyre Not Exists";
    public const string ProcessCompleted = "Process Completed";

    public const string SuccessCreatedTyreMessage = "Tyre Successfully Created";
    public const string SuccessDeletedTyreMessage = "Tyre Successfully Deleted";
    public const string SuccessUpdatedTyreMessage = "Tyre Successfully Updated";
    
	//public const string SuccessMovedRecord = "Selected Record Successfuly Moved";
	//public const string Ranked1stError = "The record you want to move up is ranked 1st";
	//public const string RankedLastError = "The record you want to move down is in the last row";

    public const string TechnicalError = "Technical Error";
	public const string NotFoundRecord = "No Record Found!";
	public const string IncorrectOperation = "Incorrect Operation";
    //public const string IdNumberAlreadyExists = "This Id Number is Already Registered in the System";
    // Bu lastik türü zaten mevcut TyreTypeAllreadyExist
    public const string TyreNoAllreadyExist = " Bu lastik numarasý zaten mevcut";

}
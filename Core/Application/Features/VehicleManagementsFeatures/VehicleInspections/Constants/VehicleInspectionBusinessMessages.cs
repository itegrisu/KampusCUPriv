namespace Application.Features.VehicleManagementFeatures.VehicleInspections.Constants;

public static class VehicleInspectionsBusinessMessages
{
    public const string SectionName = "VehicleInspection";

    public const string VehicleInspectionNotExists = "VehicleInspection Not Exists";
    public const string ProcessCompleted = "Process Completed";

    public const string SuccessCreatedVehicleInspectionMessage = "VehicleInspection Successfully Created";
    public const string SuccessDeletedVehicleInspectionMessage = "VehicleInspection Successfully Deleted";
    public const string SuccessUpdatedVehicleInspectionMessage = "VehicleInspection Successfully Updated";
    
	//public const string SuccessMovedRecord = "Selected Record Successfuly Moved";
	//public const string Ranked1stError = "The record you want to move up is ranked 1st";
	//public const string RankedLastError = "The record you want to move down is in the last row";

    public const string TechnicalError = "Technical Error";
	public const string NotFoundRecord = "No Record Found!";
	public const string IncorrectOperation = "Incorrect Operation";
    //public const string IdNumberAlreadyExists = "This Id Number is Already Registered in the System";
    //FileUploaded
    public const string FileUploaded = "Dosya Baþarýyla Yüklendi";
}
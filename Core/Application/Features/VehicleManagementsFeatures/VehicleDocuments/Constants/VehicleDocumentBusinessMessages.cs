namespace Application.Features.VehicleManagementFeatures.VehicleDocuments.Constants;

public static class VehicleDocumentsBusinessMessages
{
    public const string SectionName = "VehicleDocument";

    public const string VehicleDocumentNotExists = "VehicleDocument Not Exists";
    public const string ProcessCompleted = "Process Completed";

    public const string SuccessCreatedVehicleDocumentMessage = "VehicleDocument Successfully Created";
    public const string SuccessDeletedVehicleDocumentMessage = "VehicleDocument Successfully Deleted";
    public const string SuccessUpdatedVehicleDocumentMessage = "VehicleDocument Successfully Updated";
    
	//public const string SuccessMovedRecord = "Selected Record Successfuly Moved";
	//public const string Ranked1stError = "The record you want to move up is ranked 1st";
	//public const string RankedLastError = "The record you want to move down is in the last row";

    public const string TechnicalError = "Technical Error";
	public const string NotFoundRecord = "No Record Found!";
	public const string IncorrectOperation = "Incorrect Operation";
    //public const string IdNumberAlreadyExists = "This Id Number is Already Registered in the System";
    // Dosya baiaroyla yüklendi
    public const string FileUploaded = "Dosya baþarýyla yüklendi";
}
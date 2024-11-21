namespace Application.Features.TransportationManagementFeatures.TransportationExternalServices.Constants;

public static class TransportationExternalServicesBusinessMessages
{
    public const string SectionName = "TransportationExternalService";

    public const string TransportationExternalServiceNotExists = "TransportationExternalService Not Exists";
    public const string ProcessCompleted = "Process Completed";

    public const string SuccessCreatedTransportationExternalServiceMessage = "TransportationExternalService Successfully Created";
    public const string SuccessDeletedTransportationExternalServiceMessage = "TransportationExternalService Successfully Deleted";
    public const string SuccessUpdatedTransportationExternalServiceMessage = "TransportationExternalService Successfully Updated";
    
	//public const string SuccessMovedRecord = "Selected Record Successfuly Moved";
	//public const string Ranked1stError = "The record you want to move up is ranked 1st";
	//public const string RankedLastError = "The record you want to move down is in the last row";

    public const string TechnicalError = "Technical Error";
	public const string NotFoundRecord = "No Record Found!";
	public const string IncorrectOperation = "Incorrect Operation";
	//public const string IdNumberAlreadyExists = "This Id Number is Already Registered in the System";
}
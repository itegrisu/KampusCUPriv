namespace Application.Features.TransportationManagementFeatures.TransportationGroups.Constants;

public static class TransportationGroupsBusinessMessages
{
    public const string SectionName = "TransportationGroup";

    public const string TransportationGroupNotExists = "TransportationGroup Not Exists";
    public const string ProcessCompleted = "Process Completed";

    public const string SuccessCreatedTransportationGroupMessage = "TransportationGroup Successfully Created";
    public const string SuccessDeletedTransportationGroupMessage = "TransportationGroup Successfully Deleted";
    public const string SuccessUpdatedTransportationGroupMessage = "TransportationGroup Successfully Updated";
    
	//public const string SuccessMovedRecord = "Selected Record Successfuly Moved";
	//public const string Ranked1stError = "The record you want to move up is ranked 1st";
	//public const string RankedLastError = "The record you want to move down is in the last row";

    public const string TechnicalError = "Technical Error";
	public const string NotFoundRecord = "No Record Found!";
	public const string IncorrectOperation = "Incorrect Operation";
	//public const string IdNumberAlreadyExists = "This Id Number is Already Registered in the System";
    public const string TransportationGroupCantReported = "Grup Bildirilemedi";
    public const string SuccessReportedTransportationGroupMessage = "Grup Basariyla Bildirildi";
    public const string SuccessCanceledTransportationGroupMessage = "Grup Iptal Edildi";

}
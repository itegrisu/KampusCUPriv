namespace Application.Features.TransportationManagementFeatures.TransportationPersonnels.Constants;

public static class TransportationPersonnelsBusinessMessages
{
    public const string SectionName = "TransportationPersonnel";

    public const string TransportationPersonnelNotExists = "TransportationPersonnel Not Exists";
    public const string ProcessCompleted = "Process Completed";

    public const string SuccessCreatedTransportationPersonnelMessage = "TransportationPersonnel Successfully Created";
    public const string SuccessDeletedTransportationPersonnelMessage = "TransportationPersonnel Successfully Deleted";
    public const string SuccessUpdatedTransportationPersonnelMessage = "TransportationPersonnel Successfully Updated";
    
	//public const string SuccessMovedRecord = "Selected Record Successfuly Moved";
	//public const string Ranked1stError = "The record you want to move up is ranked 1st";
	//public const string RankedLastError = "The record you want to move down is in the last row";

    public const string TechnicalError = "Technical Error";
	public const string NotFoundRecord = "No Record Found!";
	public const string IncorrectOperation = "Incorrect Operation";
    //public const string IdNumberAlreadyExists = "This Id Number is Already Registered in the System";
    //Personel Zaten Mevcut
    public const string PersonnelAllreadyExist = "Personel zaten kay�tl�!";
    //SuccessReportedTransportationPersonnelMessage
    public const string SuccessReportedTransportationPersonnelMessage = "Personel Ba�ar�yla Bildirildi!";
    public const string SuccessReportCancelTransportationPersonnelMessage = "Personel Iptal Edildi!";


}
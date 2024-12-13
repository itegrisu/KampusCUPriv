namespace Application.Features.TransportationManagementFeatures.TransportationServices.Constants;

public static class TransportationServicesBusinessMessages
{
    public const string SectionName = "TransportationService";

    public const string TransportationServiceNotExists = "TransportationService Not Exists";
    public const string ProcessCompleted = "Process Completed";

    public const string SuccessCreatedTransportationServiceMessage = "TransportationService Successfully Created";
    public const string SuccessDeletedTransportationServiceMessage = "TransportationService Successfully Deleted";
    public const string SuccessUpdatedTransportationServiceMessage = "TransportationService Successfully Updated";
    
	//public const string SuccessMovedRecord = "Selected Record Successfuly Moved";
	//public const string Ranked1stError = "The record you want to move up is ranked 1st";
	//public const string RankedLastError = "The record you want to move down is in the last row";

    public const string TechnicalError = "Technical Error";
	public const string NotFoundRecord = "No Record Found!";
	public const string IncorrectOperation = "Incorrect Operation";
    //public const string IdNumberAlreadyExists = "This Id Number is Already Registered in the System";
    //SucessUploadFile
    public const string SucessUploadFile = "Dosya ba�ar�yla y�klendi";
    // Sefer Bildirilemedi
    public const string TransportationServiceCantReoported = "Sefer Bildirilemedi";
    // Sefer Basariyla Bildirildi
    public const string SuccessReportedTransportatinServiceMessage = "Sefer Basariyla Bildirildi";
    public const string SuccessPrintedTransportatinServiceMessage = "Sefer Basariyla Cikti Alindi";
    public const string SuccessReportCancelledTransportatinServiceMessage = "Sefer Iptal Edildi";
    //PersonnelNotExists
    public const string TransportationServiceNotReported = "Sefer Bildirilmemis";
    public const string GroupNotReported = "Hic Grup Bildirilmemis";
    public const string PersonnelNotReported = "Hic Personel Bildirilmemis";
    public const string PassengerNotReported = "Hic Yolcu Bildirilmemis";
}
namespace Application.Features.VehicleManagementFeatures.VehicleTransactions.Constants;

public static class VehicleTransactionsBusinessMessages
{
    public const string SectionName = "VehicleTransaction";

    public const string VehicleTransactionNotExists = "VehicleTransaction Not Exists";
    public const string ProcessCompleted = "Process Completed";

    public const string SuccessCreatedVehicleTransactionMessage = "VehicleTransaction Successfully Created";
    public const string SuccessDeletedVehicleTransactionMessage = "VehicleTransaction Successfully Deleted";
    public const string SuccessUpdatedVehicleTransactionMessage = "VehicleTransaction Successfully Updated";
    
	//public const string SuccessMovedRecord = "Selected Record Successfuly Moved";
	//public const string Ranked1stError = "The record you want to move up is ranked 1st";
	//public const string RankedLastError = "The record you want to move down is in the last row";

    public const string TechnicalError = "Technical Error";
	public const string NotFoundRecord = "No Record Found!";
	public const string IncorrectOperation = "Incorrect Operation";
    //public const string IdNumberAlreadyExists = "This Id Number is Already Registered in the System";
    public const string VehicleTransactionAlreadyExists = "Ara� Zaten Mevcut!!";
    // Bu ara� firma arac� olmad��� i�in kiraya verilemez
    public const string VehicleIsNotCompanyVehicleToHire = "Bu ara� firma arac� olmad��� i�in kiraya verilemez";
    public const string VehicleIsNotCompanyVehicleToSale = "Bu ara� firma arac� olmad��� i�in sat�lamaz";
    public const string VehicleIsNotCompanyVehicleToAllocate = "Bu ara� tahsis edilemez";
    //SucessUploadFile
    public const string SucessUploadFile = "Dosya ba�ar�yla y�klendi";
    //VehicleNotSuitableForSale
    public const string VehicleNotSuitablForTake = "Bu ara� Ara� Durumunda kayd� oldu�u i�in Kiral�k Al�nan Araca uygun de�il!";
    //UserNotExists
    public const string UserNotExists = "Kullan�c� Bulunamad�!";
    //NewKMShouldBeMoreThanOldKM
    public const string NewKMShouldBeMoreThanOldKM = "Girilen yeni KM arac�n eski KM sinden b�y�k olmal�d�r";

}
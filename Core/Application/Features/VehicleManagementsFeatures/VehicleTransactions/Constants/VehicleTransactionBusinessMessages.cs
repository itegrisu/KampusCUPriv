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
    public const string VehicleTransactionAlreadyExists = "Araç Zaten Mevcut!!";
    // Bu araç firma aracý olmadýðý için kiraya verilemez
    public const string VehicleIsNotCompanyVehicleToHire = "Bu araç firma aracý olmadýðý için kiraya verilemez";
    public const string VehicleIsNotCompanyVehicleToSale = "Bu araç firma aracý olmadýðý için satýlamaz";
    public const string VehicleIsNotCompanyVehicleToAllocate = "Bu araç tahsis edilemez";
    //SucessUploadFile
    public const string SucessUploadFile = "Dosya baþarýyla yüklendi";
    //VehicleNotSuitableForSale
    public const string VehicleNotSuitablForTake = "Bu araç Araç Durumunda kaydý olduðu için Kiralýk Alýnan Araca uygun deðil!";
    //UserNotExists
    public const string UserNotExists = "Kullanýcý Bulunamadý!";
    //NewKMShouldBeMoreThanOldKM
    public const string NewKMShouldBeMoreThanOldKM = "Girilen yeni KM aracýn eski KM sinden büyük olmalýdýr";

}
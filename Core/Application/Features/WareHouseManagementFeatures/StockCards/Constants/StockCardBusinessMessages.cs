namespace Application.Features.WarehouseManagementFeatures.StockCards.Constants;

public static class StockCardsBusinessMessages
{
    public const string SectionName = "StockCard";

    public const string StockCardNotExists = "StockCard Not Exists";
    public const string ProcessCompleted = "Process Completed";

    public const string SuccessCreatedStockCardMessage = "StockCard Successfully Created";
    public const string SuccessDeletedStockCardMessage = "StockCard Successfully Deleted";
    public const string SuccessUpdatedStockCardMessage = "StockCard Successfully Updated";

    //public const string SuccessMovedRecord = "Selected Record Successfuly Moved";
    //public const string Ranked1stError = "The record you want to move up is ranked 1st";
    //public const string RankedLastError = "The record you want to move down is in the last row";

    public const string TechnicalError = "Technical Error";
    public const string NotFoundRecord = "No Record Found!";
    public const string IncorrectOperation = "Incorrect Operation";
    //public const string IdNumberAlreadyExists = "This Id Number is Already Registered in the System";

    public const string StockCategoryNotExists = "Ýlgili Stok Kategorisi Bulunamadý";
    public const string MeasureTypeNotExists = "Ýlgili Ölçü Birimi Bulunamadý";
    public const string WarehouseNotExists = "Ýlgili Depo Bulunamadý";
    public const string StockCardNotExistsInWarehouse = "Ýlgili Depoda Stok Kartý Bulunamadý";
    public const string StockNameShouldUnique = "Stok Adý Benzersiz Olmalýdýr";
    public const string StockCodeShouldUnique = "Stok Kodu Benzersiz Olmalýdýr";
}
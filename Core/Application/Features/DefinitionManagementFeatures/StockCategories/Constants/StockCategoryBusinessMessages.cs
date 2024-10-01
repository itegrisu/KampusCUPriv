namespace Application.Features.DefinitionManagementFeatures.StockCategories.Constants;

public static class StockCategoriesBusinessMessages
{
    public const string SectionName = "StockCategory";

    public const string StockCategoryNotExists = "StockCategory Not Exists";
    public const string ProcessCompleted = "Process Completed";

    public const string SuccessCreatedStockCategoryMessage = "StockCategory Successfully Created";
    public const string SuccessDeletedStockCategoryMessage = "StockCategory Successfully Deleted";
    public const string SuccessUpdatedStockCategoryMessage = "StockCategory Successfully Updated";

    //public const string SuccessMovedRecord = "Selected Record Successfuly Moved";
    //public const string Ranked1stError = "The record you want to move up is ranked 1st";
    //public const string RankedLastError = "The record you want to move down is in the last row";

    public const string TechnicalError = "Technical Error";
    public const string NotFoundRecord = "No Record Found!";
    public const string IncorrectOperation = "Incorrect Operation";
    //public const string IdNumberAlreadyExists = "This Id Number is Already Registered in the System";

    public const string StockCategoryNameShouldBeUnique = "Ýlgili Kategory Ýsmi Zaten Var";
    public const string StockCategoryCodeShouldBeUnique = "Ýlgili Kategory Kodu Zaten Var";
}
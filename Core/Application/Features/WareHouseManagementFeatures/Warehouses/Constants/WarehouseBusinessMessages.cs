namespace Application.Features.WarehouseManagementFeatures.Warehouses.Constants;

public static class WarehousesBusinessMessages
{
    public const string SectionName = "Warehouse";

    public const string WarehouseNotExists = "Warehouse Not Exists";
    public const string ProcessCompleted = "Process Completed";

    public const string SuccessCreatedWarehouseMessage = "Warehouse Successfully Created";
    public const string SuccessDeletedWarehouseMessage = "Warehouse Successfully Deleted";
    public const string SuccessUpdatedWarehouseMessage = "Warehouse Successfully Updated";

    //public const string SuccessMovedRecord = "Selected Record Successfuly Moved";
    //public const string Ranked1stError = "The record you want to move up is ranked 1st";
    //public const string RankedLastError = "The record you want to move down is in the last row";

    public const string TechnicalError = "Technical Error";
    public const string NotFoundRecord = "No Record Found!";
    public const string IncorrectOperation = "Incorrect Operation";
    //public const string IdNumberAlreadyExists = "This Id Number is Already Registered in the System";

    public const string WarehouseNameShouldBeUnique = "Ýlgili Deponun Adýnda Bir Kayýt Zaten Var";
    public const string OrganizationNotExists = "Ýlgili Kurum Bulunamadý";
}
namespace Application.Features.WareHouseManagementFeatures.StockMovements.Constants;

public static class StockMovementsBusinessMessages
{
    public const string SectionName = "StockMovement";

    public const string StockMovementNotExists = "StockMovement Not Exists";
    public const string ProcessCompleted = "Process Completed";

    public const string SuccessCreatedStockMovementMessage = "StockMovement Successfully Created";
    public const string SuccessDeletedStockMovementMessage = "StockMovement Successfully Deleted";
    public const string SuccessUpdatedStockMovementMessage = "StockMovement Successfully Updated";

    //public const string SuccessMovedRecord = "Selected Record Successfuly Moved";
    //public const string Ranked1stError = "The record you want to move up is ranked 1st";
    //public const string RankedLastError = "The record you want to move down is in the last row";

    public const string TechnicalError = "Technical Error";
    public const string NotFoundRecord = "No Record Found!";
    public const string IncorrectOperation = "Incorrect Operation";
    //public const string IdNumberAlreadyExists = "This Id Number is Already Registered in the System";

    public const string StockCardNotExists = "Stock Card Not Exists";
    public const string PreviousWarehouseNotExists = "Previous Warehouse Not Exists";
    public const string NextWarehouseNotExists = "Next Warehouse Not Exists";

    public const string LessProductInWarehouse = "You don't have the quantity you want to transfer!";
}
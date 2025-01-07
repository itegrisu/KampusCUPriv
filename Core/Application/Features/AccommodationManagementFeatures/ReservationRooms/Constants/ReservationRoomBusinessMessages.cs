namespace Application.Features.AccommodationManagementFeatures.ReservationRooms.Constants;

public static class ReservationRoomsBusinessMessages
{
    public const string SectionName = "ReservationRoom";

    public const string ReservationRoomNotExists = "ReservationRoom Not Exists";
    public const string ProcessCompleted = "Process Completed";

    public const string SuccessCreatedReservationRoomMessage = "ReservationRoom Successfully Created";
    public const string SuccessDeletedReservationRoomMessage = "ReservationRoom Successfully Deleted";
    public const string SuccessUpdatedReservationRoomMessage = "ReservationRoom Successfully Updated";

    //public const string SuccessMovedRecord = "Selected Record Successfuly Moved";
    //public const string Ranked1stError = "The record you want to move up is ranked 1st";
    //public const string RankedLastError = "The record you want to move down is in the last row";

    public const string TechnicalError = "Technical Error";
    public const string NotFoundRecord = "No Record Found!";
    public const string IncorrectOperation = "Incorrect Operation";
    //public const string IdNumberAlreadyExists = "This Id Number is Already Registered in the System";
    //ReservationRoomHasAccommodationDates
    public const string ReservationRoomHasAccommodationDates = "Odada Konaklama Mevcuttur. Silme Ýþlemi Gerçekleþtirilemez.";
}
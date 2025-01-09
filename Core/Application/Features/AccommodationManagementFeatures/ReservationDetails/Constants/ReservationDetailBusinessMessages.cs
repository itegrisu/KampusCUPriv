namespace Application.Features.AccommodationManagementFeatures.ReservationDetails.Constants;

public static class ReservationDetailsBusinessMessages
{
    public const string SectionName = "ReservationDetail";

    public const string ReservationDetailNotExists = "ReservationDetail Not Exists";
    public const string ProcessCompleted = "Process Completed";

    public const string SuccessCreatedReservationDetailMessage = "ReservationDetail Successfully Created";
    public const string SuccessDeletedReservationDetailMessage = "ReservationDetail Successfully Deleted";
    public const string SuccessUpdatedReservationDetailMessage = "ReservationDetail Successfully Updated";

    //public const string SuccessMovedRecord = "Selected Record Successfuly Moved";
    //public const string Ranked1stError = "The record you want to move up is ranked 1st";
    //public const string RankedLastError = "The record you want to move down is in the last row";

    public const string TechnicalError = "Technical Error";
    public const string NotFoundRecord = "No Record Found!";
    public const string IncorrectOperation = "Incorrect Operation";
    //public const string IdNumberAlreadyExists = "This Id Number is Already Registered in the System";

    public const string RoomCountCanBeGreaterThanZero = "Oda Sayýsý Sýfýrdan Büyük Olmalýdýr!";
    public const string DateMustBeBetweenResDate = "Seçili Tarih Rezervasyon Tarihleri Arasýnda Olmalýdýr!";

    public const string ReservationDateCanBeUniq = "Seçili Oda Tipinde ve Seçili Otelde Bu Tarihte Rezervasyon Vardýr!";

    public const string ReservationHotelNotExist = "Rezervasyon Oteli Mevcut Deðil!";
    public const string RoomTypeNotExist = "Oda Tipi Mevcut Deðil!";

    public const string ReservationHotelAuthError = "Bu Otele Eriþiminiz Yoktur!";

}
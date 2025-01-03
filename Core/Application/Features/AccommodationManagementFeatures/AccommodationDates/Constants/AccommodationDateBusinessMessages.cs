namespace Application.Features.AccommodationManagementFeatures.AccommodationDates.Constants;

public static class AccommodationDatesBusinessMessages
{
    public const string SectionName = "AccommodationDate";

    public const string AccommodationDateNotExists = "AccommodationDate Not Exists";
    public const string ProcessCompleted = "Process Completed";

    public const string SuccessCreatedAccommodationDateMessage = "AccommodationDate Successfully Created";
    public const string SuccessDeletedAccommodationDateMessage = "AccommodationDate Successfully Deleted";
    public const string SuccessUpdatedAccommodationDateMessage = "AccommodationDate Successfully Updated";

    //public const string SuccessMovedRecord = "Selected Record Successfuly Moved";
    //public const string Ranked1stError = "The record you want to move up is ranked 1st";
    //public const string RankedLastError = "The record you want to move down is in the last row";

    public const string TechnicalError = "Technical Error";
    public const string NotFoundRecord = "No Record Found!";
    public const string IncorrectOperation = "Incorrect Operation";
    //public const string IdNumberAlreadyExists = "This Id Number is Already Registered in the System";

    public const string ReservationDetailFKNotExists = "Rezervasyon Mevcut Deðil!";
    public const string GuestFKNotExists = "Ziyaretçi Mevcut Deðil!";
    public const string RoomNoFKNotExists = "Rezervasyon Odasý Mevcut Deðil!";
    public const string ReservationHotelFKNotExists = "Rezervasyon Otel Mevcut Deðil!";

    public const string RoomNotAvailableForSelectedDates = "Seçili Tarihlerde Oda Mevcut Deðil!";
    public const string RoomTypeNotAvailableForSelectedDates = "Seçili Tarihlerde Oda Tipi Mevcut Deðil!";
    public const string CustomerAlreadyRegisteredAnotherRoom = "Müþteri Zaten Baþka Bir Odada Kayýtlý!";
}
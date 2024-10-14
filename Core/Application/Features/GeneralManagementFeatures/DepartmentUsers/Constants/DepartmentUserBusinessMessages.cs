namespace Application.Features.GeneralManagementFeatures.DepartmentUsers.Constants;

public static class DepartmentUsersBusinessMessages
{
    public const string SectionName = "DepartmentUser";

    public const string DepartmentUserNotExists = "DepartmentUser Not Exists";
    public const string ProcessCompleted = "Process Completed";

    public const string SuccessCreatedDepartmentUserMessage = "DepartmentUser Successfully Created";
    public const string SuccessDeletedDepartmentUserMessage = "DepartmentUser Successfully Deleted";
    public const string SuccessUpdatedDepartmentUserMessage = "DepartmentUser Successfully Updated";

    //public const string SuccessMovedRecord = "Selected Record Successfuly Moved";
    //public const string Ranked1stError = "The record you want to move up is ranked 1st";
    //public const string RankedLastError = "The record you want to move down is in the last row";

    public const string TechnicalError = "Technical Error";
    public const string NotFoundRecord = "No Record Found!";
    public const string IncorrectOperation = "Incorrect Operation";
    //public const string IdNumberAlreadyExists = "This Id Number is Already Registered in the System";

    public const string DepartmentNotExists = "ÝLgili Departman bulunamadý";
    public const string PersonelNotExists = "Ýlgili Personel bulunamadý";
    public const string PersonelAlreadyAddedToDepartment = "Personel zaten bu departmana eklenmiþ";
    public const string HasAdminUser = "Departman Asýl veya Yedek yönetici silinemez. Öncelikle silmek istediðiniz personeli Asýl veya Yedek yöneticiden çýkarýnýz";
}
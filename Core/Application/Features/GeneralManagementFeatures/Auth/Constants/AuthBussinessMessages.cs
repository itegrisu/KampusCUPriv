namespace Application.Features.GeneralManagementFeatures.Auth.Constants
{
    public static class AuthBussinessMessages
    {
        public const string SectionName = "University";

        public const string UserNotExists = "UserFK Not Exists";
        public const string ProcessCompleted = "Process Completed";
        public const string SuccessDeletedRefreshTokenMessage = "Refresh Token Deleted Successfully";
        public const string SuccessCreatedTokenByRefreshMessage = "Refresh Token Deleted Successfully";
        public const string SuccessRegister = "Successfully Register";
        public const string SuccessLogin = "Successfully Login";
        public const string SuccessUpdatedPassword = "Password Updated Successfully";
        public const string LoginFailed = "Login Failed";
        public const string NotAuthorisedToLogIn = "The user is not authorised to log in";


        public const string Error = "Error";
        public const string ProcessError = "Process Error";
        public const string RefreshTokenNotFound = "Refresh Token Not Found";
        public const string TokenNotCreatedByRefresh = "Token Not Created By Refresh Token";
        public const string UserAlreadyExist = "UserFK Already Exists!";
        public const string IncorrectOperation = "Incorrect Operation";
        public const string EmailNotFound = "Email Not Found";
        public const string EmailOrPasswordIncorrect = "Email or Password Incorrect!";

        public const string UnamRoleIsEmpty = "Unam Role is Empty!";
        public const string IdNumberAlreadyExist = "Id Number Already Exists!";
        public const string EmailAlreadyExist = "Email Already Exists!";
        public const string PhoneNumberAlreadyExist = "Phone Number Already Exists!";

    }
}

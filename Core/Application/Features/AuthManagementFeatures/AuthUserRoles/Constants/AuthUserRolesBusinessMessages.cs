namespace Application.Features.AuthManagementFeatures.AuthUserRoles.Constants;

public static class AuthUserRolesBusinessMessages
{
    public const string SectionName = "AuthUserRole";

    public const string AuthUserRoleNotExists = "AuthUserRoleNotExists";
    public const string ProcessCompleted = "Process Completed";
    public const string SuccessCreatedAuthUserRoleMessage = "Auth UserFK Role Successfully Created";
    public const string SuccessDeletedAuthUserRoleMessage = "Auth UserFK Role Successfully Deleted";
    public const string SuccessUpdatedAuthUserRoleMessage = "Auth UserFK Role Successfully Updated";

    public const string ErrorAuthUserRoleShouldExist = "Auth Role Page Not Found!";
    public const string ErrorUserShouldExist = "User Not Found!";
    public const string ErrorRoleShouldExist = "Role Not Found!";
    public const string ErrorPageShouldExist = "Page Not Found!";
    public const string AuthRoleHasBeenAddedBefore = "Role Has Been Added Before!";
    public const string AuthPageHasBeenAddedBefore = "Page Has Been Added Before!";

    public const string DoesUserHasPage = "User Already This Has Page!";
}
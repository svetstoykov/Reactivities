namespace Application.Common.ErrorHandling;

public static class CommonErrorMessages
{
    public const string MigrationError = "An error occurred during migration";
        
    public const string SomethingWentWrong = "Oops! Something went wrong!";

    public const string InvalidEmail = "Invalid user email";

    public const string FailedLogin = "Incorrect user credentials. Please double check email/password";

    public const string EmailTaken = "User with email: '{0} already exists!'";

    public const string UsernameTaken = "User with username: '{0} already exists!'";

    public const string FailedToCreateUser = "Failed to create new user";

    public const string PasswordRequirementsNotMet = "Password must be at least 8 chars long and must contain - one uppercase, one lowercase, one number and one special symbol";
}
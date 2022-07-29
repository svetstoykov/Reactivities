namespace Models.ErrorHandling.Helpers
{
    public static class IdentityErrorMessages
    {
        public const string EmailTaken = "User with email: '{0} already exists!'";

        public const string UsernameTaken = "User with username: '{0} already exists!'";

        public const string FailedToCreateUser = "Failed to create new user";

        public const string PasswordRequirementsNotMet = "Password must be at least 8 chars long and must contain - one uppercase, one lowercase, one number and one special symbol";

        public const string InvalidCurrentUser = "Failed to load current user";

        public const string InvalidUser = "Failed to load user";

        public const string InvalidEmail = "Invalid user email.";

        public const string FailedLogin = "Incorrect user credentials. Please double check email/password";
    }
}

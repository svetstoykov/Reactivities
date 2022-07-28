namespace Models.ErrorHandling.Helpers
{
    public static class IdentityErrorMessages
    {
        public static string EmailTaken = "User with email: '{0} already exists!'";

        public static string UsernameTaken = "User with username: '{0} already exists!'";

        public static string FailedToCreateUser = "Failed to create new user";

        public static string InvalidEmail = "Email cannot be empty.";

        public static string InvalidUsername = "Username cannot be empty.";

        public static string InvalidDisplayName = "Display name cannot be empty.";

        public static string PasswordRequirementsNotMet =
            "Password must be at least 8 chars along and must contain - one uppercase, one lowercase, one number and one special symbol";

        public static string InvalidCurrentUser =
            "Failed to load current user";
    }
}

namespace Infrastructure.Identity.DataServices
{
    public interface IUserDataService
    {
        Task<bool> ChangeEmailAddress(string currentEmail, string newEmail);
    }
}

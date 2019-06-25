namespace MishMash.Services.Contracts
{
    using MishMash.ViewModels.Users;

    public interface IUsersService
    {
        string GetUserRole(string username);

        void RegisterUser(RegisterViewModel model);

        bool UserExists(LoginViewModel model);
    }
}
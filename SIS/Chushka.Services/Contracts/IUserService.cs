namespace Chushka.Services.Contracts
{
    using Chushka.ViewModels.Users;

    public interface IUserService
    {
        string GetUserRole(string username);

        void RegisterUser(RegisterViewModel model);

        bool UserExists(LoginViewModel model);
    }
}
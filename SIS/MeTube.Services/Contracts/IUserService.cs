namespace MeTube.Services.Contracts
{
    using MeTube.ViewModels.Users;

    public interface IUserService
    {
        string GetEmail(string username);

        void RegisterUser(RegisterViewModel model);

        bool UserExists(LoginViewModel model);
    }
}
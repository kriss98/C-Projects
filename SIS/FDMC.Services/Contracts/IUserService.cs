namespace FDMC.Services.Contracts
{
    using FDMC.ViewModels.Users;

    public interface IUserService
    {
        void RegisterUser(RegisterViewModel model);

        bool UserExists(LoginViewModel model);
    }
}
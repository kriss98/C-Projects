namespace FDMC.Services
{
    using System.Linq;

    using FDMC.Data;
    using FDMC.Models;
    using FDMC.Services.Base;
    using FDMC.Services.Contracts;
    using FDMC.ViewModels.Users;

    public class UserService : BaseService, IUserService
    {
        public UserService(FDMCContext context)
            : base(context)
        {
        }

        public void RegisterUser(RegisterViewModel model)
        {
            var user = new User
                           {
                               Username = model.Username,
                               Email = model.Email,
                               Password = model.Password,
                           };

            this.context.Users.Add(user);
            this.context.SaveChanges();
        }

        public bool UserExists(LoginViewModel model)
        {
            return this.context.Users.Any(u => u.Username == model.Username && u.Password == model.Password);
        }
    }
}
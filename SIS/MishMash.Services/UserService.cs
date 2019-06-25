namespace MishMash.Services
{
    using System.Linq;

    using MishMash.Data;
    using MishMash.Models;
    using MishMash.Models.Enums;
    using MishMash.Services.Base;
    using MishMash.Services.Contracts;
    using MishMash.ViewModels.Users;

    public class UserService : BaseService, IUsersService
    {
        public UserService(MishMashContext context)
            : base(context)
        {
        }

        public string GetUserRole(string username)
        {
            return this.context.Users.FirstOrDefault(u => u.Username == username).Role.ToString();
        }

        public void RegisterUser(RegisterViewModel model)
        {
            var role = this.context.Users.Any() ? UserRole.User : UserRole.Admin;
            var user = new User
                           {
                               Username = model.Username,
                               Email = model.Email,
                               Password = model.Password,
                               Role = role
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
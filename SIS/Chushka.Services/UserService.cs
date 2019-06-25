namespace Chushka.Services
{
    using System.Linq;

    using Chushka.Data;
    using Chushka.Models;
    using Chushka.Models.Enums;
    using Chushka.Services.Base;
    using Chushka.Services.Contracts;
    using Chushka.ViewModels.Users;

    public class UserService : BaseService, IUserService
    {
        public UserService(ChushkaContext context)
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
                               FullName = model.FullName,
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
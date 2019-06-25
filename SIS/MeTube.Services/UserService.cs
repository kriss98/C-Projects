namespace MeTube.Services
{
    using System.Linq;

    using MeTube.Data;
    using MeTube.Models;
    using MeTube.Services.Base;
    using MeTube.Services.Contracts;
    using MeTube.ViewModels.Users;

    public class UserService : BaseService, IUserService
    {
        public UserService(MeTubeContext context)
            : base(context)
        {
        }

        public string GetEmail(string username)
        {
            return this.context.Users.FirstOrDefault(u => u.Username == username).Email;
        }

        public void RegisterUser(RegisterViewModel model)
        {
            var user = new User { Username = model.Username, Email = model.Email, Password = model.Password };

            this.context.Users.Add(user);
            this.context.SaveChanges();
        }

        public bool UserExists(LoginViewModel model)
        {
            return this.context.Users.Any(u => u.Username == model.Username && u.Password == model.Password);
        }
    }
}
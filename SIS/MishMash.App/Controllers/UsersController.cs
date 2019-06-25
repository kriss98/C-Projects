namespace MishMash.App.Controllers
{
    using System.Collections.Generic;

    using MishMash.Services.Contracts;
    using MishMash.ViewModels.Users;

    using SIS.Framework.ActionResults;
    using SIS.Framework.Attributes.Action;
    using SIS.Framework.Attributes.Method;
    using SIS.Framework.Security;

    public class UsersController : BaseController
    {
        private readonly IUsersService usersService;

        public UsersController(IUsersService usersService)
        {
            this.usersService = usersService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            if (this.IsLoggedIn())
            {
                return this.RedirectToAction("/");
            }

            return this.View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            var userexists = this.usersService.UserExists(model);

            if (!userexists)
            {
                return this.RedirectToAction("/Users/Register");
            }

            var role = this.usersService.GetUserRole(model.Username);

            this.SignIn(
                new IdentityUser
                    {
                        Username = model.Username,
                        Password = model.Password,
                        Roles = new List<string> { role }
                    });
            return this.RedirectToAction("/");
        }

        [HttpGet]
        [Authorize]
        public IActionResult Logout()
        {
            this.SignOut();
            return this.RedirectToAction("/");
        }

        [HttpGet]
        public IActionResult Register()
        {
            if (this.IsLoggedIn())
            {
                return this.RedirectToAction("/");
            }

            return this.View();
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            if (model.Password != model.ConfirmPassword)
            {
                return this.View();
            }

            this.usersService.RegisterUser(model);

            var role = this.usersService.GetUserRole(model.Username);

            this.SignIn(
                new IdentityUser
                    {
                        Email = model.Email,
                        Password = model.Password,
                        Username = model.Username,
                        Roles = new List<string> { role }
                    });

            return this.RedirectToAction("/");
        }
    }
}
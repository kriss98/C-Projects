namespace FDMC.App.Controllers
{
    using FDMC.ViewModels.Users;

    using FDMC.Services.Contracts;

    using SIS.Framework.ActionResults;
    using SIS.Framework.Attributes.Action;
    using SIS.Framework.Attributes.Method;
    using SIS.Framework.Security;

    public class UsersController : BaseController
    {
        private readonly IUserService usersService;

        public UsersController(IUserService usersService)
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

            this.SignIn(new IdentityUser { Username = model.Username, Password = model.Password });
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

            this.SignIn(new IdentityUser { Email = model.Email, Password = model.Password, Username = model.Username });

            return this.RedirectToAction("/");
        }
    }
}
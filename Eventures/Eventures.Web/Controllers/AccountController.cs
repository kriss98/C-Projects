namespace Eventures.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using AutoMapper;

    using Eventures.Models;
    using Eventures.Web.ViewModels.Account;
    using Eventures.Web.ViewModels.Account.Binding;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class AccountController : Controller
    {
        private readonly SignInManager<EventuresUser> signIn;

        private readonly IMapper mapper;

        public AccountController(SignInManager<EventuresUser> signIn, IMapper mapper)
        {
            this.signIn = signIn;
            this.mapper = mapper;
        }

        public IActionResult ExternalLogin(string provider)
        {
            var returnUrl = "/";
            var redirectUrl = this.Url.Action(nameof(this.ExternalLoginCallback), "Account", new { returnUrl });
            var properties = this.signIn.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            return this.Challenge(properties, provider);
        }

        public async Task<IActionResult> ExternalLoginCallback(string returnUrl = null, string remoteError = null)
        {
            if (remoteError != null)
            {
                return this.RedirectToAction(nameof(Login));
            }

            var info = await this.signIn.GetExternalLoginInfoAsync();
            if (info == null)
            {
                return this.RedirectToAction(nameof(Login));
            }

            var result = await this.signIn.ExternalLoginSignInAsync(
                             info.LoginProvider,
                             info.ProviderKey,
                             isPersistent: false,
                             bypassTwoFactor: true);
            if (result.Succeeded)
            {
                return this.RedirectToLocal(returnUrl);
            }

            this.ViewData["ReturnUrl"] = returnUrl;
            this.ViewData["LoginProvider"] = info.LoginProvider;
            var email = info.Principal.FindFirstValue(ClaimTypes.Email);
            return this.View("ExternalLogin", new ExternalLoginConfirmationViewModel { Email = email });
        }

        [HttpPost]
        public async Task<IActionResult> ExternalLoginConfirmation(
            ExternalLoginConfirmationViewModel model,
            string returnUrl = null)
        {
            if (this.ModelState.IsValid)
            {
                var info = await this.signIn.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    throw new ApplicationException("Error loading external login information during confirmation.");
                }

                var user = this.mapper.Map<EventuresUser>(model);

                var result = await this.signIn.UserManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    if (this.signIn.UserManager.Users.Count() == 1)
                    {
                        await this.signIn.UserManager.AddToRoleAsync(user, "Admin");
                    }
                    else
                    {
                        await this.signIn.UserManager.AddToRoleAsync(user, "User");
                    }

                    result = await this.signIn.UserManager.AddLoginAsync(user, info);
                    if (result.Succeeded)
                    {
                        await this.signIn.SignInAsync(user, isPersistent: false);
                        return this.RedirectToLocal(returnUrl);
                    }
                }

                this.AddErrors(result);
            }

            this.ViewData["ReturnUrl"] = returnUrl;
            return this.View(nameof(this.ExternalLogin), model);
        }

        public IActionResult Login()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginBindingModel model)
        {
            if (this.ModelState.IsValid)
            {
                var result = await this.signIn.PasswordSignInAsync(model.Username, model.Password, true, false);

                if (result.Succeeded)
                {
                    return this.RedirectToAction("Index", "Home");
                }
            }

            this.ModelState.AddModelError(string.Empty, "Invalid login attempt");

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await this.signIn.SignOutAsync();
            return this.RedirectToAction("Index", "Home");
        }

        public IActionResult Register()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterBindingModel viewModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(viewModel);
            }

            var user = this.mapper.Map<EventuresUser>(viewModel);

            var result = this.signIn.UserManager.CreateAsync(user, viewModel.Password).Result;

            if (result.Succeeded)
            {
                if (this.signIn.UserManager.Users.Count() == 1)
                {
                    await this.signIn.UserManager.AddToRoleAsync(user, "Admin");
                }
                else
                {
                    await this.signIn.UserManager.AddToRoleAsync(user, "User");
                }

                this.signIn.SignInAsync(user, false).Wait();
                return this.RedirectToAction("Index", "Home");
            }

            if (this.signIn.UserManager.Users.Any(u => u.UserName == viewModel.Username))
            {
                this.ModelState.AddModelError(string.Empty, $"Username {viewModel.Username} is already taken!");
            }

            return this.View(viewModel);
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                this.ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (this.Url.IsLocalUrl(returnUrl))
            {
                return this.Redirect(returnUrl);
            }

            return this.RedirectToAction(nameof(HomeController.Index), "Home");
        }

        [Authorize(Roles = "Admin")]
        public IActionResult ManageUsers()
        {
            return this.View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Promote(UserIdViewModel model)
        {
            var user = this.signIn.UserManager.Users.FirstOrDefault(u => u.Id == model.Id);
            await this.signIn.UserManager.AddToRoleAsync(user, "Admin");
            await this.signIn.UserManager.RemoveFromRoleAsync(user, "User");

            return this.RedirectToAction("ManageUsers");
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Demote(UserIdViewModel model)
        {
            var user = this.signIn.UserManager.Users.FirstOrDefault(u => u.Id == model.Id);
            await this.signIn.UserManager.AddToRoleAsync(user, "User");
            await this.signIn.UserManager.RemoveFromRoleAsync(user, "Admin");

            return this.RedirectToAction("ManageUsers");
        }
    }
}
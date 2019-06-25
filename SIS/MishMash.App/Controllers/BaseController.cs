namespace MishMash.App.Controllers
{
    using System.Linq;
    using System.Runtime.CompilerServices;

    using SIS.Framework.ActionResults;
    using SIS.Framework.Controllers;

    public class BaseController : Controller
    {
        public bool IsLoggedIn()
        {
            return this.Identity != null;
        }

        protected override IViewable View([CallerMemberName] string actionName = "")
        {
            if (this.IsLoggedIn())
            {
                if (this.Identity.Roles.Contains("Admin"))
                {
                    this.Model.Data["LoggedInAdmin"] = "block";
                    this.Model.Data["LoggedIn"] = "none";
                    this.Model.Data["NotLoggedIn"] = "none";
                }
                else
                {
                    this.Model.Data["LoggedInAdmin"] = "none";
                    this.Model.Data["LoggedIn"] = "block";
                    this.Model.Data["NotLoggedIn"] = "none";
                }
            }
            else
            {
                this.Model.Data["NotLoggedIn"] = "block";
                this.Model.Data["LoggedInAdmin"] = "none";
                this.Model.Data["LoggedIn"] = "none";
            }

            return base.View(actionName);
        }
    }
}
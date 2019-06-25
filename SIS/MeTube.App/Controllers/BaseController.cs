namespace MeTube.App.Controllers
{
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
                this.Model.Data["LoggedIn"] = "block";
                this.Model.Data["NotLoggedIn"] = "none";
            }
            else
            {
                this.Model.Data["NotLoggedIn"] = "block";
                this.Model.Data["LoggedIn"] = "none";
            }

            return base.View(actionName);
        }
    }
}
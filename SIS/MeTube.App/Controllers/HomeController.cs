namespace MeTube.App.Controllers
{
    using MeTube.Services.Contracts;

    using SIS.Framework.ActionResults;
    using SIS.Framework.Attributes.Method;

    public class HomeController : BaseController
    {
        private readonly ITubeService tubeService;

        public HomeController(ITubeService tubeService)
        {
            this.tubeService = tubeService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            if (this.IsLoggedIn())
            {
                this.Model["Username"] = this.Identity.Username;

                var tubes = this.tubeService.GetAllTubes();

                this.Model["Tubes"] = tubes;
            }

            return this.View();
        }
    }
}
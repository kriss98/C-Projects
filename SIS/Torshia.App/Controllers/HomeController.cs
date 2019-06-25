namespace Torshia.App.Controllers
{
    using SIS.Framework.ActionResults;
    using SIS.Framework.Attributes.Method;

    using Torshia.Services.Contracts;

    public class HomeController : BaseController
    {
        private readonly ITasksService tasksService;

        public HomeController(ITasksService tasksService)
        {
            this.tasksService = tasksService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            if (this.IsLoggedIn())
            {
                this.Model["Username"] = this.Identity.Username;

                var taskViewModels = this.tasksService.AllNonReported();

                this.Model["Tasks"] = taskViewModels;
            }

            return this.View();
        }
    }
}
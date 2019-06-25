namespace MeTube.App.Controllers
{
    using MeTube.Services.Contracts;
    using MeTube.ViewModels.Tubes;

    using SIS.Framework.ActionResults;
    using SIS.Framework.Attributes.Action;
    using SIS.Framework.Attributes.Method;

    public class TubesController : BaseController
    {
        private readonly ITubeService tubeService;

        public TubesController(ITubeService tubeService)
        {
            this.tubeService = tubeService;
        }

        [HttpGet]
        [Authorize]
        public IActionResult Details(TubeViewModel model)
        {
            this.tubeService.IncrementViews(model.Id);
            var tube = this.tubeService.GetTube(model.Id);

            this.Model["Author"] = tube.Author;
            this.Model["Views"] = tube.Views;
            this.Model["Title"] = tube.Title;
            this.Model["YoutubeId"] = tube.YoutubeId;
            this.Model["Description"] = tube.Description;

            return this.View();
        }

        [HttpGet]
        [Authorize]
        public IActionResult Upload()
        {
            return this.View();
        }

        [HttpPost]
        [Authorize]
        public IActionResult Upload(UploadViewModel model)
        {
            this.tubeService.UploadTube(model, this.Identity.Username);

            return this.RedirectToAction("/");
        }
    }
}
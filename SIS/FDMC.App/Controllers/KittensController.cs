namespace FDMC.App.Controllers
{
    using FDMC.Services.Contracts;
    using FDMC.ViewModels.Kittens;

    using SIS.Framework.ActionResults;
    using SIS.Framework.Attributes.Action;
    using SIS.Framework.Attributes.Method;

    public class KittensController : BaseController
    {
        private readonly IKittenService kittenService;

        public KittensController(IKittenService kittenService)
        {
            this.kittenService = kittenService;
        }

        [HttpGet]
        [Authorize]
        public IActionResult Add()
        {
            return this.View();
        }

        [HttpPost]
        [Authorize]
        public IActionResult Add(KittenViewModel model)
        {
            this.kittenService.AddKitten(model);

            return this.RedirectToAction("/Kittens/All");
        }

        [HttpGet]
        [Authorize]
        public IActionResult All()
        {
            var kittens = this.kittenService.GetAllKittens();

            this.Model["Kittens"] = kittens;

            return this.View();
        }
    }
}
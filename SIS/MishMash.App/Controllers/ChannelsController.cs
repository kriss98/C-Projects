namespace MishMash.App.Controllers
{
    using MishMash.Services.Contracts;
    using MishMash.ViewModels.Channels;

    using SIS.Framework.ActionResults;
    using SIS.Framework.Attributes.Action;
    using SIS.Framework.Attributes.Method;

    public class ChannelsController : BaseController
    {
        private readonly IChannelService channelService;

        public ChannelsController(IChannelService channelService)
        {
            this.channelService = channelService;
        }

        [HttpGet]
        [Authorize("Admin")]
        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        [Authorize("Admin")]
        public IActionResult Create(ChannelDetailsViewModel model)
        {
            this.channelService.CreateChannel(model);

            return this.RedirectToAction("/");
        }

        [HttpGet]
        [Authorize]
        public IActionResult Details(ChannelViewModel model)
        {
            var viewModel = this.channelService.GetChannelById(model.Id);

            this.Model["Name"] = viewModel.Name;
            this.Model["Tags"] = viewModel.Tags;
            this.Model["Followers"] = viewModel.Followers;
            this.Model["Type"] = viewModel.Type;
            this.Model["Description"] = viewModel.Description;

            return this.View();
        }

        [HttpGet]
        [Authorize]
        public IActionResult Follow(ChannelViewModel model)
        {
            this.channelService.FollowChannel(model.Id, this.Identity.Username);

            return this.RedirectToAction("/");
        }

        [HttpGet]
        [Authorize]
        public IActionResult Followed()
        {
            var channels = this.channelService.GetFollowedChannels(this.Identity.Username);

            this.Model["Channels"] = channels;

            return this.View();
        }

        [HttpGet]
        [Authorize]
        public IActionResult Unfollow(ChannelViewModel model)
        {
            this.channelService.UnfollowChannel(model.Id, this.Identity.Username);

            return this.RedirectToAction("/Channels/Followed");
        }
    }
}
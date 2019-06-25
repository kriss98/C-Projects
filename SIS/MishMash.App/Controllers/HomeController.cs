namespace MishMash.App.Controllers
{
    using MishMash.Services.Contracts;

    using SIS.Framework.ActionResults;
    using SIS.Framework.Attributes.Method;

    public class HomeController : BaseController
    {
        private readonly IChannelService channelService;

        public HomeController(IChannelService channelService)
        {
            this.channelService = channelService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            if (this.IsLoggedIn())
            {
                this.Model["Username"] = this.Identity.Username;

                var myChannels = this.channelService.GetMyFollowedChannels(this.Identity.Username);

                var suggestedChannels = this.channelService.GetSuggestedChannels(this.Identity.Username);

                var otherChannels = this.channelService.GetOtherChannels(this.Identity.Username);

                this.Model["MyChannels"] = myChannels;
                this.Model["Suggested"] = suggestedChannels;
                this.Model["SeeOther"] = otherChannels;
            }

            return this.View();
        }
    }
}
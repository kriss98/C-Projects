namespace Eventures.Web.Controllers
{
    using System.Linq;

    using Eventures.Web.Filters;
    using Eventures.Web.Services.Contracts;
    using Eventures.Web.ViewModels.Events;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    using X.PagedList;

    public class EventsController : Controller
    {
        private readonly IEventService eventService;

        public EventsController(IEventService eventService)
        {
            this.eventService = eventService;
        }

        [Authorize]
        public IActionResult All(int? page)
        {
            var events = this.eventService.All();

            var pageNumber = page ?? 1;
            var pagedViewModels = events.ToPagedList(pageNumber, 10);

            this.ViewBag.ViewModels = pagedViewModels;
            this.ViewBag.Page = pageNumber;

            return this.View();
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ServiceFilter(typeof(EventsLogActionFilter))]
        public IActionResult Create(CreateEventBindingModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            this.eventService.CreateEvent(model);

            return this.RedirectToAction("All", "Events");
        }

        [Authorize]
        public IActionResult My(int? page)
        {
            var myEvents = this.eventService.GetMyEvents(this.User.Identity.Name);

            var pageNumber = page ?? 1;
            var pagedEvents = myEvents.ToPagedList(pageNumber, 10);

            this.ViewBag.MyPagedEvents = pagedEvents;
            this.ViewBag.Page = pageNumber;

            return this.View();
        }
    }
}
namespace Eventures.Web.Services
{
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;

    using Eventures.Data;
    using Eventures.Models;
    using Eventures.Web.Services.Contracts;
    using Eventures.Web.ViewModels.Events;

    using Microsoft.EntityFrameworkCore;

    public class EventService : IEventService
    {
        private readonly EventuresDbContext db;

        private readonly IMapper mapper;

        public EventService(EventuresDbContext db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }

        public ICollection<EventViewModel> All()
        {
            var allEvents = this.db.Events.Where(e => e.TotalTickets > 0);

            return allEvents.Select(
                eventuresEvent => this.mapper.Map<EventViewModel>(eventuresEvent))
                .OrderBy(e => e.Start)
                .ToList();
        }

        public void CreateEvent(CreateEventBindingModel model)
        {
            var newEvent = this.mapper.Map<EventuresEvent>(model);

            this.db.Events.Add(newEvent);
            this.db.SaveChanges();
        }

        public ICollection<MyEventsViewModel> GetMyEvents(string username)
        {
            var myEvents = this.db.Orders.Where(o => o.Customer.UserName == username).Include(o => o.Event);

            return myEvents.Select(
                    order => this.mapper.Map<MyEventsViewModel>(order))
                .OrderBy(e => e.Start)
                .ToList();
        }
    }
}
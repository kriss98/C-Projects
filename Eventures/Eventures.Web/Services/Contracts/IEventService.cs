namespace Eventures.Web.Services.Contracts
{
    using System.Collections.Generic;

    using Eventures.Web.ViewModels.Events;

    public interface IEventService
    {
        ICollection<EventViewModel> All();

        void CreateEvent(CreateEventBindingModel model);

        ICollection<MyEventsViewModel> GetMyEvents(string username);
    }
}
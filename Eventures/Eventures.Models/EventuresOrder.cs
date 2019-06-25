namespace Eventures.Models
{
    using System;

    public class EventuresOrder
    {
        public string Id { get; set; }

        public DateTime OrderedOn { get; set; }

        public string EventId { get; set; }

        public virtual EventuresEvent Event { get; set; }

        public string CutomerId { get; set; }

        public virtual EventuresUser Customer { get; set; }

        public int TicketsCount { get; set; }
    }
}
namespace Eventures.Web.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;

    using Eventures.Data;
    using Eventures.Models;
    using Eventures.Web.Services.Contracts;
    using Eventures.Web.ViewModels.Orders;
    using Eventures.Web.ViewModels.Orders.Binding;

    using Microsoft.EntityFrameworkCore;

    public class OrderService : IOrderService
    {
        private readonly EventuresDbContext db;

        private readonly IMapper mapper;

        public OrderService(EventuresDbContext db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }

        public void CreateOrder(CreateOrderBindingModel model)
        {
            var order = this.mapper.Map<EventuresOrder>(model);
            var customer = this.db.Users.FirstOrDefault(u => u.UserName == model.CustomerName);
            var @event = this.db.Events.FirstOrDefault(e => e.Id == model.EventId);

            if (@event.TotalTickets < model.Tickets)
            {
                var ex = new ArgumentOutOfRangeException();
                ex.Data["Tickets"] = @event.TotalTickets.ToString();
                throw ex;
            }

            order.CutomerId = customer.Id;
            order.Customer = customer;
            order.Event = @event;
            order.OrderedOn = DateTime.UtcNow;

            @event.TotalTickets -= model.Tickets;

            this.db.Orders.Add(order);
            this.db.SaveChanges();
        }

        public ICollection<OrderViewModel> All()
        {
            var allOrders = this.db.Orders.Include(o => o.Event).Include(o => o.Customer);

            return allOrders.Select(
                    order => this.mapper.Map<OrderViewModel>(order))
                .OrderBy(o => o.OrderedOn)
                .ToList();
        }
    }
}
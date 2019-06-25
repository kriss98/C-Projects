namespace Chushka.Services
{
    using System.Collections.Generic;
    using System.Linq;

    using Chushka.Data;
    using Chushka.Services.Base;
    using Chushka.Services.Contracts;
    using Chushka.ViewModels.Orders;

    public class OrderService : BaseService, IOrderService
    {
        public OrderService(ChushkaContext context)
            : base(context)
        {
        }

        public ICollection<OrderViewModel> GetAllOrders()
        {
            var orders = this.context.Orders.ToArray();

            var orderViewModels = new List<OrderViewModel>();

            for (var i = 0; i < orders.Length; i++)
            {
                var orderViewModel = new OrderViewModel
                                         {
                                             Id = orders[i].Id.ToString(),
                                             Index = i + 1,
                                             OrderedOn =
                                                 orders[i].OrderedOn.ToString("hh:mm dd/MM/yyyy"),
                                             ClientUsername =
                                                 this.context.Users.FirstOrDefault(
                                                         u => u.Orders.Any(o => o.Id == orders[i].Id))
                                                     .Username,
                                             ProductName =
                                                 this.context.Products.FirstOrDefault(
                                                     p => p.Orders.Any(o => o.Id == orders[i].Id)).Name
                                         };

                orderViewModels.Add(orderViewModel);
            }

            return orderViewModels;
        }
    }
}
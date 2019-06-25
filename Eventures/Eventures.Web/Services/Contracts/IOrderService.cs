namespace Eventures.Web.Services.Contracts
{
    using System.Collections.Generic;

    using Eventures.Web.ViewModels.Orders;
    using Eventures.Web.ViewModels.Orders.Binding;

    public interface IOrderService
    {
        void CreateOrder(CreateOrderBindingModel model);

        ICollection<OrderViewModel> All();
    }
}
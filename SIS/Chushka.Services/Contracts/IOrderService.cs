namespace Chushka.Services.Contracts
{
    using System.Collections.Generic;

    using Chushka.ViewModels.Orders;

    public interface IOrderService
    {
        ICollection<OrderViewModel> GetAllOrders();
    }
}
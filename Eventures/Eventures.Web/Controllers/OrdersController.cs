namespace Eventures.Web.Controllers
{
    using System;
    using System.Linq;

    using Eventures.Web.Services.Contracts;
    using Eventures.Web.ViewModels.Orders;
    using Eventures.Web.ViewModels.Orders.Binding;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using X.PagedList;

    public class OrdersController : Controller
    {
        private readonly IOrderService ordersService;

        public OrdersController(IOrderService ordersService)
        {
            this.ordersService = ordersService;
        }

        [HttpPost]
        [Authorize]
        public IActionResult OrderTickets(CreateOrderBindingModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.RedirectToAction("All", "Events", model);
            }

            try
            {
                this.ordersService.CreateOrder(model);
            }
            catch (Exception e)
            {
                var errorModel = new NotEnoughTicketsErrorViewModel();
                errorModel.TotalTickets = e.Data["Tickets"].ToString();
                return this.RedirectToAction("NotEnoughTicketsError", errorModel);
            }

            return this.RedirectToAction("My", "Events");
        }

        [Authorize(Roles = "Admin")]
        public IActionResult All(int? page)
        {
            var orders = this.ordersService.All();

            var pageNumber = page ?? 1;
            var pagedOrders = orders.ToPagedList(pageNumber, 10);

            this.ViewBag.PagedOrders = pagedOrders;
            this.ViewBag.Page = pageNumber;

            return this.View();
        }

        [Authorize]
        public IActionResult NotEnoughTicketsError(NotEnoughTicketsErrorViewModel model)
        {
            return this.View(model);
        }
    }
}
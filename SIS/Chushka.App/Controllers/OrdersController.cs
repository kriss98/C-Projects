namespace Chushka.App.Controllers
{
    using Chushka.Services.Contracts;

    using SIS.Framework.ActionResults;
    using SIS.Framework.Attributes.Action;
    using SIS.Framework.Attributes.Method;

    public class OrdersController : BaseController
    {
        private readonly IOrderService orderService;

        public OrdersController(IOrderService orderService)
        {
            this.orderService = orderService;
        }

        [HttpGet]
        [Authorize("Admin")]
        public IActionResult All()
        {
            var orders = this.orderService.GetAllOrders();

            this.Model["Orders"] = orders;

            return this.View();
        }
    }
}
namespace Chushka.App.Controllers
{
    using Chushka.Services.Contracts;

    using SIS.Framework.ActionResults;
    using SIS.Framework.Attributes.Method;

    public class HomeController : BaseController
    {
        private readonly IProductService productService;

        public HomeController(IProductService productService)
        {
            this.productService = productService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            if (this.IsLoggedIn())
            {
                this.Model["Username"] = this.Identity.Username;

                var products = this.productService.GetAllProducts();

                this.Model["Products"] = products;
            }

            return this.View();
        }
    }
}
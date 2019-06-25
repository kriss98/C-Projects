namespace Chushka.App.Controllers
{
    using Chushka.Services.Contracts;
    using Chushka.ViewModels;
    using Chushka.ViewModels.Products;

    using SIS.Framework.ActionResults;
    using SIS.Framework.Attributes.Action;
    using SIS.Framework.Attributes.Method;

    public class ProductsController : BaseController
    {
        private readonly IProductService productService;

        public ProductsController(IProductService productService)
        {
            this.productService = productService;
        }

        [HttpGet]
        [Authorize("Admin")]
        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        [Authorize("Admin")]
        public IActionResult Create(ProductCreateViewModel model)
        {
            this.productService.CreateProduct(model);

            return this.RedirectToAction("/");
        }

        [HttpGet]
        [Authorize("Admin")]
        public IActionResult Delete(IdViewModel idModel)
        {
            var model = this.productService.GetProductById(idModel.Id);

            this.Model["Id"] = model.Id;
            this.Model["Name"] = model.Name;
            this.Model["Description"] = model.Description;
            this.Model["Price"] = model.Price;

            this.SetTypeOfProduct(model);

            return this.View();
        }

        [HttpPost]
        [Authorize("Admin")]
        public IActionResult Delete(ProductDetailsViewModel model)
        {
            this.productService.DeleteProductById(model.Id);

            return this.RedirectToAction("/");
        }

        [HttpGet]
        [Authorize]
        public IActionResult Details(IdViewModel idModel)
        {
            var model = this.productService.GetProductById(idModel.Id);

            this.Model["Id"] = model.Id;
            this.Model["Name"] = model.Name;
            this.Model["Description"] = model.Description;
            this.Model["Price"] = model.Price;
            this.Model["Type"] = model.Type;

            return this.View();
        }

        [HttpGet]
        [Authorize("Admin")]
        public IActionResult Edit(IdViewModel idModel)
        {
            var model = this.productService.GetProductById(idModel.Id);

            this.Model["Id"] = model.Id;
            this.Model["Name"] = model.Name;
            this.Model["Description"] = model.Description;
            this.Model["Price"] = model.Price;

            this.SetTypeOfProduct(model);

            return this.View();
        }

        [HttpPost]
        [Authorize("Admin")]
        public IActionResult Edit(ProductDetailsViewModel model)
        {
            this.productService.EditProduct(model);

            return this.RedirectToAction($"/Products/Details?Id={model.Id}");
        }

        [HttpGet]
        [Authorize("Admin")]
        public IActionResult Order(IdViewModel idModel)
        {
            this.productService.OrderProduct(idModel.Id, this.Identity.Username);

            return this.RedirectToAction("/");
        }

        private void SetTypeOfProduct(ProductDetailsViewModel model)
        {
            switch (model.Type)
            {
                case "Food":
                    this.Model["Food"] = "checked";
                    break;
                case "Cosmetic":
                    this.Model["Cosmetic"] = "checked";
                    break;
                case "Domestic":
                    this.Model["Domestic"] = "checked";
                    break;
                case "Health":
                    this.Model["Health"] = "checked";
                    break;
                case "Other":
                    this.Model["Other"] = "checked";
                    break;
            }
        }
    }
}
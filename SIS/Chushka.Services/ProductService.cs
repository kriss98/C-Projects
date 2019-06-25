namespace Chushka.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Chushka.Data;
    using Chushka.Models;
    using Chushka.Models.Enums;
    using Chushka.Services.Base;
    using Chushka.Services.Contracts;
    using Chushka.ViewModels.Products;

    public class ProductService : BaseService, IProductService
    {
        public ProductService(ChushkaContext context)
            : base(context)
        {
        }

        public void CreateProduct(ProductCreateViewModel model)
        {
            var product = new Product
                              {
                                  Name = model.Name,
                                  Description = model.Description,
                                  Price = model.Price,
                                  Type = (ProductType)Enum.Parse(typeof(ProductType), model.Type, true)
                              };

            this.context.Products.Add(product);
            this.context.SaveChanges();
        }

        public void DeleteProductById(int id)
        {
            this.context.Products.Remove(this.context.Products.FirstOrDefault(p => p.Id == id));

            this.context.SaveChanges();
        }

        public void EditProduct(ProductDetailsViewModel model)
        {
            var product = this.context.Products.FirstOrDefault(p => p.Id == model.Id);

            product.Description = model.Description;
            product.Name = model.Name;
            product.Price = model.Price;
            product.Type = (ProductType)Enum.Parse(typeof(ProductType), model.Type, true);

            this.context.SaveChanges();
        }

        public ICollection<ProductIndexViewModel> GetAllProducts()
        {
            var products = this.context.Products;

            var viewModels = new List<ProductIndexViewModel>();

            foreach (var product in products)
            {
                var productViewModel = new ProductIndexViewModel
                                           {
                                               Description =
                                                   product.Description.Length > 50
                                                       ? product.Description.Substring(0, 50)
                                                         + "..."
                                                       : product.Description,
                                               Name = product.Name,
                                               Id = product.Id,
                                               Price = product.Price
                                           };

                viewModels.Add(productViewModel);
            }

            return viewModels;
        }

        public ProductDetailsViewModel GetProductById(int id)
        {
            var product = this.context.Products.FirstOrDefault(p => p.Id == id);

            var productViewModel = new ProductDetailsViewModel();

            productViewModel.Id = id;
            productViewModel.Description = product.Description;
            productViewModel.Name = product.Name;
            productViewModel.Price = product.Price;
            productViewModel.Type = product.Type.ToString();

            return productViewModel;
        }

        public void OrderProduct(int id, string username)
        {
            var product = this.context.Products.FirstOrDefault(p => p.Id == id);

            var user = this.context.Users.FirstOrDefault(u => u.Username == username);

            var order = new Order
                            {
                                ClientId = user.Id,
                                Client = user,
                                Product = product,
                                ProductId = product.Id,
                                OrderedOn = DateTime.UtcNow
                            };

            this.context.Orders.Add(order);
            this.context.SaveChanges();
        }
    }
}
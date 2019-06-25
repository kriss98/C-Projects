namespace Chushka.Services.Contracts
{
    using System.Collections.Generic;

    using Chushka.ViewModels.Products;

    public interface IProductService
    {
        ICollection<ProductIndexViewModel> GetAllProducts();

        void CreateProduct(ProductCreateViewModel model);

        ProductDetailsViewModel GetProductById(int id);

        void EditProduct(ProductDetailsViewModel model);

        void DeleteProductById(int id);

        void OrderProduct(int id, string username);
    }
}
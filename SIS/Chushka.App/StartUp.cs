namespace Chushka.App
{
    using Chushka.Services;
    using Chushka.Services.Contracts;

    using SIS.Framework.Api;
    using SIS.Framework.Services;

    public class StartUp : MvcApplication
    {
        public override void ConfigureServices(IDependencyContainer dependencyContainer)
        {
            dependencyContainer.RegisterDependency<IUserService, UserService>();
            dependencyContainer.RegisterDependency<IProductService, ProductService>();
            dependencyContainer.RegisterDependency<IOrderService, OrderService>();
        }
    }
}
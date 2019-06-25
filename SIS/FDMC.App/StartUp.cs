namespace FDMC.App
{
    using FDMC.Services;
    using FDMC.Services.Contracts;

    using SIS.Framework.Api;
    using SIS.Framework.Services;

    public class StartUp : MvcApplication
    {
        public override void ConfigureServices(IDependencyContainer dependencyContainer)
        {
            dependencyContainer.RegisterDependency<IUserService, UserService>();
            dependencyContainer.RegisterDependency<IKittenService, KittenService>();
        }
    }
}
namespace MeTube.App
{
    using MeTube.Services;
    using MeTube.Services.Contracts;

    using SIS.Framework.Api;
    using SIS.Framework.Services;

    public class StartUp : MvcApplication
    {
        public override void ConfigureServices(IDependencyContainer dependencyContainer)
        {
            dependencyContainer.RegisterDependency<IUserService, UserService>();
            dependencyContainer.RegisterDependency<ITubeService, TubeService>();
        }
    }
}
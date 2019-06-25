namespace MishMash.App
{
    using MishMash.Services;
    using MishMash.Services.Contracts;

    using SIS.Framework.Api;
    using SIS.Framework.Services;

    public class StartUp : MvcApplication
    {
        public override void ConfigureServices(IDependencyContainer dependencyContainer)
        {
            dependencyContainer.RegisterDependency<IUsersService, UserService>();
            dependencyContainer.RegisterDependency<IChannelService, ChannelService>();
        }
    }
}
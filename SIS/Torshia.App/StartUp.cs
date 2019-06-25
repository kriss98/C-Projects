namespace Torshia.App
{
    using SIS.Framework.Api;
    using SIS.Framework.Services;

    using Torshia.Services;
    using Torshia.Services.Contracts;

    public class StartUp : MvcApplication
    {
        public override void ConfigureServices(IDependencyContainer dependencyContainer)
        {
            dependencyContainer.RegisterDependency<IUsersService, UsersService>();
            dependencyContainer.RegisterDependency<ITasksService, TasksService>();
            dependencyContainer.RegisterDependency<IReportService, ReportService>();
        }
    }
}
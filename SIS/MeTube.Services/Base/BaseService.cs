namespace MeTube.Services.Base
{
    using MeTube.Data;

    public abstract class BaseService
    {
        protected readonly MeTubeContext context;

        protected BaseService(MeTubeContext context)
        {
            this.context = context;
        }
    }
}
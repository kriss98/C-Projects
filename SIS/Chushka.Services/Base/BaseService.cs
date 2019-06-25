namespace Chushka.Services.Base
{
    using Chushka.Data;

    public abstract class BaseService
    {
        protected readonly ChushkaContext context;

        protected BaseService(ChushkaContext context)
        {
            this.context = context;
        }
    }
}
namespace Torshia.Services
{
    using Torshia.Data;

    public abstract class BaseService
    {
        protected readonly TorshiaContext context;

        public BaseService(TorshiaContext context)
        {
            this.context = context;
        }
    }
}
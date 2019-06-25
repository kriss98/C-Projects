namespace FDMC.Services.Base
{
    using FDMC.Data;

    public abstract class BaseService
    {
        protected readonly FDMCContext context;

        protected BaseService(FDMCContext context)
        {
            this.context = context;
        }
    }
}
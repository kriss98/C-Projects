namespace MishMash.Services.Base
{
    using MishMash.Data;

    public class BaseService
    {
        protected readonly MishMashContext context;

        public BaseService(MishMashContext context)
        {
            this.context = context;
        }
    }
}
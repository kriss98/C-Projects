namespace Eventures.Web.MiddleWares.Extensions
{
    using Microsoft.AspNetCore.Builder;

    public static class SeedDataExtension
    {
        public static IApplicationBuilder UseSeedDataMiddleWare(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<SeedDataMiddleware>();
        }
    }
}
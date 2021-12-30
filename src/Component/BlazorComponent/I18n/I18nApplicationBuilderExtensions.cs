using BlazorComponent.I18n;
using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.AspNetCore.Builder
{
    public static class I18nApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseMasaI18n(this IApplicationBuilder app)
        {
            app.UseMiddleware<CookieMiddleware>();

            return app;
        }

        public static async Task<IServiceProvider> UseMasaI18n(this IServiceProvider serviceProvider)
        {
            var i18nConfig = serviceProvider.GetRequiredService<I18nConfig>();
            await i18nConfig.Initialization();

            return serviceProvider;
        }
    }
}

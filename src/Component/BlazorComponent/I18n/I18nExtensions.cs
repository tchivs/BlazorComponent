using BlazorComponent.Components;
using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.AspNetCore.Builder
{
    public static class I18nExtensions
    {
        public static IApplicationBuilder UseMasaI18n(this IApplicationBuilder app)
        {
            app.UseMiddleware<CookieMiddleware>();

            return app;
        }

        public static IServiceProvider UseMasaI18n(this IServiceProvider serviceProvider)
        {
            var cookieStorage = serviceProvider.GetRequiredService<CookieStorage>();
            I18nConfig.GloablLanguage = cookieStorage.GetCookie(I18nConfig.LanguageCookieKey).Result;

            return serviceProvider;
        }
    }
}

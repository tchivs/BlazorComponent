using Microsoft.AspNetCore.Http;

namespace BlazorComponent.Components
{
    public class I18nConfig
    {
        CookieStorage? _cookieStorage;

        public I18nConfig() { }

        public I18nConfig(CookieStorage cookieStorage)
        {
            _cookieStorage = cookieStorage;
        }

        public static string LanguageCookieKey { get; set; } = "Masa_I18nConfig_Language";

        private string? _language;

        public string? Language
        {
            get => _language;
            set
            {
                _language = value;
                _cookieStorage?.SetItemAsync(LanguageCookieKey, value);
            }
        }

        public void Initialization(IRequestCookieCollection cookies)
        {
            _language = cookies[LanguageCookieKey];
        }

        public void Bind(I18nConfig i18NConfig)
        {
            _language = i18NConfig.Language;
        }
    }
}

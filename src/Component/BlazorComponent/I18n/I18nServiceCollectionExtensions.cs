using BlazorComponent.Components;
using System.Text.Json;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class I18nServiceCollectionExtensions
    {
        public static IServiceCollection AddI18n(this IServiceCollection services, List<LanguageSetting> languageSettings)
        {
            foreach (var seeting in languageSettings)
            {
                I18n.AddLang(seeting.Value, seeting.FilePath, seeting.IsDefaultLanguage);
            }
            services.AddScoped<I18n>();

            return services;
        }

        public static IServiceCollection AddI18n(this IServiceCollection services, string langugeSettingFilePath)
        {
            var languageSettings = JsonSerializer.Deserialize<List<LanguageSetting>>(File.ReadAllText(langugeSettingFilePath)) ?? throw new Exception("Failed to read i18n josn file data!");
            services.AddI18n(languageSettings);

            return services;
        }
    }

    public class LanguageSetting
    {
        public LanguageSetting(string value, string filePath, bool isDefaultLanguage = false)
        {
            Value = value;
            FilePath = filePath;
            IsDefaultLanguage = isDefaultLanguage;
        }

        public string Value { get; set; }

        public string FilePath { get; set; }

        public bool IsDefaultLanguage { get; set; }
    }
}

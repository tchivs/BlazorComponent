using BlazorComponent.Components;
using System.Text.Json;
using System.IO;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class I18nServiceCollectionExtensions
    {
        public static IServiceCollection AddMasaI18n(this IServiceCollection services, IEnumerable<(string language, Dictionary<string, string>)> languageMap, string? defaultLanguage = null)
        {
            foreach (var (language, map) in languageMap)
            {
                I18n.AddLang(language, map, language == defaultLanguage);
            }
            services.AddScoped<I18n>();


            return services;
        }

        public static IServiceCollection AddMasaI18n(this IServiceCollection services, string langugeDirectory, string? defaultLanguage = null)
        {
            var files = Directory.GetFiles(langugeDirectory);
            var languageMap=new List<(string language, Dictionary<string, string>)>();
            foreach (var filePath in files)
            {
                var language = Path.GetFileName(filePath);
                var map = JsonSerializer.Deserialize<Dictionary<string, string>>(File.ReadAllText(filePath));
                languageMap.Add((language,map));
            }
            services.AddMasaI18n(languageMap, defaultLanguage);

            return services;
        }

        /// <summary>
        /// If you want to customize the languageSetting json file name, use this method
        /// </summary>
        /// <param name="services"></param>
        /// <param name="languageSettings"></param>
        /// <returns></returns>
        public static IServiceCollection AddMasaI18n(this IServiceCollection services, IEnumerable<LanguageSetting> languageSettings)
        {
            var languageMap = languageSettings.Select(setting => (setting.Value, JsonSerializer.Deserialize<Dictionary<string, string>>(File.ReadAllText(setting.FilePath))));
            services.AddMasaI18n(languageMap, languageSettings.FirstOrDefault(setting => setting.IsDefaultLanguage)?.Value);

            return services;
        }

        public static IServiceCollection AddMasaI18n(this IServiceCollection services, string langugeSettingFilePath)
        {
            var languageSettings = JsonSerializer.Deserialize<List<LanguageSetting>>(File.ReadAllText(langugeSettingFilePath)) ?? throw new Exception("Failed to read i18n josn file data!");
            services.AddMasaI18n(languageSettings);

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

using BlazorComponent.I18n;
using System.Text.Json;
using System.IO;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Components;
using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class I18nServiceCollectionExtensions
    {
        static HttpClient _httpClient;

        static string _baseUrl;

        static I18nServiceCollectionExtensions()
        {
            _httpClient = new HttpClient();
            _baseUrl = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        }

        public static IServiceCollection AddMasaI18n(this IServiceCollection services, IEnumerable<(string language, Dictionary<string, string>)> languageMap, string? defaultLanguage = null)
        {
            foreach (var (language, map) in languageMap)
            {
                I18n.AddLang(language, map, language == defaultLanguage);
            }
            services.AddScoped<I18n>();
            services.AddScoped<I18nConfig>();
            services.AddScoped<CookieStorage>();

            return services;
        }

        public static IServiceCollection AddMasaI18n(this IServiceCollection services, string langugeDirectory, string? defaultLanguage = null)
        {
            var files = Directory.GetFiles(Path.Combine(_baseUrl, langugeDirectory));
            var languageMap=new List<(string language, Dictionary<string, string>)>();
            foreach (var filePath in files)
            {
                var language = Path.GetFileNameWithoutExtension(filePath); 
                var map = JsonSerializer.Deserialize<Dictionary<string, string>>(File.ReadAllText(Path.Combine(_baseUrl, filePath)));
                languageMap.Add((language,map));
            }
            services.AddMasaI18n(languageMap, defaultLanguage);

            return services;
        }

        public static IServiceCollection AddMasaI18n(this IServiceCollection services, IEnumerable<LanguageSetting> languageSettings)
        {
            var languageMap = languageSettings.Select(setting => (setting.Value, JsonSerializer.Deserialize<Dictionary<string, string>>(File.ReadAllText(setting.FilePath))));
            services.AddMasaI18n(languageMap, languageSettings.FirstOrDefault(setting => setting.IsDefaultLanguage)?.Value);

            return services;
        }

        public static IServiceCollection AddMasaI18nWithSeetingFile(this IServiceCollection services, string langugeSettingFilePath)
        {
            var languageSettings = JsonSerializer.Deserialize<List<LanguageSetting>>(File.ReadAllText(Path.Combine(_baseUrl, langugeSettingFilePath))) ?? throw new Exception("Failed to read i18n josn file data!");
            services.AddMasaI18n(languageSettings);

            return services;
        }

        /// <summary>
        /// Only supports WebAssembly 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="uri"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static async Task AddMasaI18nWithHttpClient(this IServiceCollection services, IEnumerable<LanguageSetting> languageSettings)
        {
            var languageMap = new List<(string language, Dictionary<string, string>)>();
            foreach (var setting in languageSettings)
            {
                var map = await _httpClient.GetFromJsonAsync<Dictionary<string, string>>(setting.FilePath);
                languageMap.Add((setting.Value, map));
            }
            services.AddMasaI18n(languageMap, languageSettings.FirstOrDefault(setting => setting.IsDefaultLanguage)?.Value);
        }

        /// <summary>
        /// Only supports WebAssembly 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="uri"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static async Task AddMasaI18nWithHttpClient(this IServiceCollection services, string baseUri, string uri)
        {
            _httpClient.BaseAddress = new Uri(baseUri);
            var languageSettings = await _httpClient.GetFromJsonAsync<List<LanguageSetting>>(uri) ?? throw new Exception("Failed to read i18n josn file data!");
            await services.AddMasaI18nWithHttpClient(languageSettings);
        }

        public static ParameterView GetMasaI18nParameter(this IServiceProvider servicesProvider)
        {
            var i18nConfig = servicesProvider.GetRequiredService<I18nConfig>();
            return ParameterView.FromDictionary(new Dictionary<string, object?> { [nameof(I18nConfig)] = i18nConfig });
        }
    }

    public class LanguageSetting
    {
        public LanguageSetting(string value, string? filePath, bool isDefaultLanguage = false)
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

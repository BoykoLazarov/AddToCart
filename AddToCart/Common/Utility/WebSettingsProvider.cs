using AddToCart.Common.Objects;

namespace AddToCart.Common.Utility
{
    public static class WebSettingsProvider
    {
        private static string WebSettingsPath { get; } = Path.Combine(Environment.CurrentDirectory, "Settings", "WebSettings.json");

        public static WebSettings GetSettings()
        {
            return JsonFileReader.ReadJsonFile<WebSettings>(WebSettingsPath);
        }
    }
}
using AddToCart.Common.Utility;

namespace AddToCart.Common.Driver
{
    public static class Timeouts
    {
        private const int DEFAULT_TIMEOUT = 30;

        public static TimeSpan GetDefaultBrowserTimeout()
        {
            int? defaultTimeoutSettignsValue = WebSettingsProvider.GetSettings().BrowserPageTimeout;
            int defaultTimeout = defaultTimeoutSettignsValue ?? DEFAULT_TIMEOUT;

            return TimeSpan.FromSeconds(defaultTimeout);
        }
    }
}
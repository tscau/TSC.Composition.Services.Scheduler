using System.Configuration;

namespace TSC.Composition.Services.Scheduler.Config
{
    /// <summary>
    /// TGhe Logging section in the app.config file
    /// </summary>
    public class LoggingSettings : ILoggingSettings
    {
        /// <summary>
        /// The URL to use when logging to the central logging service
        /// </summary>
        public string LogServiceUrl
        {
            get
            {
                string logServiceUrl = ConfigurationManager.AppSettings.Get("LogServiceUrl");
                if (string.IsNullOrWhiteSpace(logServiceUrl))
                {
                    logServiceUrl = string.Empty;
                }

                return logServiceUrl;
            }
        }
    }
}

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
            set
            {
                LogServiceUrl = value;
            }
        }

        private string _logFileName;
        /// <summary>
        /// 
        /// </summary>
        public string BatchLogFileName
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_logFileName))
                {
                    _logFileName = ConfigurationManager.AppSettings.Get("BatchLogFileName");
                    if (string.IsNullOrWhiteSpace(_logFileName))
                    {
                        _logFileName = "composition.scheduler-.log";
                    }
                }
                
                return _logFileName;
            }
            set
            {
                _logFileName = value;
            }
        }


        private string _logFileName2;
        /// <summary>
        /// 
        /// </summary>
        public string BatchLogFileName2
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_logFileName2))
                {
                    _logFileName2 = ConfigurationManager.AppSettings.Get("BatchLogFileName2");
                    if (string.IsNullOrWhiteSpace(_logFileName))
                    {
                        _logFileName = "composition.scheduler_002-.log";
                    }
                }

                return _logFileName2;
            }
            set
            {
                _logFileName2 = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string RelativeJobLogFileLocation {
            get
            {
                string relativeJobLogFileLocation = ConfigurationManager.AppSettings.Get("RelativeJobLogFileLocation");
                if (string.IsNullOrWhiteSpace(relativeJobLogFileLocation))
                {
                    relativeJobLogFileLocation = "Logs";
                }

                return relativeJobLogFileLocation;
            }
            set
            {
                RelativeJobLogFileLocation = value;
            }
        }

        string _batchLogFileBasePath;
        /// <summary>
        /// 
        /// </summary>
        public string BatchLogFileBasePath {
            get
            {
                _batchLogFileBasePath = ConfigurationManager.AppSettings.Get("BatchLogFileBasePath");
                if (string.IsNullOrWhiteSpace(_batchLogFileBasePath))
                {
                    _batchLogFileBasePath = "/CorresSystem/LOCAL/Jobs/Batch";
                }

                return _batchLogFileBasePath;
            }
            set
            {
                _batchLogFileBasePath = value;
            }
        }
    }
}

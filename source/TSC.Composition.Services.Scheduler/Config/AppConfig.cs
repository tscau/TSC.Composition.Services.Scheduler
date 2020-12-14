namespace TSC.Composition.Services.Scheduler.Config
{
    /// <summary>
    /// 
    /// </summary>
    public class AppConfig : IAppConfig
    {
        /// <summary>
        /// 
        /// </summary>
        public AppConfig()
        {
            AppSettings = new AppSettings();
            Logging = new LoggingSettings();
            Kestrel = new KestrelSettings();
            ConnectionStrings = new ConnectionStrings();
        }


        /// <summary>
        /// 
        /// </summary>
        public IAppSettings AppSettings { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ILoggingSettings Logging { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public IKestrelSettings Kestrel { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public IConnectionStrings ConnectionStrings { get; set; }
    }
}

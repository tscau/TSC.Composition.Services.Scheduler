namespace TSC.Composition.Services.Scheduler.Config
{
    /// <summary>
    /// 
    /// </summary>
    public interface IAppConfig
    {
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

namespace TSC.Composition.Services.Scheduler.Config
{
    /// <summary>
    /// 
    /// </summary>
    public interface ILoggingSettings
    {
        /// <summary>
        /// 
        /// </summary>
        public string BatchLogFileName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string BatchLogFileName2 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string RelativeJobLogFileLocation { get; set; }



        /// <summary>
        /// 
        /// </summary>
        public string LogServiceUrl { get; set; }

        public string BatchLogFileBasePath { get; set; }
    }
}

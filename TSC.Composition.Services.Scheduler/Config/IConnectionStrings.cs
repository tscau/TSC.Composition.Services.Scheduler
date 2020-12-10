namespace TSC.Composition.Services.Scheduler.Config
{
    /// <summary>
    /// 
    /// </summary>
    public interface IConnectionStrings
    {
        /// <summary>
        /// 
        /// </summary>
        public string MonolithicDB { get; }

        /// <summary>
        /// 
        /// </summary>
        public string HangfireCompositionDb { get; }

        /// <summary>
        /// 
        /// </summary>
        public string HangfireDeliveryDb { get; }
    }
}

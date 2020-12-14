namespace TSC.Composition.Services.Scheduler.Config
{
    /// <summary>
    /// 
    /// </summary>
    public interface IKestrelSettings
    {
        /// <summary>
        /// 
        /// </summary>
        public int RESTAPIPort { get; }

        /// <summary>
        /// 
        /// </summary>
        public bool UseCertificateForSSL { get; }

        /// <summary>
        /// 
        /// </summary>
        public string CertificateSubject { get; }

        /// <summary>
        /// 
        /// </summary>
        public string CertificateStoreName { get; }

        /// <summary>
        /// 
        /// </summary>
        public string CertificateLocation { get; }

        /// <summary>
        /// 
        /// </summary>
        public bool CertificateAllowInvalid { get; }
    }
}

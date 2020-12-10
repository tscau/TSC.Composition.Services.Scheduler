using System.Configuration;

namespace TSC.Composition.Services.Scheduler.Config
{
    /// <summary>
    /// 
    /// </summary>
    public class KestrelSettings : IKestrelSettings
    {
        /// <summary>
        /// 
        /// </summary>
        public int RESTAPIPort
        {
            get
            {
                int.TryParse(ConfigurationManager.AppSettings.Get("RESTAPIPort"), out int port);
                return port;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool UseCertificateForSSL
        {
            get
            {
                bool.TryParse(ConfigurationManager.AppSettings.Get("UseCertificateForSSL"), out bool useSSL);
                return useSSL;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string CertificateSubject
        {
            get
            {
                return ConfigurationManager.AppSettings.Get("CertificateSubject");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string CertificateStoreName
        {
            get
            {
                return ConfigurationManager.AppSettings.Get("CertificateStoreName");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string CertificateLocation
        {
            get
            {
                return ConfigurationManager.AppSettings.Get("CertificateLocation");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool CertificateAllowInvalid
        {
            get
            {
                bool.TryParse(ConfigurationManager.AppSettings.Get("CertificateAllowInvalid"), out bool allowInvalid);
                return allowInvalid;
            }
        }
    }
}

using System.Configuration;

namespace TSC.Composition.Services.Scheduler.Config
{
    /// <summary>
    /// 
    /// </summary>
    public class ConnectionStrings : IConnectionStrings
    {

        /// <summary>
        /// The Connection String for the Outbound database
        /// </summary>
        public string MonolithicDB
        {
            get
            {
                ConnectionStringSettings connectionStrings = ConfigurationManager.ConnectionStrings["MonolithicDB"];
                if (connectionStrings is null)
                {
                    return string.Empty;
                }


                string outboundCorrogen = ConfigurationManager.ConnectionStrings["MonolithicDB"].ConnectionString;
                if (string.IsNullOrWhiteSpace(outboundCorrogen))
                {
                    outboundCorrogen = string.Empty;
                }
                return outboundCorrogen;
            }
        }

        /// <summary>
        /// The connections tring for the hangfire database
        /// </summary>
        public string HangfireCompositionDb
        {
            get
            {
                ConnectionStringSettings connectionStrings = ConfigurationManager.ConnectionStrings["HangfireComposition"];
                if (connectionStrings is null)
                {
                    return string.Empty;
                }

                string hangfire = ConfigurationManager.ConnectionStrings["HangfireComposition"].ConnectionString;
                if (string.IsNullOrWhiteSpace(hangfire))
                {
                    hangfire = string.Empty;
                }
                return hangfire;
            }
        }


        /// <summary>
        /// The Hangfire database that will be used for post composition services
        /// </summary>
        public string HangfireDeliveryDb
        {
            get
            {
                ConnectionStringSettings connectionStrings = ConfigurationManager.ConnectionStrings["HangfireDelivery"];
                if (connectionStrings is null)
                {
                    return string.Empty;
                }

                string hangfireDeliveryDb = ConfigurationManager.ConnectionStrings["HangfireDelivery"].ConnectionString;
                if (string.IsNullOrWhiteSpace(hangfireDeliveryDb))
                {
                    hangfireDeliveryDb = string.Empty;
                }
                return hangfireDeliveryDb;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Reflection;

namespace TSC.Composition.Services.Scheduler.Config
{
    /// <summary>
    /// 
    /// </summary>
    public class AppSettings : IAppSettings
    {
        /// <summary>
        /// 
        /// </summary>
        public string Environment
        {
            get
            {
                return ConfigurationManager.AppSettings.Get("Env");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int DaysToFilter
        {
            get
            {
                int.TryParse(ConfigurationManager.AppSettings.Get("DaysToFilter"), out int daysToFilter);
                if (daysToFilter == 0)
                {
                    daysToFilter = 90;
                }

                return daysToFilter;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int HangfireWorkerCount
        {
            get
            {
                int.TryParse(ConfigurationManager.AppSettings.Get("HangfireWorkerCount"), out int hangfireWorkerCount);
                if (hangfireWorkerCount == 0)
                {
                    hangfireWorkerCount = 10;
                }

                return hangfireWorkerCount;
            }
        }

        /// <summary>
        /// Max records per csv file before splitting the file into multiples
        /// Defaults to 50,000
        /// </summary>
        public int MaxRecordsPerCsvFile
        {
            get
            {
                int.TryParse(ConfigurationManager.AppSettings.Get(nameof(MaxRecordsPerCsvFile)), out int maxRecordsPerCsvFile);
                if (maxRecordsPerCsvFile == 0)
                {
                    maxRecordsPerCsvFile = 50_000;
                }

                return maxRecordsPerCsvFile;
            }
        }

        /// <summary>
        /// When a csv file has been split into multiple batches, this is the time to wait between scheduling each batch
        /// Defaults to 15 seconds
        /// </summary>
        public TimeSpan DelayBetweenCsvSchedules
        {
            get
            {
                TimeSpan.TryParse(ConfigurationManager.AppSettings.Get(nameof(DelayBetweenCsvSchedules)), out TimeSpan delayBetweenCsvSchedules);
                if (delayBetweenCsvSchedules == TimeSpan.Zero)
                {
                    delayBetweenCsvSchedules = TimeSpan.FromSeconds(15);
                }

                return delayBetweenCsvSchedules;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string UserToRecordAgainst
        {
            get
            {
                return ConfigurationManager.AppSettings.Get("UserToRecordAgainst");
            }
        }

        /// <summary>
        /// Returns the version of the application
        /// </summary>
        public Dictionary<string, AssemblyVersionDetails> Versions
        {
            get
            {
                Dictionary<string, AssemblyVersionDetails> versions = new Dictionary<string, AssemblyVersionDetails>();
                AssemblyVersionDetails details = new AssemblyVersionDetails
                {
                    ReleaseBuild = Assembly.GetExecutingAssembly().GetName().Version.ToString(),
                    ReleaseVersion = Assembly.GetExecutingAssembly().GetCustomAttribute<AssemblyFileVersionAttribute>().Version,
                    BuildNumber = Assembly.GetExecutingAssembly().GetCustomAttribute<AssemblyInformationalVersionAttribute>().InformationalVersion,
                    ReleaseName = Assembly.GetExecutingAssembly().GetCustomAttribute<AssemblyProductAttribute>().Product
                };
                versions.Add(Assembly.GetExecutingAssembly().GetName().Name, details);

                Assembly sharedAssembly = Assembly.Load("DCS.Composition.Services.Shared");
                details = new AssemblyVersionDetails
                {
                    ReleaseBuild = sharedAssembly.GetName().Version.ToString(),
                    ReleaseVersion = sharedAssembly.GetCustomAttribute<AssemblyFileVersionAttribute>().Version,
                    BuildNumber = sharedAssembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>().InformationalVersion,
                    ReleaseName = sharedAssembly.GetCustomAttribute<AssemblyProductAttribute>().Product
                };
                versions.Add(sharedAssembly.GetName().Name, details);

                sharedAssembly = Assembly.Load("DCS.PostComposition.Services.Shared");
                details = new AssemblyVersionDetails
                {
                    ReleaseBuild = sharedAssembly.GetName().Version.ToString(),
                    ReleaseVersion = sharedAssembly.GetCustomAttribute<AssemblyFileVersionAttribute>().Version,
                    BuildNumber = sharedAssembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>().InformationalVersion,
                    ReleaseName = sharedAssembly.GetCustomAttribute<AssemblyProductAttribute>().Product
                };
                versions.Add(sharedAssembly.GetName().Name, details);

                //add the shared assemblies as well
                sharedAssembly = Assembly.Load("DCS.Shared.DataAccess.Outbound");
                details = new AssemblyVersionDetails
                {
                    ReleaseBuild = sharedAssembly.GetName().Version.ToString(),
                    ReleaseVersion = sharedAssembly.GetCustomAttribute<AssemblyFileVersionAttribute>().Version,
                    BuildNumber = sharedAssembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>().InformationalVersion,
                    ReleaseName = sharedAssembly.GetCustomAttribute<AssemblyProductAttribute>().Product
                };
                versions.Add(sharedAssembly.GetName().Name, details);
                return versions;
            }
        }

        /// <summary>
        /// Returns the name of the server that is currently executing the code
        /// </summary>
        public string Server => System.Environment.MachineName;

        /// <summary>
        /// 
        /// </summary>
        public string JobFolderRootPath
        {
            get
            {
                string jobFolderRootPath = ConfigurationManager.AppSettings.Get("JobFolderRootPath");
                if (string.IsNullOrWhiteSpace(jobFolderRootPath))
                {
                    return string.Empty;
                }
                else
                {
                    return jobFolderRootPath;
                }
            }
        }

        /// <summary>
        /// </summary>
        public string BaseStagingFolder
        {
            get
            {
                string baseStagingFolder = ConfigurationManager.AppSettings.Get("BaseStagingFolder");
                if (string.IsNullOrWhiteSpace(baseStagingFolder))
                {
                    return string.Empty;
                }
                else
                {
                    return baseStagingFolder;
                }
            }
        }
    }
}

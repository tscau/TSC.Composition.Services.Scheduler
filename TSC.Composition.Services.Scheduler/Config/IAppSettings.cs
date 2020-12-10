using System;
using System.Collections.Generic;

namespace TSC.Composition.Services.Scheduler.Config
{
    /// <summary>
    /// 
    /// </summary>
    public interface IAppSettings
    {
        /// <summary>
        /// 
        /// </summary>
        public string Environment { get; }

        /// <summary>
        /// 
        /// </summary>
        public int DaysToFilter { get; }

        /// <summary>
        /// 
        /// </summary>
        public int HangfireWorkerCount { get; }

        /// <summary>
        /// 
        /// </summary>
        public string UserToRecordAgainst { get; }

        /// <summary>
        /// The version of the application abd dependent assemblies
        /// </summary>
        public Dictionary<string, AssemblyVersionDetails> Versions { get; }

        /// <summary>
        /// 
        /// </summary>
        public string Server { get; }

        /// <summary>
        /// 
        /// </summary>
        public int MaxRecordsPerCsvFile { get; }

        /// <summary>
        /// 
        /// </summary>
        TimeSpan DelayBetweenCsvSchedules { get; }

        /// <summary>
        /// 
        /// </summary>
        public string JobFolderRootPath { get; }

        /// <summary>
        /// 
        /// </summary>
        public string BaseStagingFolder { get; }
    }
}

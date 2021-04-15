using Serilog;
using Serilog.Core;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace TSC.Composition.Services.Scheduler.Infrastructure
{
    /// <summary>
    /// 
    /// </summary>
    public static class PerBatchLog
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="format"></param>
        /// <param name="args"></param>
        [Pure]
        public delegate void LogDelegate(string format, params object[] args);

        private const string SETTING_PREFIX = "per-batch-2:serilog:";
        private const string FILE_PATH_KEY = "write-to:File.path";


        private static Logger _batchLogger;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="batchId"></param>
        /// <param name="jobPath"></param>
        ///<param name="logFileName"></param>
        ///<param name="logFolderName"></param>
        /// <returns></returns>
        public static Logger CreateForBatchId(long batchId, string jobPath, string logFolderName, string logFileName)
        {
            if (string.IsNullOrWhiteSpace(jobPath))
            {
                throw new ArgumentException("Value cannot be null or whitespace", nameof(jobPath));
            }

            var settings = ConfigurationManager.AppSettings.AllKeys
                    .Where(key => key.StartsWith(SETTING_PREFIX))
                    .Select(key =>
                    {
                        var keyWithoutPrefix = key.Substring(SETTING_PREFIX.Length);
                        var settingValue = ConfigurationManager.AppSettings[key];
                        return new KeyValuePair<string, string>(keyWithoutPrefix, settingValue);
                    })
                    .ToList();


            var logPath = Path.Join(jobPath, batchId.ToString(), logFolderName, logFileName);
            settings.Add(new KeyValuePair<string, string>(FILE_PATH_KEY, logPath));

            _batchLogger = new LoggerConfiguration()
                .Enrich.WithProperty("BatchId", batchId)
                .Enrich.WithMachineName()
                .ReadFrom.KeyValuePairs(settings)
                .CreateLogger();
            return _batchLogger;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="log"></param>
        /// <param name="callerMemberName"></param>
        /// <param name="callerFilePath"></param>
        /// <param name="callerLineNUmber"></param>
        /// <returns></returns>
        public static LogDelegate Fatal([CallerMemberName] string callerMemberName = "", [CallerFilePath] string callerFilePath = "", [CallerLineNumber] int callerLineNUmber = 0)
        {
            return new LogDelegate((format, args) =>
            {
                _batchLogger.Fatal(format + $" [{callerMemberName:l}|{callerFilePath:l}()|{callerLineNUmber}]", CombineArgs(callerMemberName, callerFilePath, callerLineNUmber, args));
                //Log.Error(format + $" [{callerMemberName:l}|{callerFilePath:l}()|{callerLineNUmber}]", CombineArgs(callerMemberName, callerFilePath, callerLineNUmber, args));
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="callerMemberName"></param>
        /// <param name="callerFilePath"></param>
        /// <param name="callerLineNumber"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        private static object[] CombineArgs(string callerMemberName, string callerFilePath, int callerLineNumber, object[] args)
        {
            var args2 = new object[args.Length + 3];
            args.CopyTo(args2, 0);
            args2[args.Length] = callerFilePath;
            args2[args.Length + 1] = callerMemberName;
            args2[args.Length + 2] = callerLineNumber;
            return args2;
        }
    }
}


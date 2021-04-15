using Hangfire.Server;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Context;
using Serilog.Core;
using System;
using TSC.Composition.Services.Scheduler.Config;
using TSC.Composition.Services.Scheduler.Infrastructure;
using TSC.Composition.Services.Shared.Interfaces;
using TSC.Composition.Services.Shared.MessageFormats;

namespace TSC.Composition.Services.Scheduler.Domain
{
    /// <summary>
    /// 
    /// </summary>
    public class SchedulerImplementation : IScheduler
    {
        readonly IAppConfig _appConfig;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="appConfig"></param>
        public SchedulerImplementation(IAppConfig appConfig)
        {
            _appConfig = appConfig;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public string RunHighVolumeSchedule(CompositionMessage msg, PerformContext context)
        {
            IPerBatchLogger perBatchLogger = new PerBatchLogger();
            Logger batchLogger = perBatchLogger.CreateForBatchId(9999, _appConfig.Logging.BatchLogFileBasePath, "Logs", _appConfig.Logging.BatchLogFileName);
            PerBatchLog.CreateForBatchId(9999, _appConfig.Logging.BatchLogFileBasePath, "Logs", _appConfig.Logging.BatchLogFileName2);

            try
            {
                object[] args = new object[] { };
                perBatchLogger.Debug("RunHighVolumeSchedule::This is a DEBUG log entry", args);
                perBatchLogger.Verbose("RunHighVolumeSchedule::This is a VERBOSE log entry", args);
                perBatchLogger.Info("RunHighVolumeSchedule::This is a INFO log entry", args);
                perBatchLogger.Warn("RunHighVolumeSchedule::This is a WARN log entry", args);
                perBatchLogger.Fatal("RunHighVolumeSchedule::This is a FATAL log entry", args);
                Exception ex = new Exception("RunHighVolumeSchedule::This is a manufactured exception");
                perBatchLogger.Error(ex, "RunHighVolumeSchedule::This is a ERROR log entry with an exception", args);
                perBatchLogger.Error(null, "RunHighVolumeSchedule::This is a ERROR log entry", args);

                Log.ForContext("BatchId", 9999).Error(ex, "RunHighVolumeSchedule::This is a ERROR log entry with an exception usiing Log: {message}; {stackTrace};", ex.Message, ex.StackTrace);

                //Try the second way of doing it
                PerBatchLog.Fatal()($"RunHighVolumeSchedule::This is a ERROR log entry with an exception using PerBatchLog Static class: {ex.Message}; {ex.StackTrace};", ex.Message, ex.StackTrace);

                return "success";
            }
            finally
            {
                batchLogger.Dispose();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public string RunLowVolumeSchedule(CompositionMessage message, PerformContext context)
        {
            IPerBatchLogger perBatchLogger = new PerBatchLogger();
            Logger batchLogger = perBatchLogger.CreateForBatchId(9999, _appConfig.Logging.BatchLogFileBasePath, "Logs", _appConfig.Logging.BatchLogFileName);

            try
            {
                object[] args = new object[] { };
                perBatchLogger.Debug("RunLowVolumeSchedule::This is a DEBUG log entry", args);
                perBatchLogger.Verbose("RunLowVolumeSchedule::This is a VERBOSE log entry", args);
                perBatchLogger.Info("RunLowVolumeSchedule::This is a INFO log entry", args);
                perBatchLogger.Warn("RunLowVolumeSchedule::This is a WARN log entry", args);
                perBatchLogger.Fatal("RunLowVolumeSchedule::This is a FATAL log entry", args);
                Exception ex = new Exception("RunLowVolumeSchedule::This is a manufactured exception");
                perBatchLogger.Error(ex, "RunLowVolumeSchedule::This is a ERROR log entry with an exception", args);
                perBatchLogger.Error(null, "RunLowVolumeSchedule::This is a ERROR log entry", args);

                Log.ForContext("BatchId", 9999).Error(ex, "RunLowVolumeSchedule::This is a ERROR log entry with an exception usiing Log: {message}; {stackTrace};", ex.Message, ex.StackTrace);

                return "success";
            }
            finally
            {
                batchLogger.Dispose();
            }
        }
    }
}

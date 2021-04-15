using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using Serilog;
using Serilog.Core;

namespace TSC.Composition.Services.Scheduler.Infrastructure
{

    public interface IPerBatchLogger
    {
        Logger CreateForBatchId(long batchId, string jobPath, string logFolderName, string logFileName);

        public void Debug(string format, object[] args, [CallerMemberName] string callerMemberName = "", [CallerFilePath] string callerFilePath = "", [CallerLineNumber] int callerLineNumber = 0);

        public void Verbose(string format, object[] args, [CallerMemberName] string callerMemberName = "", [CallerFilePath] string callerFilePath = "", [CallerLineNumber] int callerLineNumber = 0);

        public void Info(string format, object[] args, [CallerMemberName] string callerMemberName = "", [CallerFilePath] string callerFilePath = "", [CallerLineNumber] int callerLineNumber = 0);


        public void Warn(string format, object[] args, [CallerMemberName] string callerMemberName = "", [CallerFilePath] string callerFilePath = "", [CallerLineNumber] int callerLineNumber = 0);

        public void Fatal(string format, object[] args, [CallerMemberName] string callerMemberName = "", [CallerFilePath] string callerFilePath = "", [CallerLineNumber] int callerLineNumber = 0);

        public void Error(Exception ex, string format, object[] args, [CallerMemberName] string callerMemberName = "", [CallerFilePath] string callerFilePath = "", [CallerLineNumber] int callerLineNumber = 0);

    }

    /// <summary>
    /// 
    /// </summary>
    public class PerBatchLogger : IPerBatchLogger
    {
        private Logger _loggerForBatch;

        private const string SETTING_PREFIX = "per-batch:serilog:";
        private const string FILE_PATH_KEY = "write-to:File.path";

        //[Pure]
        //public delegate void LogDelegate(string format, params object[] args);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="batchId"></param>
        /// <param name="jobPath"></param>
        ///<param name="logFileName"></param>
        ///<param name="logFolderName"></param>
        /// <returns></returns>
        public Logger CreateForBatchId(long batchId, string jobPath, string logFolderName, string logFileName)
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

            _loggerForBatch = new LoggerConfiguration()
                .Enrich.WithProperty("BatchId", batchId)
                .Enrich.WithMachineName()
                .ReadFrom.KeyValuePairs(settings)
                .CreateLogger();
            return _loggerForBatch;
        }

        //Delegates
        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        /// <param name="format"></param>
        /// <param name="callerMemberName"></param>
        /// <param name="callerFilePath"></param>
        /// <param name="callerLineNumber"></param>
        /// <returns></returns>
        public void Fatal(string format, object[]  args, [CallerMemberName] string callerMemberName = "", [CallerFilePath] string callerFilePath = "", [CallerLineNumber] int callerLineNumber = 0)
        {
            object[] combinedArgs = CombineArgs(callerMemberName, callerFilePath, callerLineNumber, args);
            _loggerForBatch.Fatal(format + $"[{callerFilePath:l}|{callerMemberName:l}|Line:{callerLineNumber}]", combinedArgs);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="format"></param>
        /// <param name="args"></param>
        /// <param name="callerMemberName"></param>
        /// <param name="callerFilePath"></param>
        /// <param name="callerLineNumber"></param>
        /// <returns></returns>
        public void Debug(string format, object[] args, [CallerMemberName] string callerMemberName = "", [CallerFilePath] string callerFilePath = "", [CallerLineNumber] int callerLineNumber = 0)
        {
            object[] combinedArgs = CombineArgs(callerMemberName, callerFilePath, callerLineNumber, args);
            _loggerForBatch.Debug(format + $"[{callerFilePath:l}|{callerMemberName:l}|Line:{callerLineNumber}]", combinedArgs);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="args"></param>
        /// <param name="format"></param>
        /// <param name="callerMemberName"></param>
        /// <param name="callerFilePath"></param>
        /// <param name="callerLineNumber"></param>
        /// <returns></returns>
        public void Error(Exception ex, string format, object[] args, [CallerMemberName] string callerMemberName = "", [CallerFilePath] string callerFilePath = "", [CallerLineNumber] int callerLineNumber = 0)
        {
            object[] combinedArgs = CombineArgs(callerMemberName, callerFilePath, callerLineNumber, args);
            if (ex == null)
            {
                _loggerForBatch.Error(format + $"[{callerFilePath:l}|{callerMemberName:l}|Line:{callerLineNumber}]", combinedArgs);
            } else
            {
                _loggerForBatch.Error(ex, format + $"[{callerFilePath:l}|{callerMemberName:l}|Line:{callerLineNumber}]", combinedArgs);
            }
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        /// <param name="format"></param>
        /// <param name="callerMemberName"></param>
        /// <param name="callerFilePath"></param>
        /// <param name="callerLineNumber"></param>
        /// <returns></returns>
        public void Info(string format, object[] args, [CallerMemberName] string callerMemberName = "", [CallerFilePath] string callerFilePath = "", [CallerLineNumber] int callerLineNumber = 0)
        {
            object[] combinedArgs = CombineArgs(callerMemberName, callerFilePath, callerLineNumber, args);
            _loggerForBatch.Information(format + $"[{callerFilePath:l}|{callerMemberName:l}|Line:{callerLineNumber}]", combinedArgs);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        /// <param name="format"></param>
        /// <param name="callerMemberName"></param>
        /// <param name="callerFilePath"></param>
        /// <param name="callerLineNumber"></param>
        /// <returns></returns>
        public void Verbose(string format, object[] args, [CallerMemberName] string callerMemberName = "", [CallerFilePath] string callerFilePath = "", [CallerLineNumber] int callerLineNumber = 0)
        {
            object[] combinedArgs = CombineArgs(callerMemberName, callerFilePath, callerLineNumber, args);
            _loggerForBatch.Verbose(format + $"[{callerFilePath:l}|{callerMemberName:l}|Line:{callerLineNumber}]", combinedArgs);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        /// <param name="format"></param>
        /// <param name="callerMemberName"></param>
        /// <param name="callerFilePath"></param>
        /// <param name="callerLineNumber"></param>
        /// <returns></returns>
        public void Warn(string format, object[] args, [CallerMemberName] string callerMemberName = "", [CallerFilePath] string callerFilePath = "", [CallerLineNumber] int callerLineNumber = 0)
        {
            object[] combinedArgs = CombineArgs(callerMemberName, callerFilePath, callerLineNumber, args);
            _loggerForBatch.Warning(format + $"[{callerFilePath:l}|{callerMemberName:l}|Line:{callerLineNumber}]", combinedArgs);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="callerMemberName"></param>
        /// <param name="callerFilePath"></param>
        /// <param name="callerLineNumber"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        private object[] CombineArgs(string callerMemberName, string callerFilePath, int callerLineNumber, object[] args)
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

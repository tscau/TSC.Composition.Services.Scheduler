using Serilog.Core;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Text;
using System.Threading.Tasks;

namespace TSC.Composition.Services.Scheduler.Infrastructure
{
    /// <summary>
    /// 
    /// </summary>
    public class TSCLoggingSink : ILogEventSink, IDisposable
    {
        private static readonly HttpClient client = new HttpClient();

        private readonly IFormatProvider _formatProvider;
        private bool disposedValue;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="formatrProvider"></param>
        /// <param name="logServiceUrl"></param>
        public TSCLoggingSink(IFormatProvider formatrProvider, string logServiceUrl)
        {
            _formatProvider = formatrProvider;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logEvent"></param>
        public void Emit(LogEvent logEvent)
        {
            var message = logEvent.RenderMessage(_formatProvider);
            //send the message to the Logging REST endpoint
            //Read logService from the config - skip if empty
            string logServiceUrl = ConfigurationManager.AppSettings.Get("LogServiceUrl");
            if (!string.IsNullOrWhiteSpace(logServiceUrl))
            {
                StringContent content = new StringContent(message, Encoding.UTF8, "application/json");
                client.PostAsync(logServiceUrl, content);
            }
            Console.WriteLine(DateTimeOffset.Now.ToString() + " " + message);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // dispose managed state (managed objects)
                }

                // free unmanaged resources (unmanaged objects) and override finalizer
                // set large fields to null
                disposedValue = true;
            }
        }

        //  override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~TSCLoggingSink()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}

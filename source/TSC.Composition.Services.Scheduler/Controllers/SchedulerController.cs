using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Serilog;
using TSC.Composition.Services.Scheduler.Config;
using TSC.Composition.Services.Scheduler.Domain;
using TSC.Composition.Services.Shared.MessageFormats;

namespace TSC.Composition.Services.Scheduler.Controllers
{
    /// <summary>
    /// The main controller that calls the methods
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class SchedulerController : ControllerBase
    {
        readonly IAppConfig _appConfig;

        /// <summary>
        /// Default constructor that sets the objects passed to it from the Dependancy Injection framework
        /// </summary>
        /// <param name="appConfig"></param>
        public SchedulerController(IAppConfig appConfig)
        {
            _appConfig = appConfig;
        }


        /// <summary>
        /// REST Endpoint to place the message on teh high priority queue
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("runhighpriority")]
        public ActionResult RunHighPriority(CompositionMessage message)
        {
            TelemetryConfiguration tcconfig = new TelemetryConfiguration("7ff36511-0d21-4f97-b2f4-27bbc6bc9461");
            tcconfig.ConnectionString = "InstrumentationKey=7ff36511-0d21-4f97-b2f4-27bbc6bc9461;IngestionEndpoint=https://australiaeast-0.in.applicationinsights.azure.com/";
            TelemetryClient tc = new TelemetryClient(tcconfig);
            SchedulerImplementation impl = new SchedulerImplementation(_appConfig, tc);
            impl.RunHighVolumeSchedule(message, null);
            return new OkResult();
        }

        /// <summary>
        /// REST Endpoint to place the message on teh low priority queue
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("runlowpriority")]
        public ActionResult RunLowPriority(CompositionMessage message)
        {
            TelemetryConfiguration tcconfig = new TelemetryConfiguration("7ff36511-0d21-4f97-b2f4-27bbc6bc9461");
            tcconfig.ConnectionString = "InstrumentationKey=7ff36511-0d21-4f97-b2f4-27bbc6bc9461;IngestionEndpoint=https://australiaeast-0.in.applicationinsights.azure.com/";
            TelemetryClient tc = new TelemetryClient(tcconfig);
            SchedulerImplementation impl = new SchedulerImplementation(_appConfig, tc);
            impl.RunLowVolumeSchedule(message, null);
            return new OkResult();
        }

        /// <summary>
        /// REST Endpoint to call the schedule directly. Simulates the call from the queue mechanism
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("runschedule")]
        public ActionResult RunSchedule(CompositionMessage message)
        {

            return new OkResult();
        }

        /// <summary>
        /// REST Endpoint that wil return the current configuration settings for the service. These are the running values, and may be over-written by seperate configuration operations
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("setconfig")]
        public JsonResult SetConfig(string configKey, string configValue)
        {
            _appConfig.Logging.BatchLogFileName = configValue;
            return new JsonResult(_appConfig);
        }

        /// <summary>
        /// REST Endpoint that wil return the current configuration settings for the service. These are the running values, and may be over-written by seperate configuration operations
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("getconfig")]
        public JsonResult GetConfig()
        {
            return new JsonResult(_appConfig);
        }


        /// <summary>
        /// REST end point to get the version of the application and any shared components that make sense to report on
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("versioninfo")]
        public JsonResult VersionInfo()
        {
            return new JsonResult(_appConfig.AppSettings.Versions);
        }

        /// <summary>
        /// REST Endpoint used to determine whether the services is running or not
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("heartbeat")]
        public ActionResult Heartbeat()
        {
            return new OkResult();
        }
    }
}

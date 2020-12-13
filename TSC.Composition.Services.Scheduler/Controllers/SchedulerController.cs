﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TSC.Composition.Services.Scheduler.Config;

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
        public ActionResult RunHighPriority()
        {

            return new OkResult();
        }

        /// <summary>
        /// REST Endpoint to place the message on teh low priority queue
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("runlowpriority")]
        public ActionResult RunLowPriority()
        {

            return new OkResult();
        }

        /// <summary>
        /// REST Endpoint to call the schedule directly. Simulates the call from the queue mechanism
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("runschedule")]
        public ActionResult RunSchedule()
        {

            return new OkResult();
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
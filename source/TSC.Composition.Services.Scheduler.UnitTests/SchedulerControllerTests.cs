using Microsoft.AspNetCore.Mvc;
using Moq;
using TSC.Composition.Services.Scheduler.Config;
using TSC.Composition.Services.Scheduler.Controllers;
using Xunit;

namespace TSC.Composition.Services.Scheduler.UnitTests
{
    public class SchedulerControllerTests
    {
        [Fact]
        public void RunHighPriority_Success()
        {
            var mockAppConfig = new Mock<IAppConfig>();
            mockAppConfig.Setup(x => x.AppSettings.DaysToFilter).Returns(0);

            SchedulerController scheduler = new SchedulerController(mockAppConfig.Object);
            var result = scheduler.RunHighPriority(new Shared.MessageFormats.CompositionMessage());
            Assert.NotNull(result);
            OkResult expectedResult = new OkResult();
            Assert.Equal(expectedResult.StatusCode, ((OkResult)result).StatusCode);
        }

        [Fact]
        public void RunLowPriority_Success()
        {
            var mockAppConfig = new Mock<IAppConfig>();
            mockAppConfig.Setup(x => x.AppSettings.DaysToFilter).Returns(0);

            SchedulerController scheduler = new SchedulerController(mockAppConfig.Object);
            var result = scheduler.RunLowPriority(new Shared.MessageFormats.CompositionMessage());
            Assert.NotNull(result);
            OkResult expectedResult = new OkResult();
            Assert.Equal(expectedResult.StatusCode, ((OkResult)result).StatusCode);
        }

        [Fact]
        public void RunSchedule_Success()
        {
            var mockAppConfig = new Mock<IAppConfig>();
            mockAppConfig.Setup(x => x.AppSettings.DaysToFilter).Returns(0);

            SchedulerController scheduler = new SchedulerController(mockAppConfig.Object);
            var result = scheduler.RunSchedule(new Shared.MessageFormats.CompositionMessage());
            Assert.NotNull(result);
            OkResult expectedResult = new OkResult();
            Assert.Equal(expectedResult.StatusCode, ((OkResult)result).StatusCode);
        }

        [Fact]
        public void GetConfig_Success()
        {
            var mockAppConfig = new Mock<IAppConfig>();
            mockAppConfig.Setup(x => x.AppSettings.DaysToFilter).Returns(0);

            SchedulerController scheduler = new SchedulerController(mockAppConfig.Object);
            var result = scheduler.GetConfig();
            Assert.NotNull(result);
        }

        [Fact]
        public void VersionInfo_Success()
        {
            var mockAppConfig = new Mock<IAppConfig>();
            mockAppConfig.Setup(x => x.AppSettings.DaysToFilter).Returns(0);

            SchedulerController scheduler = new SchedulerController(mockAppConfig.Object);
            var result = scheduler.VersionInfo();
            Assert.NotNull(result);
        }

        [Fact]
        public void Heartbeat_Success()
        {
            var mockAppConfig = new Mock<IAppConfig>();
            mockAppConfig.Setup(x => x.AppSettings.DaysToFilter).Returns(0);

            SchedulerController scheduler = new SchedulerController(mockAppConfig.Object);
            var result = scheduler.Heartbeat();
            Assert.NotNull(result);
        }
    }
}

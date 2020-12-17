using System;
using System.IO;
using TSC.Composition.Services.Scheduler.Config;
using Xunit;

namespace TSC.Composition.Services.Scheduler.UnitTests
{

    public class AppConfigTests
    {
        [Fact]
        public void AppConfig_Success()
        {
            //Copy the support config to the testhost.dll.config
            var assembly = System.Reflection.Assembly.GetExecutingAssembly();
            var assemblyPath = assembly.GetFiles()[0].Name;
            var assemblyDir = System.IO.Path.GetDirectoryName(assemblyPath);
            File.Copy(Path.Combine(assemblyDir, @"Support/App.config"), Path.Combine(assemblyDir, "testhost.dll.config"), true);

            IAppConfig cfg = new AppConfig();

            Assert.NotNull(cfg);
            Assert.NotNull(cfg.AppSettings);
            Assert.NotNull(cfg.ConnectionStrings);
            Assert.NotNull(cfg.Kestrel);
            Assert.NotNull(cfg.Logging);

            Assert.Equal("DEV", cfg.AppSettings.Environment);
            Assert.Equal(90, cfg.AppSettings.DaysToFilter);
            Assert.Equal(10, cfg.AppSettings.HangfireWorkerCount);
            Assert.Equal("SCHD", cfg.AppSettings.UserToRecordAgainst);
            Assert.Equal(50000, cfg.AppSettings.MaxRecordsPerCsvFile);
            TimeSpan ts = new TimeSpan();
            ts = TimeSpan.FromSeconds(15);
            Assert.Equal(ts, cfg.AppSettings.DelayBetweenCsvSchedules);
            Assert.Equal(@"C:\CorresSystem\Jobs\Batch", cfg.AppSettings.JobFolderRootPath);
            Assert.Equal(@"C:\CorresSystem\Staging\PrintVendor\[REPLACEWITHSTAGINGFOLDER]\System\input", cfg.AppSettings.BaseStagingFolder);

            Assert.Equal(Environment.MachineName, cfg.AppSettings.Server);

            Assert.Equal("Server=DEVIQ-SB03,1433;Database=HangfireCompositionDEV; Trusted_Connection=false;User Id=composition;password=Winter210!; MultipleActiveResultSets=True;MultiSubnetFailover=False;", cfg.ConnectionStrings.HangfireCompositionDb);
            Assert.Equal("Server=DEVIQ-SB03,1433;Database=HangfireDeliveryDEV; Trusted_Connection=false;User Id=composition;password=Winter210!; MultipleActiveResultSets=True;MultiSubnetFailover=False;", cfg.ConnectionStrings.HangfireDeliveryDb);
            Assert.Equal("Server=DEVIQ-SB03,1433; database=MonolithicDBDEV;Integrated Security=false;User Id=composition;password=Winter210!;MultiSubnetFailover=False;", cfg.ConnectionStrings.MonolithicDB);

            Assert.Equal("https://localhost:19130/api/TSCLogger/LogMessage", cfg.Logging.LogServiceUrl);

            Assert.Equal(10100, cfg.Kestrel.RESTAPIPort);
            Assert.False(cfg.Kestrel.UseCertificateForSSL);
            Assert.Equal("localhost", cfg.Kestrel.CertificateSubject);
            Assert.Equal("MY", cfg.Kestrel.CertificateStoreName);
            Assert.Equal("LocalMachine", cfg.Kestrel.CertificateLocation);
            Assert.True(cfg.Kestrel.CertificateAllowInvalid);
        }


        //[Fact]
        //public void AppConfig_AllNulls_Success()
        //{
        //    //Copy the support config to the testhost.dll.config
        //    var assembly = System.Reflection.Assembly.GetExecutingAssembly();
        //    var assemblyPath = assembly.GetFiles()[0].Name;
        //    var assemblyDir = System.IO.Path.GetDirectoryName(assemblyPath);
        //    File.Copy(Path.Combine(assemblyDir, @"Support\AllNull.App.config"), Path.Combine(assemblyDir, "testhost.dll.config"), true);

        //    IAppConfig cfg = new AppConfig();

        //    Assert.NotNull(cfg);
        //    Assert.NotNull(cfg.AppSettings);
        //    Assert.NotNull(cfg.ConnectionStrings);
        //    Assert.NotNull(cfg.Kestrel);
        //    Assert.NotNull(cfg.Logging);

        //    Assert.Equal("", cfg.AppSettings.Environment);
        //    Assert.Equal(90, cfg.AppSettings.DaysToFilter);
        //    Assert.Equal(10, cfg.AppSettings.HangfireWorkerCount);
        //    Assert.Equal("", cfg.AppSettings.UserToRecordAgainst);
        //    Assert.Equal(50000, cfg.AppSettings.MaxRecordsPerCsvFile);
        //    TimeSpan ts = new TimeSpan();
        //    ts = TimeSpan.FromSeconds(15);
        //    Assert.Equal(ts, cfg.AppSettings.DelayBetweenCsvSchedules);
        //    Assert.Equal("", cfg.AppSettings.JobFolderRootPath);
        //    Assert.Equal("", cfg.AppSettings.BaseStagingFolder);

        //    Assert.Equal(Environment.MachineName, cfg.AppSettings.Server);

        //    Assert.Equal("", cfg.ConnectionStrings.HangfireCompositionDb);
        //    Assert.Equal("", cfg.ConnectionStrings.HangfireDeliveryDb);
        //    Assert.Equal("", cfg.ConnectionStrings.MonolithicDB);

        //    Assert.Equal("", cfg.Logging.LogServiceUrl);

        //    Assert.Equal(0, cfg.Kestrel.RESTAPIPort);
        //    Assert.False(cfg.Kestrel.UseCertificateForSSL);
        //    Assert.Equal("", cfg.Kestrel.CertificateSubject);
        //    Assert.Equal("", cfg.Kestrel.CertificateStoreName);
        //    Assert.Equal("", cfg.Kestrel.CertificateLocation);
        //    Assert.False(cfg.Kestrel.CertificateAllowInvalid);
        //}
    }
}

using System.Diagnostics;
using System.Reflection;
using System.Threading.Tasks;
using NetMsixUpdater.Updater.Extensions.MsixUpdater;
using Xunit;
using Xunit.Abstractions;

namespace NetMsixUpdater.Test.DownloadTests
{
    [Collection("DownlaodAndInstall")]
    public class DownloadInstallTest
    {
        private readonly ITestOutputHelper output;

        public DownloadInstallTest(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void DownloadAndInstallAsyncTest() =>
            DownloadAndInstallAsync();

        private async void DownloadAndInstallAsync()
        {
            MsixUpdater msixUpdater = new(Assembly.GetExecutingAssembly(), Consts.YAML_PATH);
            msixUpdater.Build();
            Stopwatch stopwatch = new();
            
            UpdateExtension.OnDownloadStart += (_) => stopwatch.Start();
            UpdateExtension.OnDownlaodComplete += (_) => stopwatch.Stop();
            
            Task downloadInstallTask = msixUpdater.DownloadAndInstallAsync();
            await downloadInstallTask;
            
            output.WriteLine("Time (Seconds): " + stopwatch.Elapsed.Seconds);
        }
    }
}
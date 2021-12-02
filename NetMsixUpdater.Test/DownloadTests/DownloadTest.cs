using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using NetMsixUpdater.Updater.Extensions.MsixUpdater;
using Xunit;
using Xunit.Abstractions;

namespace NetMsixUpdater.Test.DownloadTests
{
    [Collection("Downlaod")]
    public class DownloadTest
    {
        private readonly ITestOutputHelper output;

        public DownloadTest(ITestOutputHelper output)
        {
            this.output = output;
        }
        /// <summary>
        /// Need to be executed after all tests in this class.
        /// Delete downloaded update from <c>Consts.MSIX_OUTPUT</c>
        /// </summary>
        private void DeleteDownloadedUpdateFile() =>
            File.Delete(Consts.MSIX_OUTPUT);

        [Fact]
        public void DownloadMsix()
        {
            MsixUpdater msixUpdater = new(Assembly.GetExecutingAssembly(), Consts.YAML_PATH);
            msixUpdater.Build();
            
            msixUpdater.DownlaodUpdate(Consts.MSIX_OUTPUT);
            
            Assert.True(File.Exists(Consts.MSIX_OUTPUT));
            DeleteDownloadedUpdateFile();
        }
        [Fact]
        public async void DownloadMsixAsync()
        {
            MsixUpdater msixUpdater = new(Assembly.GetExecutingAssembly(), Consts.YAML_PATH);
            msixUpdater.Build();
            
            Task downloadTask = msixUpdater.DownlaodUpdateAsync(Consts.MSIX_OUTPUT);
            await downloadTask;

            Assert.True(File.Exists(Consts.MSIX_OUTPUT));
            DeleteDownloadedUpdateFile();
        }
        [Fact]
        public void DownlaodEventsTest()
        {
            MsixUpdater msixUpdater = new(Assembly.GetExecutingAssembly(), Consts.YAML_PATH);
            msixUpdater.Build();
            
            UpdateExtension.OnDownloadStart += (_) =>
            {
                output.WriteLine("The download has ben started");
            };
            UpdateExtension.OnDownlaodComplete += (_) =>
            {
                output.WriteLine("The download is ended");  
            };
            msixUpdater.DownlaodUpdate(Consts.MSIX_OUTPUT);
            
            Assert.True(File.Exists(Consts.MSIX_OUTPUT));
            DeleteDownloadedUpdateFile();
        }
        [Fact]
        public void DownlaodProgressChangeEventTest()
        {
            StartProgressChangeEvent();
        }
        private async void StartProgressChangeEvent()
        {
            MsixUpdater msixUpdater = new(Assembly.GetExecutingAssembly(), Consts.YAML_PATH);
            msixUpdater.Build();
            
            UpdateExtension.OnDownloadStart += (_) =>
            {
                  output.WriteLine("StartProgressChangeEvent has ben started...");
            };
            UpdateExtension.OnUpdateDownloadProgresssChange += (_, progress) =>
            {
                output.WriteLine("Bytes recived: " + progress.BytesReceived);
                output.WriteLine("Bytes to recive: " + progress.TotalBytesToReceive);
                output.WriteLine("Progress: " + progress.ProgressPercentage);  
            };
            
            Task downloadUpdateAsync = msixUpdater.DownlaodUpdateAsync(Consts.MSIX_OUTPUT);
            downloadUpdateAsync.ContinueWith(_ =>
            {
                Assert.True(File.Exists(Consts.MSIX_OUTPUT));
                DeleteDownloadedUpdateFile();
            });
            await downloadUpdateAsync;
        }
    }
}

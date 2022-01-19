using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using NetMsixUpdater.Test.Utils;
using NetMsixUpdater.Updater.Extensions.MsixUpdater;
using Xunit;
using Xunit.Abstractions;

namespace NetMsixUpdater.Test
{
    public class DownloadTest
    {
        private readonly ITestOutputHelper output;
        private MsixUpdater _msixUpdater { get; }

        public DownloadTest(ITestOutputHelper output)
        {
            this.output = output;
            _msixUpdater = new(Assembly.GetExecutingAssembly(), Consts.YAML_MODEL_FILE, "prod");
            _msixUpdater.Build();
        }

        [Fact]
        public void DownloadMsix()
        {
            _msixUpdater.DownlaodUpdate(Consts.MSIX_OUTPUT);
            
            Assert.True(File.Exists(Consts.MSIX_OUTPUT));
        }
        [Fact]
        public async void DownloadMsixAsync()
        {
            Task downloadTask = _msixUpdater.DownlaodUpdateAsync(Consts.MSIX_OUTPUT);
            await downloadTask;

            Assert.True(File.Exists(Consts.MSIX_OUTPUT));
        }
        [Fact]
        public void DownlaodEventsTest()
        {
            UpdateExtension.OnDownloadStart += (_) =>
            {
                output.WriteLine("The download has ben started");
            };
            UpdateExtension.OnDownlaodComplete += (_) =>
            {
                output.WriteLine("The download is ended");  
            };
            _msixUpdater.DownlaodUpdate(Consts.MSIX_OUTPUT);
            
            Assert.True(File.Exists(Consts.MSIX_OUTPUT));
        }
        [Fact]
        public void DownlaodProgressChangeEventTest()
        {
            StartProgressChangeEvent();
        }
        private async void StartProgressChangeEvent()
        {
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
            
            Task downloadUpdateAsync = _msixUpdater.DownlaodUpdateAsync(Consts.MSIX_OUTPUT);
            downloadUpdateAsync.ContinueWith(_ =>
            {
                Assert.True(File.Exists(Consts.MSIX_OUTPUT));
            });
            await downloadUpdateAsync;
        }
    }
}

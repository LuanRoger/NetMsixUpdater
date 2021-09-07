using System.Diagnostics;
using System.Net;
using System.Reflection;
using NetMsixUpdater.Updater.Extensions;
using NetMsixUpdater.Updater.Extensions.MsixUpdater;
using Xunit;

namespace NetMsixUpdater.Test.DownloadTests
{
    [Collection("Downlaod")]
    public class ProgressChangeTest
    {
        [Fact]
        public async void DownloadProgressChange()
        {
            MsixUpdater msixUpdater = new(Assembly.GetExecutingAssembly(), Consts.YAML_PATH);
            msixUpdater.Build();
            
            UpdateExtension.OnUpdateDownloadProgresssChange += UpdateExtensionOnOnUpdateDownloadProgresssChange;
            await msixUpdater.DownlaodUpdateAsync(Consts.MSIX_OUTPUT);
        }

        private void UpdateExtensionOnOnUpdateDownloadProgresssChange(object sender, DownloadProgressChangedEventArgs e)
        {
            Debug.WriteLine("Bytes recived: " + e.BytesReceived);
            Debug.WriteLine("Percentage: " + e.ProgressPercentage + "%");
        }
    }
}
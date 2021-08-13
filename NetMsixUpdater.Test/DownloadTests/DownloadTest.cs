using System.Reflection;
using NetMsixUpdater.Updater.Extensions;
using Xunit;

namespace NetMsixUpdater.Test.DownloadTests
{
    [Collection("Downlaod")]
    public class DownloadTest
    {
        [Fact]
        public void DownloadMsix()
        {
            MsixUpdater msixUpdater = new(Assembly.GetExecutingAssembly(), Consts.YAML_PATH);
            msixUpdater.Build();
            
            msixUpdater.DownlaodUpdate(Consts.MSIX_OUTPUT);
        }
        [Fact]
        public async void DownloadMsixAsync()
        {
            MsixUpdater msixUpdater = new(Assembly.GetExecutingAssembly(), Consts.YAML_PATH);
            msixUpdater.Build();
            
            await msixUpdater.DownlaodUpdateAsync(Consts.MSIX_OUTPUT);
        }
    }
}

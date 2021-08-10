using NetMsixUpdater.Updater.Extensions;
using System.Reflection;
using Xunit;

namespace NetMsixUpdater.Test
{
    public class DownloadTest
    {
        [Fact]
        public void DownloadMsix()
        {
            MsixUpdater msixUpdater = new(Assembly.GetExecutingAssembly(), Consts.YAML_PATH);
            msixUpdater.Build();
            
            msixUpdater.VerifyAndDownload();
        }
        [Fact]
        public async void DownloadMsixAsync()
        {
            MsixUpdater msixUpdater = new(Assembly.GetExecutingAssembly(), Consts.YAML_PATH);
            msixUpdater.Build();

            await msixUpdater.VerifyAndDownloadAsync();
        }
    }
}

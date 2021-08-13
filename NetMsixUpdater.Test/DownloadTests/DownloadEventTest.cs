using System.Diagnostics;
using System.Reflection;
using NetMsixUpdater.Updater.Extensions;
using Xunit;

namespace NetMsixUpdater.Test.DownloadTests
{
    [Collection("Downlaod")]
    public class DownloadEventTest
    {
        [Fact]
        public void DownlaodDoneEventTest()
        {
            MsixUpdater msixUpdater = new(Assembly.GetExecutingAssembly(), Consts.YAML_PATH);
            msixUpdater.Build();
            
            UpdateExtension.OnDownlaodComplete += (_) =>
            {
                Debug.WriteLine("Done.");
            };
            msixUpdater.DownlaodUpdate(Consts.MSIX_OUTPUT);
        }
    }
}
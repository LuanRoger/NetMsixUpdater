using System.Reflection;
using Xunit;

namespace NetMsixUpdater.Test.VirifyTests
{
    public class VerifyesTest
    {
        [Fact]
        public void VerifyUpdate()
        {
            MsixUpdater msixUpdater = new(Assembly.GetExecutingAssembly(), Consts.YAML_PATH);
            msixUpdater.Build();
            
            //This assembly will always be outdated compared with Yaml local file
            Assert.False(msixUpdater.hasUpdated);
        }

        [Fact]
        public void VerifyUpdateInServer()
        {
            MsixUpdater msixUpdater = new(Assembly.GetExecutingAssembly(), Consts.YAML_SERVER_PATH);
            msixUpdater.Build();
            
            //This assembly will always be updated compared with Yaml server file
            Assert.True(msixUpdater.hasUpdated);
        }
    }
}

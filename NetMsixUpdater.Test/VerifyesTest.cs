using System.Reflection;
using Xunit;

namespace NetMsixUpdater.Test
{
    public class VerifyesTest
    {
        [Fact]
        public void VerifyUpdate()
        {
            MsixUpdater msixUpdater = new(Assembly.GetExecutingAssembly(), Consts.YAML_PATH);
            msixUpdater.Build();

            Assert.False(msixUpdater.hasUpdated);
        }

        [Fact]
        public void VerifyUpdateInServer()
        {
            MsixUpdater msixUpdater = new(Assembly.GetExecutingAssembly(), Consts.YAML_SERVER_PATH);
            msixUpdater.Build();

            Assert.True(msixUpdater.hasUpdated);
        }
    }
}

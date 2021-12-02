using System.Reflection;
using Xunit;

namespace NetMsixUpdater.Test.DeserializeTests
{
    public class DeserializeTest
    {
        [Fact]
        public void Deserialize()
        {
            MsixUpdater msixUpdater = new(Assembly.GetExecutingAssembly(), Consts.YAML_PATH);
            msixUpdater.Build();
            
            Assert.NotNull(msixUpdater.yamlUpdateInfo);
            Assert.NotEqual(string.Empty, msixUpdater.yamlUpdateInfo.version.ToString());
        }
    }
}

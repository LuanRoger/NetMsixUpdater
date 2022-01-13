using System.Reflection;
using NetMsixUpdater.Test.Utils;
using Xunit;

namespace NetMsixUpdater.Test
{
    [Collection("FileManipulation")]
    public class DeserializeTest
    {
        public DeserializeTest()
        {
            YamlFileWriter.WriteYamlFile();
        }
        
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

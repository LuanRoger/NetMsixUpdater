using System.Reflection;
using NetMsixUpdater.Test.Utils;
using Xunit;

namespace NetMsixUpdater.Test
{
    [Collection("FileManipulation")]
    public class VerifyesTest
    {
        public VerifyesTest()
        {
            YamlFileWriter.WriteYamlFile();
        }
        
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
            MsixUpdater msixUpdater = new(Assembly.GetExecutingAssembly(), Consts.YAML_FILE_IN_SERVER);
            msixUpdater.Build();
            
            Assert.True(msixUpdater.hasUpdated);
        }
    }
}

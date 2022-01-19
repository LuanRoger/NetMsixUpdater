using System.Reflection;
using NetMsixUpdater.Test.Utils;
using Xunit;

namespace NetMsixUpdater.Test
{
    public class VerifyesTest
    {
        [Fact]
        public void VerifyUpdate()
        {
            MsixUpdater msixUpdater = new(Assembly.GetExecutingAssembly(), Consts.YAML_MODEL_FILE, "prod");
            msixUpdater.Build();
            
            Assert.False(msixUpdater.hasUpdated);
        }
        
        //TODO: Dedicate file in server
        /*[Fact]
        public void VerifyUpdateInServer()
        {
            MsixUpdater msixUpdater = new(Assembly.GetExecutingAssembly(), Consts.YAML_FILE_IN_SERVER, "prod");
            msixUpdater.Build();
            
            Assert.True(msixUpdater.hasUpdated);
        }*/
    }
}

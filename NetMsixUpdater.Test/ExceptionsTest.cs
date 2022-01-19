using System.Reflection;
using NetMsixUpdater.Exeptions;
using NetMsixUpdater.Test.Utils;
using Xunit;

namespace NetMsixUpdater.Test
{
    public class ExceptionsTest
    {
        [Fact]
        public void HasBuildedExeptionInBuildTest()
        {
            MsixUpdater msixUpdater = new(Assembly.GetExecutingAssembly(), Consts.YAML_MODEL_FILE, "prod");
            
            msixUpdater.Build();
            Assert.Throws<HasBuildedExeption>(() => msixUpdater.Build());
        }

        [Fact]
        public void NoUpdateChannelCreatedTest()
        {
            MsixUpdater msixUpdater = new(Assembly.GetExecutingAssembly(), Consts.YAML_INCORECT_NO_CHANNELS_MODEL,
                "hml");
            
            Assert.Throws<NoUpdateChannelCreated>(msixUpdater.Build);
        }
        
        [Fact]
        public void YamlFieldNullExeptionTest()
        {
            MsixUpdater msixUpdater = new(Assembly.GetExecutingAssembly(), Consts.YAML_INCORECT_MODEL_FILE,
                "hml");
            
            Assert.Throws<YamlFieldNullExeption>(msixUpdater.Build);
        }
    }
}
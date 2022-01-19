using System.Reflection;
using NetMsixUpdater.Test.Utils;
using NetMsixUpdater.YamlInfo.Models;
using Xunit;

namespace NetMsixUpdater.Test
{
    [Collection("FileManipulation")]
    public class DeserializeTest
    {
        private MsixUpdater _msixUpdater { get; }

        public DeserializeTest()
        {
            _msixUpdater = new(Assembly.GetExecutingAssembly(), Consts.YAML_MODEL_FILE, "prod");
            _msixUpdater.Build();
        }
        
        [Fact]
        public void DeserializationTest()
        {
            Assert.NotNull(_msixUpdater.yamlUpdateInfo);
            Assert.NotNull(_msixUpdater.yamlUpdateInfo.channels);
        }
        [Fact]
        public void CheckAllValuesTest()
        {
            foreach (ChannelInfo channelInfo in _msixUpdater.yamlUpdateInfo.channels.Values)
            {
                Assert.NotNull(channelInfo.version);
                Assert.NotNull(channelInfo.url);
                Assert.NotNull(channelInfo.extension);
                Assert.NotNull(channelInfo.changelog);
                Assert.NotNull(channelInfo.mandatory);
            }
        }
        [Fact]
        public void CheckNonMandatoryValuesTest()
        {
            MsixUpdater msixUpdater = new(Assembly.GetEntryAssembly(), Consts.YAML_INCOMPLTETE_MODEL_FILE,
                "prod");
            msixUpdater.Build();
            
            foreach (ChannelInfo channelInfo in msixUpdater.yamlUpdateInfo.channels.Values)
            {
                Assert.NotNull(channelInfo.version);
                Assert.NotNull(channelInfo.url);
                Assert.NotNull(channelInfo.extension);
                Assert.Null(channelInfo.changelog);
                Assert.False(channelInfo.mandatory);
            }
        }
    }
}

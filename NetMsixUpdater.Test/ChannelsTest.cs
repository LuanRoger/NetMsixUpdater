using System.Reflection;
using NetMsixUpdater.Exeptions;
using NetMsixUpdater.Test.Utils;
using Xunit;

namespace NetMsixUpdater.Test
{
    public class ChannelsTest
    {
        private MsixUpdater _msixUpdater { get; set; }

        public ChannelsTest()
        {
            ResetMsixUpdater();
        }
        private void ResetMsixUpdater()
        {
            _msixUpdater = new(Assembly.GetExecutingAssembly(), Consts.YAML_MODEL_FILE, "prod");
            _msixUpdater.Build();
        }
        
        [Fact]
        public void GetCurrentChannelTest()
        {
            Assert.NotNull(_msixUpdater.currentUpdateChannel);
            
            Assert.NotNull(_msixUpdater.currentChannelInfo);
            Assert.Equal("prod", _msixUpdater.currentUpdateChannel);
        }
        [Fact]
        public void SetCurrentChannelTest()
        {
            _msixUpdater.currentUpdateChannel = "dev";
            _msixUpdater.Build();
            
            Assert.Equal("dev", _msixUpdater.currentUpdateChannel);
            Assert.NotNull(_msixUpdater.currentChannelInfo);
            
            ResetMsixUpdater();
        }
        [Fact]
        public void ChangeChannelAndNotBuildTest()
        {
            _msixUpdater.currentUpdateChannel = "dev";
            
            Assert.Throws<NonBuildedException>(() => _msixUpdater.currentChannelInfo);
            
            ResetMsixUpdater();
        }
        
        [Fact]
        public void ChannelsToStringTest()
        {
            Assert.Equal("prod, dev", _msixUpdater.yamlUpdateInfo.ToString());
        }
    }
}
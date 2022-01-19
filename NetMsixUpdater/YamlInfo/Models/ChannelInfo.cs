using System;

namespace NetMsixUpdater.YamlInfo.Models
{
    public sealed class ChannelInfo
    {
        public Version version { get; internal set; }
        public string url { get; internal set; }
        public string extension {get; internal set;}
        public string changelog { get; internal set; }
        public bool mandatory { get; internal set; }
        
    }
}

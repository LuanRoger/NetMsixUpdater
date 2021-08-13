using System;

namespace NetMsixUpdater.YamlInfo.Model
{
    public sealed class YamlFileInfo
    {
        public Version version {get; internal set;}
        public string url { get; internal set; }
        public string extension {get; internal set;}
        public string changelog { get; internal set; }
        public bool mandatory { get; internal set; }
    }
}

using System;

namespace NetMsixUpdater.YamlInfo.Model
{
    public sealed class YamlFileInfo
    {
        public Version version {get; set;}
        public string url { get; set; }
        public string extension {get; set;}
        public string changelog { get; set; }
        public bool mandatory { get; set; }
    }
}

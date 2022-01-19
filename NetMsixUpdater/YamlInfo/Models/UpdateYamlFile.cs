using System.Collections.Generic;

namespace NetMsixUpdater.YamlInfo.Models
{
    public class UpdateYamlFile
    {
        public Dictionary<string, ChannelInfo> channels { get; set; }
    }
}
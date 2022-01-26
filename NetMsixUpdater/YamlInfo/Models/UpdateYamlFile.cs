using System.Collections.Generic;

namespace NetMsixUpdater.YamlInfo.Models
{
    public class UpdateYamlFile
    {
        public Dictionary<string, ChannelInfo> channels { get; internal set; }
        
        /// <summary>
        /// Get all channels separated by comma
        /// </summary>
        /// <returns>Channels separated by comma</returns>
        public override string ToString()
        {
            List<string> stringChannels = new();
            foreach (string channelsName in channels.Keys)
                stringChannels.Add(channelsName);
            
            return string.Join(", ", stringChannels.ToArray());
        }
    }
}
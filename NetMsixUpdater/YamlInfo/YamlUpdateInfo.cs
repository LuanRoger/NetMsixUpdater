using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using NetMsixUpdater.Exeptions;
using NetMsixUpdater.YamlInfo.Models;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace NetMsixUpdater.YamlInfo
{
    internal sealed class YamlUpdateInfo : IDisposable
    {
        private string yamlText { get; set; }
        private bool isInServer { get; }

        internal YamlUpdateInfo(string yamlFilePath)
        {
            isInServer = Uri.TryCreate(yamlFilePath, UriKind.Absolute, out Uri tempUriResult) && 
                         (tempUriResult.Scheme == Uri.UriSchemeHttp || tempUriResult.Scheme == Uri.UriSchemeHttps);

            if(isInServer)
                using(WebClient webClient = new()) yamlText = webClient.DownloadString(yamlFilePath);
            else 
                yamlText = File.ReadAllText(yamlFilePath);
        }
        ~YamlUpdateInfo() => Dispose();

        internal UpdateYamlFile DeserializeYaml()
        {
            IDeserializer deserialize = new DeserializerBuilder()
                .WithNamingConvention(UnderscoredNamingConvention.Instance)
                .Build();
            
            
            UpdateYamlFile result = deserialize.Deserialize<UpdateYamlFile>(yamlText);
            VerifyDerializationResult(result);
            
            return result;
        }
        
        private void VerifyDerializationResult(UpdateYamlFile result)
        {
            var channels = result.channels;
            if(channels == null || channels.Count == 0)
                throw new NoUpdateChannelCreated();

            foreach (ChannelInfo channel in channels.Values)
                if(channel.version == null || channel.url == null || channel.extension == null)
                    throw new YamlFieldNullExeption(channels.GetType().Name);
        }
        
        public void Dispose()
        {
            yamlText = null;

            GC.SuppressFinalize(this);
        }
    }
}

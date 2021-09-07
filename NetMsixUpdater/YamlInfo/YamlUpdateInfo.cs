using System;
using System.IO;
using System.Net;
using System.Reflection;
using NetMsixUpdater.Exeptions;
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

        internal T DeserializeYaml<T>()
        {
            IDeserializer deserialize = new DeserializerBuilder()
                .WithNamingConvention(UnderscoredNamingConvention.Instance)
                .Build();
            
            
            T result = deserialize.Deserialize<T>(yamlText);
            VerifyDerializationResult(result);
            
            return result;
        }
        
        private void VerifyDerializationResult(object result)
        {
            var properties = result.GetType().GetProperties();
            foreach (PropertyInfo propertyInfo in properties)
                if(propertyInfo.GetValue(result) == null) throw new YamlFieldNullExeption(propertyInfo.Name);
        }
        
        public void Dispose()
        {
            yamlText = null;

            GC.SuppressFinalize(this);
        }
    }
}

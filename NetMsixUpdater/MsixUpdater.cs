using System;
using System.Reflection;
using NetMsixUpdater.YamlInfo;
using NetMsixUpdater.YamlInfo.Model;

namespace NetMsixUpdater
{
    public class MsixUpdater : IDisposable
    {
        internal Assembly programAssembly { get; set; }

        public bool hasUpdated
        {
            get => Verifiers.UpdateVerifier.VerifyUpdate(this);
        }

        private string yamlPath {get; set;}
        internal YamlFileInfo yamlUpdateInfo {get; set;}

        public MsixUpdater(Assembly assembly, string yamlPath)
        {
            programAssembly = assembly;
            this.yamlPath = yamlPath;
        }
        ~MsixUpdater() => Dispose();

        public void Build()
        {
            using(YamlUpdateInfo yamlFileInfo = new(yamlPath))
                yamlUpdateInfo = (YamlFileInfo)yamlFileInfo.DeserializeYaml<YamlFileInfo>();
        }

        public bool disposed { get; set; } = false;
        public void Dispose()
        {
            programAssembly = null;
            yamlPath = null;
            yamlUpdateInfo = null;

            GC.SuppressFinalize(this);

            disposed = true;
        }
    }
}

using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Runtime.InteropServices;
using NetMsixUpdater.Exeptions;
using NetMsixUpdater.YamlInfo;
using NetMsixUpdater.YamlInfo.Models;

namespace NetMsixUpdater
{
    /// <summary>
    /// Manage update information.
    /// </summary>
    public class MsixUpdater : IDisposable
    {
        /// <summary>
        /// Assembly of the current program.
        /// </summary>
        private Assembly programAssembly { get; set; }
        
        public string currentUpdateChannel { get; }

        internal ChannelInfo currentChannel
        {
            get
            {
                if(!hasBuilded) throw new NonBuildedException();
                
                return yamlUpdateInfo.channels[currentUpdateChannel];
            }
        }

        /// <summary>
        /// Check if the assembly is update with Yaml.
        /// </summary>
        public bool hasUpdated => programAssembly.GetName().Version >= currentChannel.version;

        private bool hasBuilded { get; set; } = false;

        /// <summary>
        /// Path where the Yaml file is.
        /// </summary>
        private string yamlPath {get; set;}
        
        /// <summary>
        /// Yaml file updates information.
        /// </summary>
        public UpdateYamlFile yamlUpdateInfo { get; private set; }
        
        /// <summary>
        /// Instance new MsixUpdater
        /// </summary>
        /// <param name="assembly">Assembly of the current program.</param>
        /// <param name="yamlPath">Path where the Yaml file is. It can be an internet URI or a local path</param>
        /// <example>
        /// URI: https://raw.githubusercontent.com/LuanRoger/ProjectBook/master/update.yaml
        /// Local: C:\ProjectBook\update.yaml
        /// </example>
        public MsixUpdater(Assembly assembly, string yamlPath, [NotNull]string currentUpdateChannel)
        {
            programAssembly = assembly;
            this.yamlPath = yamlPath;
            this.currentUpdateChannel = currentUpdateChannel;
        }
        ~MsixUpdater() => Dispose();

        /// <summary>
        /// Deserializes the Yaml file indicated in <c>yamlPath</c> to <c>yamlUpdateInfo</c>.
        /// </summary>
        /// <exception cref="HasBuildedExeption">Call this method with <c>yamlUpdateInfo</c> already built may cause an exception.</exception>
        public void Build()
        {
            if(hasBuilded) 
                throw new HasBuildedExeption();
            
            using YamlUpdateInfo yamlFileInfo = new(yamlPath);
            
            yamlUpdateInfo = yamlFileInfo.DeserializeYaml();
            
            hasBuilded = true;
        }

        public void Dispose()
        {
            programAssembly = null;
            yamlPath = null;
            yamlUpdateInfo = null;
            hasBuilded = false;

            GC.SuppressFinalize(this);
        }
    }
}

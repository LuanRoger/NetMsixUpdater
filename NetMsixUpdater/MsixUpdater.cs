using System;
using System.Reflection;
using NetMsixUpdater.Exeptions;
using NetMsixUpdater.YamlInfo;
using NetMsixUpdater.YamlInfo.Model;

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
        internal Assembly programAssembly { get; private set; }
        
        /// <summary>
        /// Check if the assembly is update with Yaml.
        /// </summary>
        public bool hasUpdated
        {
            get
            {
                if(!hasBuilded) throw new NonBuildedException();
                
                return programAssembly.GetName().Version >= yamlUpdateInfo.version;
            }
        }

        private bool hasBuilded { get; set; } = false;

        /// <summary>
        /// Path where the Yaml file is.
        /// </summary>
        private string yamlPath {get; set;}
        
        /// <summary>
        /// Yaml file updates information.
        /// </summary>
        public YamlFileInfo yamlUpdateInfo { get; private set; }
        
        /// <summary>
        /// Instance new MsixUpdater
        /// </summary>
        /// <param name="assembly">Assembly of the current program.</param>
        /// <param name="yamlPath">Path where the Yaml file is. It can be an internet URI or a local path</param>
        /// <example>
        /// URI: https://raw.githubusercontent.com/LuanRoger/ProjectBook/master/update.yaml
        /// Local: C:\ProjectBook\update.yaml
        /// </example>
        public MsixUpdater(Assembly assembly, string yamlPath)
        {
            programAssembly = assembly;
            this.yamlPath = yamlPath;
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
            
            yamlUpdateInfo = yamlFileInfo.DeserializeYaml<YamlFileInfo>();
            
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

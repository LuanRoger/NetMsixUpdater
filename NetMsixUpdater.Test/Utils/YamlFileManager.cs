using System.IO;

namespace NetMsixUpdater.Test.Utils
{
    internal static class YamlFileWriter
    {
        internal static void WriteYamlFile()
        {
            File.WriteAllText(Consts.YAML_PATH, Consts.YAML_TEXT_FILE);
        }
    }
}
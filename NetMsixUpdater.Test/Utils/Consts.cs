namespace NetMsixUpdater.Test.Utils
{
    internal static class Consts
    {
        internal const string YAML_PATH = @".\update_model.yaml";
        
        internal const string YAML_TEXT_FILE = "version: 2.0.0.0\n" +
                                               "url: https://github.com/LuanRoger/ProjectBook/releases/download/v0.6.9-beta/ProjectBook.Pakage_0.6.9.0_AnyCPU.msix\n" +
                                               "extension: .msix\n" +
                                               "changelog: https://github.com/LuanRoger/ProjectBook/releases/tag/v0.6.9-beta\n" +
                                               "mandatory: false";
        internal const string YAML_FILE_IN_SERVER = "https://raw.githubusercontent.com/LuanRoger/ProjectBook/master/update.yaml";

        internal static readonly string MSIX_OUTPUT = @".\msix_output.msix";
    }
}

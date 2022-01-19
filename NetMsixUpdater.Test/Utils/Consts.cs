namespace NetMsixUpdater.Test.Utils
{
    internal static class Consts
    {
        internal const string YAML_MODEL_FILE = @"..\..\..\updateModel.yaml";
        internal const string YAML_INCOMPLTETE_MODEL_FILE = @"..\..\..\updateIncompleteModel.yaml";
        internal const string YAML_INCORECT_MODEL_FILE = @"..\..\..\updateIncorectModel.yaml";
        internal const string YAML_INCORECT_NO_CHANNELS_MODEL = @"..\..\..\updateIncorectNoChannelsModel.yaml";
        
        internal const string YAML_FILE_IN_SERVER = "https://raw.githubusercontent.com/LuanRoger/ProjectBook/master/update.yaml";
        internal static readonly string MSIX_OUTPUT = @".\msix_output.msix";
    }
}

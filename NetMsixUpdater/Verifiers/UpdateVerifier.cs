namespace NetMsixUpdater.Verifiers
{
    internal static class UpdateVerifier
    {
        internal static bool VerifyUpdate(MsixUpdater msixUpdater) =>
            msixUpdater.programAssembly.GetName().Version >= msixUpdater.yamlUpdateInfo.version;
    }
}

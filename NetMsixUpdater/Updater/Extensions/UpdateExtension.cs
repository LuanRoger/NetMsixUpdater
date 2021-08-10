using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace NetMsixUpdater.Updater.Extensions
{
    public static class UpdateExtension
    {
        private static string installerPath = Path.GetTempPath() + "NetMsixUpdaterInstaller";

        public static void VerifyAndDownload(this MsixUpdater msixUpdater)
        {
            string fileName = installerPath + msixUpdater.yamlUpdateInfo.extension;

            if(msixUpdater.hasUpdated) return;

            using(WebClient webClient = new())
                webClient.DownloadFile(msixUpdater.yamlUpdateInfo.url, fileName);

            OnDownlaodCompleteCall();

            InstallUpdate(fileName);
        }
        public async static Task VerifyAndDownloadAsync(this MsixUpdater msixUpdater)
        {
            string fileName = installerPath + msixUpdater.yamlUpdateInfo.extension;

            if(!msixUpdater.hasUpdated)
            {
                WebClient webClient = new();
                await webClient.DownloadFileTaskAsync(new Uri(msixUpdater.yamlUpdateInfo.url), fileName)
                    .ContinueWith(installer => webClient.Dispose())
                    .ContinueWith(completeCall => OnDownlaodCompleteCall());

                 InstallUpdate(fileName);
            }
        }

        private static void InstallUpdate(string fileName)
        {
            Process instalationProcess = new()
            {
                StartInfo = new ProcessStartInfo()
                {
                    UseShellExecute = true,
                    FileName = fileName
                }
            };
            instalationProcess.Start();
        }

        public delegate void CompleteEventHandler(EventArgs e);

        public static event CompleteEventHandler OnDownlaodComplete;
        private static void OnDownlaodCompleteCall() => OnDownlaodComplete?.Invoke(new EventArgs());
    }
}

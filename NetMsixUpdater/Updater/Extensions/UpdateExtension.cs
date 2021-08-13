using System;
using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;

namespace NetMsixUpdater.Updater.Extensions
{
    public static class UpdateExtension
    {
        public static void DownloadAndInstall(this MsixUpdater msixUpdater)
        {
            string fileName = Consts.installerPath + msixUpdater.yamlUpdateInfo.extension;

            if(msixUpdater.hasUpdated) return;

            using(WebClient webClient = new())
                webClient.DownloadFile(msixUpdater.yamlUpdateInfo.url, fileName);
            
            OnDownlaodCompleteCall();
            InstallUpdate(fileName);
        }
        public static Task DownloadAndInstallAsync(this MsixUpdater msixUpdater)
        {
            string fileName = Consts.installerPath + msixUpdater.yamlUpdateInfo.extension;

            if(msixUpdater.hasUpdated) return null;

            using WebClient webClient = new();
            
            webClient.DownloadProgressChanged += OnUpdateDownloadProgressChangeCall;
            webClient.DownloadFileCompleted += (_, _) => OnDownlaodCompleteCall();
                             
            return webClient.DownloadFileTaskAsync(new Uri(msixUpdater.yamlUpdateInfo.url), fileName)
                .ContinueWith(_ => InstallUpdate(fileName));
        }
        
        /// <summary>
        /// Only download the update.
        /// </summary>
        /// <param name="msixUpdater">Base <c>MsixUpdater</c> instance</param>
        /// <param name="savePath">Path where the update file will be saved (Without extension)</param>
        public static void DownlaodUpdate(this MsixUpdater msixUpdater, string savePath)
        {
            string fileName = savePath + msixUpdater.yamlUpdateInfo.extension;
            
            if(msixUpdater.hasUpdated) return;

            using(WebClient webClient = new())
                webClient.DownloadFile(msixUpdater.yamlUpdateInfo.url, fileName);
            
            OnDownlaodCompleteCall();
        }
        /// <summary>
        /// Only download the update.
        /// </summary>
        /// <param name="msixUpdater">Base <c>MsixUpdater</c> instance</param>
        /// <param name="savePath">Path where the update file will be saved (Without extension)</param>
        /// <returns>Will return <c>null</c> if has updated.</returns>
        public static Task? DownlaodUpdateAsync(this MsixUpdater msixUpdater, string savePath)
        {
            string fileName = savePath + msixUpdater.yamlUpdateInfo.extension;
            
            if(msixUpdater.hasUpdated) return null;

            using WebClient webClient = new();
            webClient.DownloadProgressChanged += OnUpdateDownloadProgressChangeCall;
            webClient.DownloadFileCompleted += (_, _) => OnDownlaodCompleteCall();
                
            return webClient.DownloadFileTaskAsync(new Uri(msixUpdater.yamlUpdateInfo.url), fileName);
        }

        private static void InstallUpdate(string fileName)
        {
            Process instalationProcess = new()
            {
                StartInfo = new()
                {
                    UseShellExecute = true,
                    FileName = fileName
                }
            };
            instalationProcess.Start();
        }

        #region Delegates
        public delegate void CompleteEventHandler(EventArgs e);
        #endregion

        #region Events
        public static event CompleteEventHandler OnDownlaodComplete;
        public static event DownloadProgressChangedEventHandler OnUpdateDownloadProgresssChange;
        #endregion

        #region EventsCalls
        private static void OnDownlaodCompleteCall() => OnDownlaodComplete?.Invoke(EventArgs.Empty);
        private static void OnUpdateDownloadProgressChangeCall(object sender, DownloadProgressChangedEventArgs eventArgs) => 
            OnUpdateDownloadProgresssChange?.Invoke(sender, eventArgs);
        #endregion
    }
}

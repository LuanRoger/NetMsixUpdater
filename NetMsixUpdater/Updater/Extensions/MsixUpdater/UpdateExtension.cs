using System;
using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;

#nullable enable

namespace NetMsixUpdater.Updater.Extensions.MsixUpdater
{

    /// <summary>
    /// Extensions for <c>MsixUpdater</c> to download and install updates
    /// </summary>
    public static partial class UpdateExtension
    {
        /// <summary>
        /// Download and install the update.
        /// The update file is stored in Temp folder.
        /// </summary>
        public static void DownloadAndInstall(this NetMsixUpdater.MsixUpdater msixUpdater)
        {
            string fileName = Consts.installerPath + msixUpdater.currentChannelInfo.extension;

            if(msixUpdater.hasUpdated) return;
            
            OnStartDownloadCall();
            using(WebClient webClient = new())
                webClient.DownloadFile(msixUpdater.currentChannelInfo.url, fileName);
            
            OnDownlaodCompleteCall();
            InstallUpdate(fileName);
        }
        
        /// <summary>
        /// Download and install the update asynchronously.
        /// The update file is stored in Temp folder.
        /// </summary>
        /// <returns>Will return <c>null</c> if has updated.</returns>
        public static Task? DownloadAndInstallAsync(this NetMsixUpdater.MsixUpdater msixUpdater)
        {
            string fileName = Consts.installerPath + msixUpdater.currentChannelInfo.extension;

            if(msixUpdater.hasUpdated) return null;
            
            OnStartDownloadCall();
            using WebClient webClient = new();
            
            webClient.DownloadProgressChanged += OnUpdateDownloadProgressChangeCall;
            webClient.DownloadFileCompleted += (_, _) => OnDownlaodCompleteCall();
                             
            return webClient.DownloadFileTaskAsync(new Uri(msixUpdater.currentChannelInfo.url), fileName)
                .ContinueWith(_ => InstallUpdate(fileName));
        }
        
        /// <summary>
        /// Only download the update.
        /// </summary>
        /// <param name="savePath">Path where the update file will be saved (Without extension)</param>
        public static void DownlaodUpdate(this NetMsixUpdater.MsixUpdater msixUpdater, string savePath)
        {
            string fileName = savePath.Contains(msixUpdater.currentChannelInfo.extension) ? savePath : 
                savePath + msixUpdater.currentChannelInfo.extension;
            
            if(msixUpdater.hasUpdated) return;
            
            OnStartDownloadCall();
            using(WebClient webClient = new())
                webClient.DownloadFile(msixUpdater.currentChannelInfo.url, fileName);
            
            OnDownlaodCompleteCall();
        }
        
        /// <summary>
        /// Only download the update asynchronously.
        /// </summary>
        /// <param name="savePath">Path where the update file will be saved (Without extension)</param>
        /// <returns>Will return <c>null</c> if has updated.</returns>
        public static Task? DownlaodUpdateAsync(this NetMsixUpdater.MsixUpdater msixUpdater, string savePath)
        {
            string fileName = savePath.Contains(msixUpdater.currentChannelInfo.extension) ? savePath : 
                savePath + msixUpdater.currentChannelInfo.extension;
            
            if(msixUpdater.hasUpdated) return null;

            using WebClient webClient = new();
            webClient.DownloadProgressChanged += OnUpdateDownloadProgressChangeCall;
            webClient.DownloadFileCompleted += (_, _) => OnDownlaodCompleteCall();
                
            return webClient.DownloadFileTaskAsync(new Uri(msixUpdater.currentChannelInfo.url), fileName);
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
        
        #region EventsCalls
        private static void OnStartDownloadCall() => OnDownloadStart?.Invoke(EventArgs.Empty);
        private static void OnDownlaodCompleteCall() => OnDownlaodComplete?.Invoke(EventArgs.Empty);
        private static void OnUpdateDownloadProgressChangeCall(object sender, DownloadProgressChangedEventArgs eventArgs) => 
            OnUpdateDownloadProgresssChange?.Invoke(sender, eventArgs);
        #endregion
    }
}

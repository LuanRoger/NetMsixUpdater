using System;
using System.Net;

namespace NetMsixUpdater.Updater.Extensions.MsixUpdater
{
    public static partial class UpdateExtension
    {
        #region Delegates
        /// <summary>
        /// Represents the method that will handle the OnDownloadStart event.
        /// </summary>
        public delegate void StartEventHandler(EventArgs e);
        /// <summary>
        /// Represents the method that will handle the OnDownlaodComplete event.
        /// </summary>
        public delegate void CompleteEventHandler(EventArgs e);
        #endregion

        #region Events
        /// <summary>
        /// Occurs when the download start
        /// </summary>
        public static event StartEventHandler OnDownloadStart;
        
        /// <summary>
        /// Occurs when the download is finished
        /// </summary>
        public static event CompleteEventHandler OnDownlaodComplete;
        
        /// <summary>
        /// Occurs when download progress is changed.
        /// </summary>
        public static event DownloadProgressChangedEventHandler OnUpdateDownloadProgresssChange;
        #endregion
    }
}
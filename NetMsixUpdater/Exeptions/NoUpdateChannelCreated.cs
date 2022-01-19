using System;

namespace NetMsixUpdater.Exeptions
{
    public class NoUpdateChannelCreated : Exception
    {
        private const string EXCEPTION_MESSAGE = "There's no update channel created or founded in YAML file.";

        public NoUpdateChannelCreated() : base(EXCEPTION_MESSAGE) { /* Nothing */ }
    }
}
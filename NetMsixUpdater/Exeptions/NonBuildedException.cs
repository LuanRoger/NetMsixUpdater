using System;

namespace NetMsixUpdater.Exeptions
{
    public class NonBuildedException : Exception
    {
        private const string EXCEPTION_MESSAGE = 
            "YamlFileInfo has not builded insted, build before try get any information";

        public NonBuildedException() : base(EXCEPTION_MESSAGE){ }
    }
}
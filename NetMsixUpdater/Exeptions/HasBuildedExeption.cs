using System;

namespace NetMsixUpdater.Exeptions
{
    public class HasBuildedExeption : Exception
    {
        private const string EXCEPTION_MESSAGE = "YamlFileInfo has already been built";

        public HasBuildedExeption() : base(EXCEPTION_MESSAGE) {}
    }
}
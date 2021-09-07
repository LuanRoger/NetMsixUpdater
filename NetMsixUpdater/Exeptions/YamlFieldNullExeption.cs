using System;

namespace NetMsixUpdater.Exeptions
{
    public class YamlFieldNullExeption : NullReferenceException
    {
        private const string EXCEPTION_MESSAGE = "Any field in the Yaml model file must not be null. " +
                                                 "{0} is null or missing";

        public YamlFieldNullExeption(string field) : base(string.Format(EXCEPTION_MESSAGE, field)) {}
    }
}
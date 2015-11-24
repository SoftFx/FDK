using System;

namespace Mql2Fdk.Launcher.Data
{
    sealed class Periodicity
    {
        public Periodicity(string code, string description)
        {
            if (code == null)
                throw new ArgumentNullException("code");

            this.Code = code;
            this.Description = description ?? string.Empty;
        }

        public string Code { get; private set; }

        public string Description { get; private set; }
    }
}

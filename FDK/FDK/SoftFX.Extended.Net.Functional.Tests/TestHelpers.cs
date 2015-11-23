namespace SoftFX.Extended.Functional.Tests
{
    using System;
    using System.Collections.Generic;
    using SoftFX.Extended;

    static class TestHelpers
    {
        static TestHelpers()
        {
            Library.Path = "<FRE>";
        }

        public static void Execute(Action<ConnectionStringBuilder> handler, IEnumerable<ConnectionStringBuilder> builders)
        {
            foreach (var element in builders)
            {
                handler(element);
            }
        }
    }
}

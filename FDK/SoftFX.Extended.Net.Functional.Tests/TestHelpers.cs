namespace SoftFX.Extended.Functional.Tests
{
    using System;
    using System.Collections.Generic;
    using SoftFX.Extended;

    static class TestHelpers
    {
        static TestHelpers()
        {
        }

        public static void Execute(Action<ConnectionStringBuilder> handler, IEnumerable<ConnectionStringBuilder> builders)
        {
            foreach (var element in builders)
            {
                handler(element);
            }
        }
        public static void Execute(Action<ConnectionStringBuilder, TestContext> handler, IEnumerable<ConnectionStringBuilder> builders,
            TestContext testContext = null)
        {
            foreach (var element in builders)
            {
                handler(element, testContext);
            }
        }
    }
}

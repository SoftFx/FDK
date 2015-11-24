namespace SoftFX.Extended.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Collections.Generic;
    using System.IO;
    using System.Reflection;
    using SoftFX.Extended.Core;
    using SoftFX.Extended.Financial;
    using SoftFX.Extended.Financial.Generated;

    [TestClass]
    public class FinancialCalculatorTests
    {
        static FinancialCalculatorTests()
        {
            Configuration.Initialize();
        }

        [TestMethod]
        public void RunCSharpCalculator()
        {
            var cases = GetCases();

            foreach (var source in cases)
            {
                var calc = CreateCalculatorFromText(source.Text);
                var current = FormatTextFromCalculator(calc);
                if (source.Text != current)
                {
                    Assert.Fail("Mismatch of loaded and saved text for test case {0}", source.Name);
                }

                calc.Clear();
                current = FormatTextFromCalculator(calc);
                if (source.Text == current)
                {
                    Assert.Fail("Clear method does not work or incorrect test case {0}", source.Name);
                }

                calc.Calculate();
                current = FormatTextFromCalculator(calc);
                if (source.Text != current)
                {
                    Assert.Fail("Calculate method does not work properly for test case {0}", source.Name);
                }
            }
        }

        [TestMethod]
        public void RunCppCalculator()
        {
            var cases = GetCases();

            foreach (var source in cases)
            {
                using (var calc = new FinCalcProxy(Native.LrpLlCommonClient, source.Text))
                {
                    var current = calc.Format();
                    if (source.Text != current)
                    {
                        Assert.Fail("Mismatch of loaded and saved text for test case {0}", source.Name);
                    }

                    calc.Clear();
                    current = calc.ToString();
                    if (source.Text == current)
                    {
                        Assert.Fail("Clear method does not work or incorrect test case {0}", source.Name);
                    }

                    calc.Calculate();
                    current = calc.Format();
                    if (source.Text != current)
                    {
                        Assert.Fail("Calculate method does not work properly for test case {0}", source.Name);
                    }
                }
            }
        }

        static IEnumerable<Case> GetCases()
        {
            var type = typeof(Resources.FinancialCalculatorCases);
            var properties = type.GetProperties(BindingFlags.Static | BindingFlags.NonPublic);
            var result = new List<Case>(properties.Length);

            foreach (var element in properties)
            {
                if (element.PropertyType != typeof(string))
                    continue;

                var value = new Case(element.Name, (string)element.GetValue(null, null));
                result.Add(value);
            }

            return result;
        }

        static FinancialCalculator CreateCalculatorFromText(string text)
        {
            using (var stream = new MemoryStream(text.Length))
            {
                using (var writer = new StreamWriter(stream))
                {
                    writer.Write(text);
                    writer.Flush();
                    stream.Position = 0;

                    var result = FinancialCalculator.Load(stream);
                    return result;
                }
            }
        }

        static string FormatTextFromCalculator(FinancialCalculator calc)
        {
            using (var stream = new MemoryStream())
            {
                calc.Save(stream);
                stream.Position = 0;
                using (var reader = new StreamReader(stream))
                {
                    var result = reader.ReadToEnd();
                    return result;
                }
            }
        }

        sealed class Case
        {
            public Case(string name, string text)
            {
                this.Name = name;
                this.Text = text;
            }

            public string Name { get; private set; }
            public string Text { get; private set; }
        }
    }
}

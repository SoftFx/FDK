namespace Lrp.Configuration
{
    using System;
    using System.Text.RegularExpressions;
    using System.Reflection;

    class Parameters
    {
        public static void PrintUsage()
        {
            Console.WriteLine("usage:");
            Console.WriteLine("lrp [/input=<file name>] [/output=<directory>|file name>] <parameters>");
            Console.WriteLine("\t/side=client|server|writer|reader");
            Console.WriteLine("\t/language=cpp|csharp|C++|C#");
            Console.WriteLine("\t/mode=local|remote is ignored for /side=writer|reader");
            Console.WriteLine("\t[/namespace=<name>] is optional for C#; ignored for C++");
            Console.WriteLine("\t[/help|/?|-?|--?]");
            Console.WriteLine("\t/namespace is optional for C#; ignored for C++");
            Console.WriteLine("\tyou can use ':' instead of '='");
            Console.WriteLine("\t/side:writer|reader requires an output file name, otherwise output directory");
            Console.WriteLine("\t/prefix is specified prefix for all generated files (C++ only)");
            Console.WriteLine();
            Console.WriteLine("lrp [/old=<file name>] [/new=<file name>]");
        }

        internal string Input { get; private set; }
        internal string Output { get; private set; }
        internal string Namespace { get; private set; }
        internal Language Language { get; private set; }
        internal Mode Mode { get; private set; }
        internal Operation Operation { get; private set; }
        internal Side Side { get; private set; }
        internal string Old { get; private set; }
        internal string New { get; private set; }
        internal string Prefix { get; private set; }

        public Parameters(string[] args)
        {
            var pattern = new Regex("^/([a-zA-Z]+)[=:](.+)$", RegexOptions.Compiled);
            foreach (var element in args)
            {
                this.Process(pattern, element);
            }
            this.Validate();
        }

        void Validate()
        {
            if (string.IsNullOrEmpty(this.Old) || string.IsNullOrEmpty(this.New))
                this.ValidateGeneration();
            else if (string.IsNullOrEmpty(this.Input) || string.IsNullOrEmpty(this.Output))
                this.ValidateVerification();
            else
                this.Operation = Operation.Help;
        }

        void ValidateGeneration()
        {
            if (string.IsNullOrEmpty(Input))
            {
                Console.WriteLine("/input is not specified");
                this.Operation = Operation.Help;
            }
            if (string.IsNullOrEmpty(Output))
            {
                Console.WriteLine("/output is not specified");
                this.Operation = Operation.Help;
            }
            if (this.Namespace == null)
            {
                this.Namespace = string.Empty;
            }
            if (this.Language == Language.None)
            {
                Console.WriteLine("/language is not specified");
                this.Operation = Operation.Help;
            }
            if (this.Mode == Mode.None)
            {
                if (this.Side != Side.Writer && this.Side != Side.Reader)
                {
                    Console.WriteLine("/mode is not specified");
                    this.Operation = Operation.Help;
                }
            }
            else if (this.Mode != Mode.Local && this.Mode != Mode.Remote)
            {
                Console.WriteLine("/mode has invalid value; expected local or remote");
                this.Operation = Operation.Help;
            }
            if (this.Side == Side.None)
            {
                Console.WriteLine("/side is not specified");
                this.Operation = Operation.Help;
            }
            if (this.Operation == Operation.None)
            {
                this.Operation = Operation.Generation;
            }
        }

        void ValidateVerification()
        {
            if (string.IsNullOrEmpty(Old))
            {
                Console.WriteLine("/old is not specified");
                this.Operation = Operation.Help;
            }
            if (string.IsNullOrEmpty(New))
            {
                Console.WriteLine("/new is not specified");
                this.Operation = Operation.Help;
            }
            if (this.Operation == Operation.None)
            {
                this.Operation = Operation.Verification;
            }
        }

        void Process(Regex pattern, string text)
        {
            if (string.IsNullOrEmpty(text))
                throw new Exception("Unexpected empty or null string.");

            if (text == "/help" || text == "-help"  || text == "--help" || text == "/?" || text == "-?" || text == "--?")
            {
                this.Operation = Operation.Help;
                return;
            }
            var match = pattern.Match(text);
            if (match.Success)
            {
                var key = match.Groups[1].Value;
                var value = match.Groups[2].Value;
                value = Aliases.ValueFromAliase(value);
                this.Process(key, value);
            }
            else if (this.Input == null)
            {
                this.Input = text;
            }
            else if (this.Output == null)
            {
                this.Output = text;
            }
            else
            {
                Console.WriteLine("Unrecoginized argument = {0}", text);
                this.Operation = Operation.Help;
            }
        }

        void Process(string key, string value)
        {
            var type = this.GetType();
            var properties = type.GetProperties(BindingFlags.NonPublic | BindingFlags.Instance);
            foreach (var element in properties)
            {
                if (string.Compare(element.Name, key, true) == 0)
                {
                    this.Process(element, key, value);
                    return;
                }
            }
            Console.WriteLine("Unrecognized option = {0}", key);
            this.Operation = Operation.Help;
        }

        void Process(PropertyInfo property, string key, string value)
        {
            if (property.PropertyType == typeof(string))
            {
                this.ProcessString(property, key, value);
            }
            else if (property.PropertyType.IsEnum)
            {
                this.ProcessEnum(property, key, value);
            }
            else
            {
                Console.WriteLine("Unrecognized option = {0}", key);
                this.Operation = Operation.Help;
            }
        }

        void ProcessString(PropertyInfo property, string key, string value)
        {
            var obj = property.GetValue(this, null);
            if (obj != null)
            {
                Console.WriteLine("Duplicate key = {0}", key);
                this.Operation = Operation.Help;
                return;
            }
            property.SetValue(this, value, null);
        }

        void ProcessEnum(PropertyInfo property, string key, string value)
        {
            var obj = property.GetValue(this, null);
            var fields = property.PropertyType.GetFields();
            var st = obj.ToString();
            if (string.Compare(fields[1].Name, st, true) != 0)
            {
                Console.WriteLine("Duplicate key = {0}", key);
                this.Operation = Operation.Help;
            }
            for (var index = 2; index < fields.Length; ++index)
            {
                var field = fields[index];
                if (string.Compare(value, field.Name, true) == 0)
                {
                    var obj2 = field.GetValue(null);
                    property.SetValue(this, obj2, null);
                    return;
                }
            }
            Console.WriteLine("Unrecognized value = {0} for key = {1}", value, key);
        }
    }
}

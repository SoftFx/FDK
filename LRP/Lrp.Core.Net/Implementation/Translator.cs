namespace SoftFX.Lrp.Implementation
{
    using System;
    using System.Collections.Generic;

    class Translator
    {
        public Translator(string st)
            : this(ushort.MaxValue, st)
        {
        }

        public Translator(ushort componentId, string st)
        {
            this.componentId = componentId;
            var separators = new char[] { ';' };
            var entries = st.Split(separators, StringSplitOptions.RemoveEmptyEntries);
            this.Name = entries[0];
            this.Methods = new string[entries.Length - 1];
            this.methodNames = new string[entries.Length - 1];

            for (var index = 1; index < entries.Length; ++index)
            {
                var method = entries[index];
                this.Methods[index - 1] = entries[index];
                var count = method.IndexOf('@');
                var name = method.Substring(0, count);
                this.methodNames[index - 1] = name;
            }
        }

        public void Translate(ref ushort componentId, ref ushort methodId)
        {
            if (this.componentId == ushort.MaxValue)
            {
                var componentName = this.Name;
                throw new InvalidComponentException(componentName, componentId);
            }

            var newMethodId = this.methods[methodId];
            if (newMethodId == ushort.MaxValue)
            {
                var componentName = this.Name;
                var methodName = this.methodNames[methodId];
                throw new InvalidMethodException(componentName, methodName, componentId, methodId);
            }

            componentId = this.componentId;
            methodId = newMethodId;
        }

        /// <summary>
        /// The method checks, if the corresponded component and method are supported by server.
        /// You should not use the method directly.
        /// </summary>
        /// <param name="methodId">a method ID</param>
        /// <returns>true, if the component and method are supported by server, otherwise false</returns>
        public bool IsSupported(ushort methodId)
        {
            if (this.componentId == ushort.MaxValue)
                return false;

            if (methodId >= this.methods.Length)
                return false;

            return this.methods[methodId] != ushort.MaxValue;
        }

        /// <summary>
        /// The method checks, if the corresponded component is supported by server.
        /// You should not use the method directly.
        /// </summary>
        /// <returns>true, if the component is supported by server, otherwise false</returns>
        public bool IsSupported()
        {
            return this.componentId != ushort.MaxValue;
        }

        public void MatchMethods(Translator translator)
        {
            if (translator != null)
                this.DoMatchMethods(translator);
            else
                this.DoResetMethods();
        }

        void DoMatchMethods(Translator translator)
        {
            var dict = new Dictionary<string, ushort>();
            foreach (var element in translator.Methods)
            {
                dict[element] = (ushort)dict.Count;
            }

            var count = this.Methods.Length;
            this.methods = new ushort[count];

            for (var index = 0; index < count; ++index)
            {
                ushort methodId;
                if (!dict.TryGetValue(this.Methods[index], out methodId))
                {
                    methodId = ushort.MaxValue;
                }
                this.methods[index] = methodId;
            }
            this.componentId = translator.componentId;
        }

        void DoResetMethods()
        {
            this.componentId = ushort.MaxValue;
            this.methods = null;
        }

        public void MatchExceptions(Translator translator)
        {
            if (translator.Methods.Length > Methods.Length)
                throw new Exception(WrongExceptionsSpecification);

            var count = translator.Methods.Length;
            for (var index = 0; index < count; ++index)
            {
                if (Methods[index] != translator.Methods[index])
                    throw new Exception(WrongExceptionsSpecification);
            }
        }

        public void FlushMethod(Logger logger)
        {
            if (this.IsSupported())
            {
                logger.Output("{0} component is supported", this.Name);
                var count = this.Methods.Length;
                for (var index = 0; index < count; ++index)
                {
                    var name = this.Methods[index];
                    var position = name.IndexOf('@');
                    name = name.Substring(0, position);
                    if (this.IsSupported((ushort)index))
                    {
                        logger.Output("\t{0} method is supported", name);
                    }
                    else
                    {
                        logger.Output("\t{0} method is not supported", name);
                    }
                }
            }
            else
            {
                logger.Output("{0} component is not supported", this.Name);
            }
        }

        public string Name { get; private set; }
        public string[] Methods { get; private set; }

        public override string ToString()
        {
            return this.Name;
        }

        #region Fields

        ushort componentId;
        ushort[] methods;
        string[] methodNames;

        #endregion

        #region Constants

        const string WrongExceptionsSpecification = "Remote exceptions specification can not be matched to local exceptions specification.";

        #endregion
    }
}

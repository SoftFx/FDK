namespace SoftFX.Lrp.Implementation
{
    using System;
    using System.Reflection;

    class DomainHost : MarshalByRefObject
    {
        #region construction

        public void Construct(string path, string typeName)
        {
            var assembly = Assembly.LoadFrom(path);
            var type = assembly.GetType(typeName);
            this.InitializeSignature(type);
            this.InitializeInvoke(type);
        }

        void InitializeSignature(Type type)
        {
            var property = type.GetProperty("LrpSignature", BindingFlags.Public | BindingFlags.Static);
            var obj = property.GetValue(null, null);
            this.signature = (string)obj;
        }

        void InitializeInvoke(Type type)
        {
            var method = type.GetMethod("LrpInvoke", BindingFlags.Public | BindingFlags.Static);
            var func = Delegate.CreateDelegate(typeof(LocalServerInvokeHandler), method);
            this.invoke = (LocalServerInvokeHandler)func;
        }

        #endregion

        public string Signature()
        {
            return this.signature;
        }

        public unsafe int Invoke(ushort componentId, ushort methodId, void* heap, int* pSize, void** ppData, int* pCapacity)
        {
            return this.invoke(componentId, methodId, heap, pSize, ppData, pCapacity);
        }

        #region Fields

        string signature;
        LocalServerInvokeHandler invoke;

        #endregion
    }
}

using System.Linq;

namespace Mql2Fdk.Translator.Semantic.Fixes.TypeTracking
{
    public class TypeParameterTable
    {
        readonly TypeData[] _paramTypes;
        public TypeData ReturnType { get; private set; }

        public TypeParameterTable(int length)
        {
            _paramTypes = new TypeData[length];
            for (var i = 0; i < length; i++)
                _paramTypes[i] = new TypeData();
            ReturnType = new TypeData();
        }

        public TypeData[] ParamTypes
        {
            get { return _paramTypes; }
        }

        public override string ToString()
        {
            return string.Format("{1} ({0})",
                                 string.Join(", ", ParamTypes.Select(it => it.ToString())),
                                 ReturnType
                );
        }

        public TypeData this[int indexParam]
        {
            set { ParamTypes[indexParam] = value; }
            get { return ParamTypes[indexParam]; }
        }
    }
}
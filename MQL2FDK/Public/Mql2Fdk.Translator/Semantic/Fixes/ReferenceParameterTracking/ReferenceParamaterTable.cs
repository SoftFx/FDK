namespace Mql2Fdk.Translator.Semantic.Fixes.ReferenceParameterTracking
{
    public class ReferenceParamaterTable
    {
        readonly ParameterKind[] _refBools;

        public ReferenceParamaterTable(int length)
        {
            _refBools = new ParameterKind[length];
        }

        public ParameterKind[] RefBools
        {
            get { return _refBools; }
        }

        public override string ToString()
        {
            return string.Format("({0})", string.Join(", ", RefBools));
        }

        public ParameterKind this[int indexParam]
        {
            set { RefBools[indexParam] = value; }
            get { return RefBools[indexParam]; }
        }
    }
}
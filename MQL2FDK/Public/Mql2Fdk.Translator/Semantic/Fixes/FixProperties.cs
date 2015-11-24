using Mql2Fdk.Translator.Common;
using Mql2Fdk.Translator.Parser.Comon;
using Mql2Fdk.Translator.Semantic.Common;

namespace Mql2Fdk.Translator.Semantic.Fixes
{
    class FixProperties : SemanticFixForToken
    {
        public FixProperties()
            : base(TokenKind.SharpProperty)
        {
        }

        protected override void FixLogic(ParseNode node)
        {
            var advancePosition = node.PositionInParent();
            var states = node.Parent.Children.GetCleanStates();
            var replaceNode = states.ShiftSharpDefineProperty(advancePosition);
            var propertyData = replaceNode.States;
            var propName = propertyData[1].Content;
            var propType = propertyData[0].Content;
            var propValue = propertyData[3].Content;
            PropertyDictionary.SetProperty(propName, propType, propValue);
            if (replaceNode.Children != null)
                replaceNode.Children.Clear();
        }
    }
}
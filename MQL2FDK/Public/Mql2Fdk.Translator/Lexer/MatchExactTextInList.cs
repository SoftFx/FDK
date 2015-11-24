using System;
using System.Collections.Generic;
using System.Linq;

namespace Mql2Fdk.Translator.Lexer
{
    public class MatchExactTextInList : IMatcher
    {
        readonly List<string> _texts = new List<string>();

        readonly bool[] _bools = new bool[128];

        public MatchExactTextInList(IEnumerable<string> startingTexts)
        {
            _texts.AddRange(startingTexts);
            foreach (var text in _texts)
            {
                SetBoolChar(text[0]);
            }
        }

        void SetBoolChar(char ch)
        {
            _bools[ch] = true;
        }

        public int Match(string text)
        {
            var index0 = text[0];
            if (index0 > 127)
                return 0;
            if (!_bools[index0])
                return 0;
            return _texts
                .Where(item => text.StartsWith(item, StringComparison.Ordinal))
                .Select(item => item.Length)
                .FirstOrDefault();
        }

        public override string ToString()
        {
            return string.Format("Match: '{0}'", string.Join(", ", _texts));
        }
    }
}
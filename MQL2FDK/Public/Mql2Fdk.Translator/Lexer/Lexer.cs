using System;
using System.Collections.Generic;
using System.IO;
using Mql2Fdk.Translator.Common;

namespace Mql2Fdk.Translator.Lexer
{
    public sealed class Lexer : IDisposable
    {
        readonly TextReader _reader;
        readonly List<LexDefinition> _lexDefinitions;

        string _lineRemaining;

        public Lexer(TextReader reader, List<LexDefinition> lexDefinitions)
        {
            _reader = reader;
            _lexDefinitions = lexDefinitions;
            NextLine();
        }

        void NextLine()
        {
            do
            {
                _lineRemaining = _reader.ReadLine();
                ++LineNumber;
                Position = 0;
            } while (_lineRemaining != null && _lineRemaining.Length == 0);
        }

        public bool Next()
        {
            if (_lineRemaining == null)
                return false;
            foreach (var def in _lexDefinitions)
            {
                var matched = def.Matcher.Match(_lineRemaining);
                if (matched <= 0) continue;
                Position += matched;
                Token = def.Token;
                TokenContents = _lineRemaining.Substring(0, matched);
                _lineRemaining = _lineRemaining.Substring(matched);
                if (_lineRemaining.Length == 0)
                    NextLine();

                return true;
            }
            throw new Exception(string.Format("Unable to match against any tokens at line {0} position {1} \"{2}\"",
                                              LineNumber, Position, _lineRemaining));
        }

        public string TokenContents { get; private set; }

        public TokenKind Token { get; private set; }

        public int LineNumber { get; private set; }

        public int Position { get; private set; }

        public void Dispose()
        {
            _reader.Dispose();
        }
    }
}
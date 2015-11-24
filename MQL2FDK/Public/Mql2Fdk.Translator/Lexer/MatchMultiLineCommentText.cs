using System;

namespace Mql2Fdk.Translator.Lexer
{
    public class MatchMultiLineCommentText : IMatcher
    {
        readonly Mq4Lexer _mq4Lexer;

        public MatchMultiLineCommentText(Mq4Lexer mq4Lexer)
        {
            _mq4Lexer = mq4Lexer;
        }

        const string StartComments = "/*";

        static bool IsCommentOpen(string input, int index)
        {
            var cond = false;
            var ch = input[index];
            var nextCh = input[index + 1];
            if (ch == '/' && nextCh == '*')
                cond = true;
            return cond;
        }

        static bool IsCommentClose(string input, int index)
        {
            var cond = false;
            var ch = input[index];
            var nextCh = input[index + 1];
            if (ch == '*' && nextCh == '/')
                cond = true;
            return cond;
        }

        static int CommentLength(string input, ref int commentOpens)
        {
            for (var index = 0; index < input.Length - 1; index++)
            {
                if (IsCommentOpen(input, index))
                {
                    commentOpens++;
                }
                if (IsCommentClose(input, index))
                {
                    commentOpens--;
                    if (commentOpens == 0)
                    {
                        return index + 2;
                    }
                }
            }
            return input.Length;
        }

        public int Match(string text)
        {
            var startsWith = text.StartsWith(StartComments, StringComparison.Ordinal);
            if (!startsWith && _mq4Lexer.IsMultiLineComment == 0)
                return 0;
            var result = CommentLength(text, ref _mq4Lexer.IsMultiLineComment);
            return result;
        }
    }
}
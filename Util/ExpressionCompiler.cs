namespace JoyMap.Util
{
    public static class ExpressionCompiler
    {
        /// <summary>
        /// Compiles a boolean expression given as a string into a Func&lt;bool&gt;.
        /// Supported operators: AND, OR, NOT, ||, &&, !, parentheses, TRUE/FALSE literals.
        /// Identifiers map to resolvers in the provided dictionary (case-insensitive lookup).
        /// Returns null on parse/compile failure.
        /// Grammar (simplified):
        ///   Expr   := OrExpr
        ///   OrExpr := AndExpr ( ( 'OR' | '||' ) AndExpr )*
        ///   AndExpr:= NotExpr ( ( 'AND'| '&&' ) NotExpr )*
        ///   NotExpr:= ( 'NOT' | '!' )* Primary
        ///   Primary:= IDENT | '(' Expr ')' | TRUE | FALSE
        /// </summary>
        public static Func<bool>? CompileBooleanExpression(string expression, IReadOnlyDictionary<string, Func<bool>> dictionary)
            => CompileBooleanExpression(expression, dictionary, out _);

        /// <summary>
        /// Compiles a boolean expression and also returns a human-readable error message on failure.
        /// </summary>
        public static Func<bool>? CompileBooleanExpression(string expression, IReadOnlyDictionary<string, Func<bool>> dictionary, out string? error)
        {
            error = null;
            if (string.IsNullOrWhiteSpace(expression))
            {
                error = "Expression is empty.";
                return null;
            }
            if (expression.StartsWith('"') && expression.EndsWith('"') && expression.Length >= 2)
                expression = expression[1..^1];
            if (string.IsNullOrWhiteSpace(expression))
            {
                error = "Expression is empty.";
                return null;
            }

            try
            {
                var tokens = Tokenize(expression);
                int index = 0;
                string? parseError = null;

                TokenKind CurrentKind() => index < tokens.Count ? tokens[index].Kind : TokenKind.EOF;

                Func<bool>? ParseExpr() => ParseOr();

                Func<bool>? ParseOr()
                {
                    var left = ParseAnd();
                    if (left == null) return null;
                    while (Match(TokenKind.Or))
                    {
                        var right = ParseAnd();
                        if (right == null) return null;
                        var l = left;
                        left = () => l() || right();
                    }
                    return left;
                }

                Func<bool>? ParseAnd()
                {
                    var left = ParseNot();
                    if (left == null) return null;
                    while (Match(TokenKind.And))
                    {
                        var right = ParseNot();
                        if (right == null) return null;
                        var l = left;
                        left = () => l() && right();
                    }
                    return left;
                }

                Func<bool>? ParseNot()
                {
                    int notCount = 0;
                    while (Match(TokenKind.Not)) notCount++;
                    var inner = ParsePrimary();
                    if (inner == null) return null;
                    if (notCount % 2 == 0) return inner;
                    return () => !inner();
                }

                Func<bool>? ParsePrimary()
                {
                    if (Match(TokenKind.True)) return () => true;
                    if (Match(TokenKind.False)) return () => false;

                    if (Match(TokenKind.LeftParen))
                    {
                        var exprFunc = ParseExpr();
                        if (exprFunc == null) return null;
                        if (!Match(TokenKind.RightParen))
                        {
                            parseError ??= "Expected closing ')'.";
                            return null;
                        }
                        return exprFunc;
                    }

                    if (CurrentKind() == TokenKind.Identifier)
                    {
                        var ident = tokens[index].Text;
                        index++;
                        var resolver = ResolveIdentifier(dictionary, ident);
                        if (resolver == null)
                        {
                            parseError ??= $"Unknown identifier '{ident}'.";
                            return null;
                        }
                        return resolver;
                    }

                    if (CurrentKind() != TokenKind.EOF)
                        parseError ??= $"Unexpected token '{tokens[index].Text}'.";
                    else
                        parseError ??= "Unexpected end of expression.";
                    return null;
                }

                bool Match(TokenKind kind)
                {
                    if (CurrentKind() == kind)
                    {
                        index++;
                        return true;
                    }
                    return false;
                }

                var func = ParseExpr();
                if (func == null)
                {
                    error = parseError ?? "Invalid expression.";
                    return null;
                }
                if (CurrentKind() != TokenKind.EOF)
                {
                    error = $"Unexpected tokens after expression (near '{tokens[index].Text}').";
                    return null;
                }
                return func;
            }
            catch (Exception ex)
            {
                error = $"Parse error: {ex.Message}";
                return null;
            }
        }

        private static Func<bool>? ResolveIdentifier(IReadOnlyDictionary<string, Func<bool>> dict, string ident)
        {
            if (dict.TryGetValue(ident, out var f))
                return f;
            // case-insensitive fallback
            var kv = dict.FirstOrDefault(k => string.Equals(k.Key, ident, StringComparison.OrdinalIgnoreCase));
            return kv.Value;
        }

        #region Tokenizer
        private enum TokenKind
        {
            Identifier,
            And,
            Or,
            Not,
            True,
            False,
            LeftParen,
            RightParen,
            EOF
        }

        private readonly struct Token
        {
            public TokenKind Kind { get; }
            public string Text { get; }
            public Token(TokenKind kind, string text) { Kind = kind; Text = text; }
        }

        private static List<Token> Tokenize(string input)
        {
            var list = new List<Token>();
            int i = 0;
            while (i < input.Length)
            {
                char c = input[i];
                if (char.IsWhiteSpace(c)) { i++; continue; }

                // Operators (symbolic)
                if (c == '(') { list.Add(new Token(TokenKind.LeftParen, "(")); i++; continue; }
                if (c == ')') { list.Add(new Token(TokenKind.RightParen, ")")); i++; continue; }
                if (c == '!') { list.Add(new Token(TokenKind.Not, "!")); i++; continue; }
                if (c == '&' && i + 1 < input.Length && input[i + 1] == '&')
                {
                    list.Add(new Token(TokenKind.And, "&&")); i += 2; continue;
                }
                if (c == '|' && i + 1 < input.Length && input[i + 1] == '|')
                {
                    list.Add(new Token(TokenKind.Or, "||")); i += 2; continue;
                }

                // Identifiers / keywords
                if (char.IsLetter(c) || c == '_')
                {
                    int start = i;
                    i++;
                    while (i < input.Length && (char.IsLetterOrDigit(input[i]) || input[i] == '_'))
                        i++;
                    var text = input.Substring(start, i - start);
                    var upper = text.ToUpperInvariant();
                    switch (upper)
                    {
                        case "AND": list.Add(new Token(TokenKind.And, text)); break;
                        case "OR": list.Add(new Token(TokenKind.Or, text)); break;
                        case "NOT": list.Add(new Token(TokenKind.Not, text)); break;
                        case "TRUE": list.Add(new Token(TokenKind.True, text)); break;
                        case "FALSE": list.Add(new Token(TokenKind.False, text)); break;
                        default: list.Add(new Token(TokenKind.Identifier, text)); break;
                    }
                    continue;
                }

                // Unknown character: treat as invalid -> add EOF to force failure
                return new List<Token> { new Token(TokenKind.EOF, "") };
            }
            list.Add(new Token(TokenKind.EOF, ""));
            return list;
        }
        #endregion
    }
}

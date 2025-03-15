using System.Diagnostics;

namespace PrimitiveSystem.Compiler.Tokens;

public class Tokenizer
{
    public static Token[] Parse(string line)
    {
        var tokens = new List<Token>();
        foreach (var value in line.Split(' '))
        {
            if (char.IsDigit(value.First()))
                tokens.Add(new Token(value, TokenKind.Number));
            else
                tokens.Add(new Token(value, TokenKind.Identifier));
        }
            
        foreach (var token in tokens)
            Debug.Write(token + " ");
        Debug.WriteLine("");
        
        return tokens.ToArray();
    }
}
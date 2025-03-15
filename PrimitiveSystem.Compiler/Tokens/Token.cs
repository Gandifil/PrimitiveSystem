namespace PrimitiveSystem.Compiler.Tokens;

public record Token(string Value, TokenKind Kind)
{
    public override string ToString() => $"{Kind}({Value})";
};
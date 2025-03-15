using PrimitiveSystem.Compiler.Tokens;
using PrimitiveSystem.Core;

namespace PrimitiveSystem.Compiler.Commands;

public record CommandDescription(string Name, Instruction Instruction, params ArgumentKind[] Arguments)
{
    public Command Generate(Token[] tokens)
    {
        var args = new int[Arguments.Length];
        for (int i = 0; i < Arguments.Length; i++)
        {
            var value = tokens[i + 1].Value;
            args[i] = Arguments[i] switch
            {
                ArgumentKind.Register => (int)Enum.Parse<Register>(value, true),
                ArgumentKind.Number => int.Parse(value),
                ArgumentKind.None => 0,
            };
        }
        return new Command(Instruction, args);
    }

    public static CommandDescription AutoParse(Instruction instruction)
    {
        var line = instruction.ToString();
        var tokens = line.Split(["Arg", "To", "From"]);
        var argumentKinds = tokens[1..].Select(x => x switch
        {
            "Register" => ArgumentKind.Register,
            "Number" => ArgumentKind.Number,
        }).ToArray();
        return new CommandDescription(tokens[0], instruction, argumentKinds);
    }
};
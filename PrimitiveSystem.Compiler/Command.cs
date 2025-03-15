using PrimitiveSystem.Compiler.Commands;
using PrimitiveSystem.Compiler.Tokens;
using PrimitiveSystem.Core;

namespace PrimitiveSystem.Compiler;

public class Command
{
    public Token Instruction { get; set; }

    public CommandDescription CommandDescription { get; set; }
    
    public Token[] Arguments { get; set; }

    public RawCommand ToRaw()
    {
        var args = new int[Arguments.Length];
        for (int i = 0; i < Arguments.Length; i++)
        {
            var value = Arguments[i].Value;
            args[i] = CommandDescription.Arguments[i] switch
            {
                ArgumentKind.Register => (int)Enum.Parse<Register>(value, true),
                ArgumentKind.Number => int.Parse(value),
                ArgumentKind.None => 0,
            };
        }
        return new RawCommand(CommandDescription.Instruction, args);
    }

    private Instruction GetRawInstructions()
    {
        if (Enum.TryParse(Instruction.Value, true, out Instruction instruction))
            return instruction;
        
        throw new KeyNotFoundException($"Could not determine instruction {Instruction.Value}");
    }
}
using PrimitiveSystem.Core;

namespace PrimitiveSystem.Compiler;

public record RawCommand(Instruction Instruction, int[] Arguments)
{
    public void Write(BinaryWriter writer)
    {
        writer.Write((int)Instruction);
        foreach (var argument in Arguments)
            writer.Write(argument);
    }
};
using System.ComponentModel.Design.Serialization;

namespace PrimitiveSystem.Core;

public record Command(Instruction Instruction, int[] Arguments)
{
    public const int DefaultArgumentCount = 2;
    public const int Length = 4 * (1 + DefaultArgumentCount);
    
    public void Write(BinaryWriter writer)
    {
        writer.Write((int)Instruction);
        for (var i = 0; i < DefaultArgumentCount; i++) 
            writer.Write(i < Arguments.Length ? Arguments[i] : 0);
    }
    
    public static Command ReadFrom(BinaryReader reader)
    {
        var instruction = (Instruction)reader.ReadInt32();
        var arguments = new int[DefaultArgumentCount];
        for (var i = 0; i < DefaultArgumentCount; i++)
            arguments[i] = reader.ReadInt32();
        
        return new Command(instruction, arguments);
    }
};
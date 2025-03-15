using PrimitiveSystem.Core;

namespace PrimitiveSystem.Processor;

public class Processor
{
    private int[] _registers = new int[(int)Register.Length];
    
    public void Process(BinaryReader reader)
    {
        while (reader.BaseStream.Position < reader.BaseStream.Length)
            Process((Instruction)reader.ReadInt32(), reader);
    }

    private void Process(Instruction instruction, BinaryReader reader)
    {
        var firstArg = reader.ReadInt32();
        switch (instruction)
        {
            case Instruction.MoveNumberToRegister:
                _registers[reader.ReadInt32()] = firstArg;
                break;
            case Instruction.AddNumberToRegister:
                _registers[reader.ReadInt32()] += firstArg;
                break;
            case Instruction.PrintRegister:
                Console.Write(_registers[firstArg]);
                break;
        }
    }
}
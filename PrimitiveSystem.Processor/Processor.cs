using PrimitiveSystem.Core;

namespace PrimitiveSystem.Processor;

public class Processor
{
    private Command[] _commands;
    private int[] _registers = new int[(int)Register.Length];

    public void LoadCommands(BinaryReader reader)
    {
        var count = reader.BaseStream.Length / Command.Length;
        _commands = new Command[count];
        for (int i = 0; i < count; i++)
            _commands[i] = Command.ReadFrom(reader);
    }

    public void Run()
    {
        for (int i = 0; i < _commands.Length; i++)
        {
            Process(_commands[i]);
        }
    }

    private void Process(Command command)
    {
        switch (command.Instruction)
        {
            case Instruction.MoveNumberToRegister:
                _registers[command.Arguments[1]] = command.Arguments[0];
                break;
            case Instruction.AddNumberToRegister:
                _registers[command.Arguments[1]] = command.Arguments[0];
                break;
            case Instruction.PrintRegister:
                Console.Write(_registers[command.Arguments[0]]);
                break;
        }
    }

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
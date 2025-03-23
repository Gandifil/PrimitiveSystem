using PrimitiveSystem.Core;

namespace PrimitiveSystem.Processor;

public class Processor
{
    private Command[] _commands;
    private Stack<int> _callStack = new();
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
        _registers[(int)Register.CommandPointer] = 0;
        while (_registers[(int)Register.CommandPointer] < _commands.Length)
        {
            Process(_commands[_registers[(int)Register.CommandPointer]]);
            _registers[(int)Register.CommandPointer]++;
        }
    }

    private void Process(Command command)
    {
        switch (command.Instruction)
        {
            case Instruction.MoveArgNumberToRegister:
                _registers[command.Arguments[1]] = command.Arguments[0];
                break;
            case Instruction.AddArgNumberToRegister:
                _registers[command.Arguments[1]] = command.Arguments[0];
                break;
            case Instruction.PrintFromRegister:
                Console.Write(_registers[command.Arguments[0]]);
                break;
            case Instruction.Exit:
                _registers[(int)Register.CommandPointer] = _commands.Length;
                break;
            case Instruction.CallToNumber:
                _callStack.Push(_registers[(int)Register.CommandPointer]);
                _registers[(int)Register.CommandPointer] = command.Arguments[0] - 1;
                break;
            case Instruction.Return:
                _registers[(int)Register.CommandPointer] = _callStack.Pop();
                break;
        }
    }
}
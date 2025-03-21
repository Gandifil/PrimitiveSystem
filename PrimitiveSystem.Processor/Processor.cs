﻿using PrimitiveSystem.Core;

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
}
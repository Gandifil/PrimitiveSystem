﻿using System.Diagnostics;
using PrimitiveSystem.Compiler;
using PrimitiveSystem.Compiler.Commands;
using PrimitiveSystem.Compiler.Tokens;
using PrimitiveSystem.Core;

public class Compiler
{
    private CommandDescription[] _commandDescriptions;
    public Compiler()
    {
        _commandDescriptions = GenerateCommandDescriptions();
    }
    
    public void Compile(StreamReader reader, BinaryWriter writer)
    {
        while (reader.EndOfStream == false)
            CompileLine(reader.ReadLine(), writer);
    }

    private void CompileLine(string? line, BinaryWriter writer)
    {
        if (string.IsNullOrEmpty(line))
            return;

        var allCommandDescriptions = GenerateCommandDescriptions();
            
        var tokens = Tokenizer.Parse(line);

        var command = new Command()
        {
            Instruction = tokens.First(),
            CommandDescription = FindCommandDescription(tokens),
            Arguments = tokens[1 ..]
        };
        
        command.ToRaw().Write(writer);
    }

    private CommandDescription FindCommandDescription(Token[] tokens)
    {
        return _commandDescriptions.First(x => x.Name == tokens.First().Value);
    }

    private CommandDescription[] GenerateCommandDescriptions()
    {
        ArgumentKind ParseArg(string s)
        {
            foreach (ArgumentKind argumentKind in Enum.GetValues(typeof(ArgumentKind)))
            {
                if (s.Contains(argumentKind.ToString()))
                    return argumentKind;
            }

            return ArgumentKind.None;
        }
        
        ArgumentKind ParseFirstArg(string s)
        {
            return ParseArg(s.Split("To")[0]);
        }
        
        ArgumentKind ParseSecondArg(string s)
        {
            return ParseArg(s.Split("To")[1]);
        }
        
        return Enum.GetValues(typeof(Instruction)).Cast<Instruction>().Select(x =>
            x.ToString() switch
            {
                { } s when s.StartsWith("Move") => new CommandDescription("MOVE", x, ParseFirstArg(s), ParseSecondArg(s)),
                { } s when s.StartsWith("Add") => new CommandDescription("ADD", x, ParseFirstArg(s), ParseSecondArg(s)),
                { } s when s.StartsWith("Print") => new CommandDescription("PRINT", x, ParseArg(s))
            }
        ).ToArray();
    }
}
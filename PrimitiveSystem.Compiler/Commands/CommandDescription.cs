using PrimitiveSystem.Core;

namespace PrimitiveSystem.Compiler.Commands;

public record CommandDescription(string Name, Instruction Instruction, params ArgumentKind[] Arguments)
{
    // public static CommandDescription[] All =
    // [
    //     new ("MOVE", Instruction.MoveNumberToRegister, Arg)
    // ];
};
namespace PrimitiveSystem.Core;

// always must be <name>[Arg|To|From<ArgumentKind1>[Arg|To|From<ArgumentKind2>...]]
public enum Instruction
{
    // None = 0,
    // MOVE
    MoveArgNumberToRegister = 11,
    // ADD
    AddArgNumberToRegister = 21,
    // SPECIAL
    PrintFromRegister = 91,
    // CONTROL
    Exit = 100,
}
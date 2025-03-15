using System.CommandLine;
using System.Diagnostics;

var inputOption = new Option<string>([ "--input", "-i" ],"The path to the psasm file that is to be converted");
var outputOption = new Option<string>([ "--output", "-o" ],"The path to the psasm file that is to be converted");

var rootCommand = new RootCommand("Compile Psasm into code file");
rootCommand.AddOption(inputOption);
rootCommand.AddOption(outputOption);
rootCommand.SetHandler(Handle, inputOption, outputOption);
rootCommand.Invoke(args);

void Handle(string inputOptionValue, string outputOptionValue)
{
    Debug.WriteLine($"inputOptionValue={inputOptionValue}");
    Debug.WriteLine($"outputOptionValue={outputOptionValue}");
    
    using var inputStream = new FileStream(inputOptionValue, FileMode.Open);
    using var reader = new StreamReader(inputStream);
    using var outputStream = new FileStream(outputOptionValue, FileMode.Create);
    using var writer = new BinaryWriter(outputStream);
    new Compiler().Compile(reader, writer);
}
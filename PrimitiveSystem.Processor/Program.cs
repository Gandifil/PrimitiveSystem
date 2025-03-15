using PrimitiveSystem.Processor;

using var inputStream = new FileStream(args.First(), FileMode.Open);
using var reader = new BinaryReader(inputStream);

var processor = new Processor();
processor.LoadCommands(reader);
processor.Run();
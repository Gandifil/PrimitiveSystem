using System.Net.Sockets;

namespace PrimitiveSystem.Compiler;

public static class StringExtensions
{
    public static string[] Split(this string str, string[] separators)
    {
        var buffer = str;
        foreach (var separator in separators)
            buffer = buffer.Replace(separator, "_");
        return buffer.Split('_');
    }
}
using System;

namespace Pong.IO
{
    static class Output
    {
        public static void Clear()
            => Console.Clear();
        
        public static void Write(string text)
            => Console.Write(text);
        
        public static void WriteLine(string text)
            => Console.WriteLine(text);
        
        public static void Write(string text, ConsoleColor color)
        {
            ConsoleColor prevColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.Write(text);
            Console.ForegroundColor = prevColor;
        }

        public static void WriteLine(string text, ConsoleColor color)
        {
            ConsoleColor prevColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ForegroundColor = prevColor;
        }

        public static void LineBreak()
            => Console.WriteLine();
    }
}
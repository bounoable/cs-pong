using System;

namespace Pong
{
    static class Drawer
    {
        public static void DrawAt(Vector2 position, char character)
        {
            Console.SetCursorPosition(position.X, position.Y);
            Console.Write(character);
        }
    }
}
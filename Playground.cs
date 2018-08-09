using System;

namespace Pong
{
    class Playground
    {
        public Vector2 Size { get; }
        public int Width => Size.X;
        public int Height => Size.Y;
        public Vector2 Center => new Vector2(Width / 2, Height / 2);

        public Playground(Vector2 size)
        {
            if (size.X < 50) {
                throw new ArgumentOutOfRangeException("size", "Playground must be at least 50 cells wide.");
            }

            if (size.Y < 10) {
                throw new ArgumentOutOfRangeException("size", "Playground must be at least 10 cells tall.");
            }

            Size = size;
        }

        public void Draw()
        {
            DrawWalls();
        }

        void DrawWalls()
        {
            for (int x = 0; x < Width; x++) {
                Drawer.DrawAt(new Vector2(x, 0), '=');
                Drawer.DrawAt(new Vector2(x, Height - 1), '=');
            }

            for (int y = 1; y < Height - 2; y++) {
                Drawer.DrawAt(new Vector2(0, y), '|');
                Drawer.DrawAt(new Vector2(Width - 1, y), '|');
            }
        }
    }
}
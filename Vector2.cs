namespace Pong
{
    struct Vector2
    {
        public int X { get; }
        public int Y { get; }

        public Vector2(int x, int y)
        {
            X = x;
            Y = y;
        }

        public Vector2 Add(Vector2 vec)
            => new Vector2(X + vec.X, Y + vec.Y);
        
        public Vector2 Subtract(Vector2 vec)
            => new Vector2(X - vec.X, Y - vec.Y);
        
        public Vector2 Top(int count = 1)
            => new Vector2(X, Y - count);
        
        public Vector2 Bottom(int count = 1)
            => new Vector2(X, Y + count);
        
        public Vector2 Right(int count = 1)
            => new Vector2(X + count, Y);
        
        public Vector2 Left(int count = 1)
            => new Vector2(X - count, Y);
    }
}